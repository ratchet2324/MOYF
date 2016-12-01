using UnityEngine;
using System.Collections;
using System;

namespace Terminal
{
    // List of stats type that we want to save
    public enum StatsType { BestLevel, LevelAverage, Accuracy, TimeSpent, PlayCount, SuccessCount, FailedCount, Retries };

    public class StatsManager : Singleton<StatsManager>
    {
        int levelAverage;
        int accuracy;
        int timeSpent;
        int playCount;
        int successCount;
        int failedCount;


        void Start()
        {
            UpdateStats();
        }

        void Update()
        {

        }

        public void UpdateStats()
        {
            timeSpent = GetStats(StatsType.TimeSpent);
            playCount = GetStats(StatsType.PlayCount);
            successCount = GetStats(StatsType.SuccessCount);
            failedCount = GetStats(StatsType.FailedCount);

            if (playCount == 0)
                levelAverage = 0;
            else
                levelAverage = timeSpent / playCount;
            SaveStats(StatsType.LevelAverage, levelAverage);

            if (successCount == 0)
                accuracy = 0;
            else
                accuracy = successCount * 100 / (successCount + failedCount);
            SaveStats(StatsType.Accuracy, accuracy);
        }

        public int GetStats(StatsType stats)
        {
            return PlayerPrefs.GetInt(stats.ToString(), 0);
        }

        public void SaveStats(StatsType stats)
        {
            int currentStats = GetStats(stats);
            currentStats++;
            PlayerPrefs.SetInt(stats.ToString(), currentStats);
        }

        public void SaveStats(StatsType stats, int val)
        {
            PlayerPrefs.SetInt(stats.ToString(), val);
        }

        public void SaveStats(StatsType stats, int val, bool increment)
        {
            int currentStats = GetStats(stats);
            if (increment)
            {
                currentStats += val;
            }
            else
            {
                currentStats -= val;
            }
            PlayerPrefs.SetInt(stats.ToString(), currentStats);
        }
        public void ResetStats()
        {
            var enumLength = Enum.GetNames(typeof(StatsType)).Length;
            foreach(StatsType stats in Enum.GetValues(typeof(StatsType)))
            { 
                //Debug.Log("Current key: " + stats.ToString());
                PlayerPrefs.DeleteKey(stats.ToString());
            }

            UpdateStats();
            LevelManager.instance.StopAllCoroutines();
            GUIManager.instance.MainMenu.GetComponent<MenuManager>().Spawn();
        }
    }
}
