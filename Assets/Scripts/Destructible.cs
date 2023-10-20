using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    /// <summary>
    /// Destructible object on the scene. An object that may have hit points. 
    /// </summary>
    public class Destructible : Entity
    {
        #region Properties
        /// <summary>
        /// Object ignores damage
        /// </summary>
        [SerializeField] private bool m_Indestructible;
        public bool isIndestructible => m_Indestructible;

        /// <summary>
        /// Start amount of hit Points
        /// </summary>
        [SerializeField] private int m_HitPoints;

        /// <summary>
        /// Current amount of hit Points
        /// </summary>
        private int m_CurrentHitPoints;
        public int HitPoints => m_CurrentHitPoints;

        public float timeBeforeRespawn;
        #endregion

        #region Unity Events
        protected virtual void Start()
        {
            m_CurrentHitPoints = m_HitPoints;
        }
        #endregion

        #region Public API

        /// <summary>
        /// Apllying damage to an object
        /// </summary>
        /// <param name="damage">Amount of damage</param>
        public void ApplyDamage(int damage)
        {
            if (m_Indestructible) return;

            m_CurrentHitPoints -= damage;

            if (m_CurrentHitPoints <= 0)
                OnDeath();
        }

        public void MakeIndestructible()
        {
            m_Indestructible = true;
        }

        public void MakeDestructible()
        {
            m_Indestructible = false;
        }
        #endregion

        /// <summary>
        /// Event of destruction, when hit points are <0
        /// </summary>
        protected virtual void OnDeath()
        {
            m_EventOnDeath?.Invoke();
            Destroy(gameObject, timeBeforeRespawn);
        }

        private static HashSet<Destructible> m_AllDestructibles;

        public static IReadOnlyCollection<Destructible> AllDestructibles => m_AllDestructibles;

        protected virtual void OnEnable()
        {
            if (m_AllDestructibles == null)
            {
                m_AllDestructibles = new HashSet<Destructible>();
            }
            m_AllDestructibles.Add(this);
        }

        protected virtual void OnDestroy()
        {
            m_AllDestructibles.Remove(this);
        }

        public const int TeamIdNeutral = 0;

        [SerializeField] private int m_TeamId;
        public int TeamId => m_TeamId;

        [SerializeField] private UnityEvent m_EventOnDeath;
        public UnityEvent EventOnDeath => m_EventOnDeath;

        #region Score
        [SerializeField] private int m_ScoreValue;

        public int ScoreValue => m_ScoreValue;
        #endregion
    }
}
