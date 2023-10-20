using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    public class LevelSequenceController : SingletonBase<LevelSequenceController>
    {
        public static string MainMenuSceneNickname = "main_menu";
        
        public Episode CurrentEpisode { get; private set; }
        
        public int CurrentLevel { get; private set; }

        public bool LastLevelResult { get; private set; }

        public PlayerStatistics LevelStatistics { get; private set; }

        public static SpaceShip PlayerShip { get; set; }

        private int bonus1 = 2;

        private int bonus2 = 3;

        private int timeForBonus1 = 10;
        private int timeForBonus2 = 5;        

        public void StartEpisode(Episode e)
        {
            CurrentEpisode = e;
            CurrentLevel = 0;

            //сбрасывает статы перед началом эпизода
            LevelStatistics = new PlayerStatistics();
            LevelStatistics.Reset();

            SceneManager.LoadScene(e.Levels[CurrentLevel]);
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
        }

        public void FinishCurrentLevel(bool success)
        {
            LastLevelResult = success;
            CalculateLevelStatistics();

            ResultPanelController.Instance.ShowResults(LevelStatistics, success);            
        }

        public void AdvanceLevel()
        {
            LevelStatistics.Reset();
            
            CurrentLevel++;
            if (CurrentEpisode.Levels.Length <= CurrentLevel)
            {
                SceneManager.LoadScene(MainMenuSceneNickname);
            }
            else
            {
                SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
            }
        }

        public void CalculateLevelStatistics()
        {
            LevelStatistics.score = Player.Instance.Score;
            LevelStatistics.numKills = Player.Instance.NumKills;            
            LevelStatistics.time = (int)LevelController.Instance.LevelTime;

            if (LevelStatistics.time <= timeForBonus1)
            {
                if (LevelStatistics.time <= timeForBonus2)
                {
                    LevelStatistics.score *= bonus2;
                    return;
                }

                LevelStatistics.score *= bonus1;
            }
        }
    }
}
