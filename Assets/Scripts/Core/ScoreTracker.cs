using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoFun.Core
{
    /// <summary>
    /// Tracks and manages the player's score, including match bonuses and penalties.
    /// </summary>
    public class ScoreTracker : MonoBehaviour
    {
        #region Fields
        private int score = 0;
        private int matchScore = 10;
        private int mismatchPenalty = 2;
        private int bonusMultiplier = 1;
        private int streakCount = 0;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the current score.
        /// </summary>
        public int Score
        {
            get => score;
            set => score = value;
        }
        #endregion

        #region Events
        /// <summary>
        /// Action to notify listeners when the score is updated.
        /// </summary>
        public static Action<int> UpdateScore;
        #endregion

        #region Score Methods
        /// <summary>
        /// Increments the score for a matched pair, applying bonus for streaks.
        /// </summary>
        public void IncrementMatchScore()
        {
            streakCount++;
            if (streakCount >= 2) bonusMultiplier = 2;
            score += matchScore * bonusMultiplier;
            UpdateScore?.Invoke(score);
        }

        /// <summary>
        /// Decreases the score for a mismatched pair and resets streak.
        /// </summary>
        public void DecreaseScore()
        {
            if (streakCount > 0) streakCount--;
            if (score > 0) score -= mismatchPenalty;
            UpdateScore?.Invoke(score);
        }

        /// <summary>
        /// Saves the current score to user progress.
        /// </summary>
        public void SaveScore()
        {
            UserProgressTracker.Instance.SavePlayerScore(score);
        }
        #endregion
    }
}
