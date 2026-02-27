using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoFun.Core
{
    /// <summary>
    /// Tracks and manages user progress, including level and high score.
    /// </summary>
    public class UserProgressTracker : MonoBehaviour
    {
        #region Singleton
        public static UserProgressTracker Instance { get; private set; }
        #endregion

        #region Fields
        private UserData userData;
        private int maxLevel = 5;
        #endregion

        #region Unity Methods
        /// <summary>
        /// Initializes player data and singleton instance.
        /// </summary>
        private void Awake()
        {
            InitializePlayerData();
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
        #endregion

        #region Player Data Methods
        /// <summary>
        /// Initializes user data from PlayerPrefs.
        /// </summary>
        private void InitializePlayerData()
        {
            userData = new UserData
            {
                playerLevel = PlayerPrefs.GetInt("PlayerLevel", 1),
                playerHighScore = PlayerPrefs.GetInt("PlayerHighScore", 0)
            };
        }

        /// <summary>
        /// Gets the player's current level.
        /// </summary>
        public int GetPlayerLevel() => userData.playerLevel;

        /// <summary>
        /// Gets the player's high score.
        /// </summary>
        public int GetPlayerHighScore() => userData.playerHighScore;

        /// <summary>
        /// Saves the player's current level.
        /// </summary>
        public void SavePlayerLevel()
        {
            PlayerPrefs.SetInt("PlayerLevel", userData.playerLevel);
        }

        /// <summary>
        /// Saves the player's score if it's a new high score.
        /// </summary>
        /// <param name="score">Score to save.</param>
        public void SavePlayerScore(int score)
        {
            if (score > userData.playerHighScore)
            {
                userData.playerHighScore = score;
                PlayerPrefs.SetInt("PlayerHighScore", score);
            }
        }

        /// <summary>
        /// Increments the player's level up to the maximum allowed.
        /// </summary>
        public void IncrementPlayerLevel()
        {
            if (userData.playerLevel < maxLevel)
                userData.playerLevel++;
            SavePlayerLevel();
        }
        #endregion
    }

    /// <summary>
    /// Data structure for storing user progress.
    /// </summary>
    public class UserData
    {
        public int playerLevel;
        public int playerHighScore;
    }
}
