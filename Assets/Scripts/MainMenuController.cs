using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class MainMenuController : SingletonBase<MainMenuController>
    {
        [SerializeField] private SpaceShip m_DefaultSpaceShip;
        [SerializeField] private GameObject m_EpisodeSelection;
        [SerializeField] private GameObject m_ShipSelection;
        [SerializeField] private GameObject m_Statistics;

        private void Start()
        {
            LevelSequenceController.PlayerShip = m_DefaultSpaceShip;
            gameObject.SetActive(true);
            m_EpisodeSelection.SetActive(false);
            m_ShipSelection.SetActive(false);
            m_Statistics.SetActive(false);
        }

        public void OnButtonStartNew()
        {
            m_EpisodeSelection.SetActive(true);
            gameObject.SetActive(false);
            m_ShipSelection.SetActive(false);
            m_Statistics.SetActive(false);
        }

        public void OnSelectShip()
        {
            m_ShipSelection.SetActive(true);
            gameObject.SetActive(false);
            m_Statistics.SetActive(false);
            m_EpisodeSelection.SetActive(false);
        }

        public void OnButtonStatistics()
        {
            m_Statistics.SetActive(true);
            gameObject.SetActive(false);
            m_EpisodeSelection.SetActive(false);
            m_ShipSelection.SetActive(false);            
        }
        public void OnButtonExit()
        {
            Application.Quit();
        }
    }
}
