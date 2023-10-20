using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Player : SingletonBase<Player>
    {
        [SerializeField] private int m_NumLives;
        [SerializeField] private SpaceShip m_Ship;
        [SerializeField] private GameObject m_PlayerShipPrefab;
        [SerializeField] private GameObject m_RespawnEffect;

        public SpaceShip ActiveShip => m_Ship;
        [SerializeField] private CameraController m_CameraController;
        [SerializeField] private MovementController m_MovementController;

        protected override void Awake()
        {
            base.Awake();
            if (m_Ship != null)
            {
                Destroy(m_Ship.gameObject);
            }
        }

        private void Start()
        {
            Respawn();            
        }

        private void OnShipDeath()
        {
            m_NumLives--;

                if (m_NumLives > 0)
                {
                    Respawn();
                }
                else
                {
                    LevelSequenceController.Instance.FinishCurrentLevel(false);
                }
        }

        private void Respawn()
        {
            if (LevelSequenceController.PlayerShip != null)
            {
                var newPlayerShip = Instantiate(LevelSequenceController.PlayerShip);

                m_Ship = newPlayerShip.GetComponent<SpaceShip>();
                var Effect = Instantiate(m_RespawnEffect);
                Destroy(Effect, 1.5f);
                m_Ship.EventOnDeath.AddListener(OnShipDeath);
                m_CameraController.SetTarget(m_Ship.transform);
                m_MovementController.SetTargetShip(m_Ship);
            }
        }

        #region Score

        public int Score { get; private set; }
        public int TotalScore { get; private set; }
        public int NumKills { get; private set; }
        public int TotalKills { get; private set; }


        public void AddKill()
        {
            NumKills++;
            TotalKills++;
        }

        public void AddScore(int num)
        {
            Score += num;
            TotalScore += num;
        }


        #endregion
    }
}
