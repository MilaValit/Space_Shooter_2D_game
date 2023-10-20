using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Projectile : Entity
    {
        public enum ProjectileType
        {
            BaseProjectile,
            HomingRocket
        }

        [SerializeField] private ProjectileType m_ProjectileType;

        [SerializeField] private float m_Velocity;        

        [SerializeField] private float m_LifeTime;

        [SerializeField] private int m_Damage;

        [SerializeField] private ImpactEffect m_ImpactEffectPrefab;

        [SerializeField] private float m_DamageRadius;

        [SerializeField] private Detonation m_Detonation;

        private float m_Timer;

        private Destructible m_SelectedTarget;

        private void Update()
        {
            if (m_ProjectileType == ProjectileType.BaseProjectile)
            {
                float stepLength = Time.deltaTime * m_Velocity;
                Vector2 step = transform.up * stepLength;

                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength);

                if (hit)
                {
                    Destructible dest = hit.collider.transform.root.GetComponent<Destructible>();

                    if (dest != null && dest != m_Parent)
                    {
                        dest.ApplyDamage(m_Damage);
                        if (m_Parent.Nickname == "Player") //Modul 20.6.4.4
                        {
                            Player.Instance.AddScore(dest.ScoreValue);
                        }
                    }

                    OnProjectileLifeEnd(hit.collider, hit.point);
                }

                m_Timer += Time.deltaTime;

                if (m_Timer > m_LifeTime)
                {
                    Destroy(gameObject);
                }

                transform.position += new Vector3(step.x, step.y, 0);
            }
            
            if (m_ProjectileType == ProjectileType.HomingRocket)
            {
                m_SelectedTarget = FindNearestDestructibleTarget();
                if (m_SelectedTarget == null) return;

                Vector3 currentPos = transform.position;
                Vector3 targetPos = m_SelectedTarget.transform.position;
                
                float stepLength = Time.deltaTime * m_Velocity;
                
                Vector3 newPos = Vector3.Slerp(currentPos, targetPos, stepLength);

                transform.position = new Vector3(newPos.x, newPos.y, 0);                           

                m_Timer += Time.deltaTime;

                if (m_Timer > m_LifeTime)
                {
                    Destroy(gameObject);
                }                
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Destructible destructible = collision.transform.root.GetComponent<Destructible>();

            if (destructible == m_Parent) return;

            if (destructible != null)
            {
                destructible.ApplyDamage(m_Damage);
                Player.Instance.AddScore(destructible.ScoreValue);
                OnRocketLifeEnd(destructible.gameObject.transform.position);
                m_Detonation.SpawnExplosion(destructible.gameObject.transform);
            }
        }
        private void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
        {
            var impactEffect = Instantiate(m_ImpactEffectPrefab.gameObject, pos, Quaternion.identity);
            Destroy(gameObject);
        }

        private void OnRocketLifeEnd(Vector3 TargetPosition)
        {
            var impactEffect = Instantiate(m_ImpactEffectPrefab.gameObject, TargetPosition, Quaternion.identity);
            Destroy(gameObject);
        }

        private Destructible m_Parent;

        public void SetParentShooter(Destructible parent)
        {
            m_Parent = parent;
        }

        private Destructible FindNearestDestructibleTarget()
        {
            float maxDist = float.MaxValue;

            Destructible potentialTarget = null;

            foreach (var v in Destructible.AllDestructibles)
            {
                if (v.GetComponent<SpaceShip>() == m_Parent) continue;
                if (v.TeamId == Destructible.TeamIdNeutral) continue;  //а надо ли?              

                float dist = Vector2.Distance(this.transform.position, v.transform.position);
                if (dist < maxDist)
                {
                    maxDist = dist;
                    potentialTarget = v;
                }
            }
            return potentialTarget;
        }
    }
}
