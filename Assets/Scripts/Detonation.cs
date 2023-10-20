using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class Detonation : MonoBehaviour
    {
        [SerializeField] private float m_Radius;
        [SerializeField] private int m_Damage;
        [SerializeField] private float m_ExplosionTime;
        private Collider2D collider;


        void Start()
        {
            collider = GetComponent<CircleCollider2D>();
            Destroy(gameObject, m_ExplosionTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Destructible destructible = collision.transform.root.GetComponent<Destructible>();

            if (destructible != null)
            {
                destructible.ApplyDamage(m_Damage);
            }
        }

        public void SpawnExplosion(Transform position)
        {
            Instantiate(this, position.position, position.rotation);
        }

    }
}
