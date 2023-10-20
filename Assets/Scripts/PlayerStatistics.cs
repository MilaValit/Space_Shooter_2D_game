using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class PlayerStatistics : MonoBehaviour
    {
        public int numKills;
        public int score;
        public int time;
        public int TotalNumKills;
        public int totalScore;
        public int bestLevelScore;
        public int bestTime;

        public void Reset()
        {
            numKills = 0;
            score = 0;
            time = 0;
        }

        public void CountGeneralStats()
        {
            CountBestLevelScore();
            CountBestTime();            
        }

        public void CountBestLevelScore()
        {
            if (score > bestLevelScore)
            {
                bestLevelScore = score;
            }
        }

        public void CountBestTime()
        {
            if (time > bestTime)
            {
                bestTime = time;
            }
        }


    }
}
