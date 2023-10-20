using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class StatisticsPanel : SingletonBase<StatisticsPanel>
    {
        [SerializeField] private Text m_TotalKills;
        [SerializeField] private Text m_BestLevelScore;
        [SerializeField] private Text m_BestTime;
        [SerializeField] private Text m_TotalScore;
        [SerializeField] private GameObject m_MainMenu;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void OnButtonBackToMainMenu()
        {
            gameObject.SetActive(false);
            m_MainMenu.SetActive(true);
        }

        public void ShowStatistics(PlayerStatistics totalStatistics)
        {
            gameObject.SetActive(true);
            m_MainMenu.SetActive(false);

            //totalStatistics.CountGeneralStats();

            m_TotalKills.text = Player.Instance.TotalKills.ToString();
            m_BestLevelScore.text = totalStatistics.bestLevelScore.ToString();
            m_BestTime.text = totalStatistics.bestTime.ToString();
            m_TotalScore.text = Player.Instance.TotalScore.ToString();
        }


    }
}
