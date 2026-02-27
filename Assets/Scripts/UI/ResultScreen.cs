using System.Collections;
using System.Collections.Generic;
using MemoFun.Core.Audio;
using TMPro;
using UnityEngine;

namespace MemoFun.Core.UI
{
    /// <summary>
    /// Handles the result screen UI, displaying score and level, and navigation actions.
    /// </summary>
    public class ResultScreen : UIScreen
    {
        #region Serialized Fields

        [SerializeField] private TextMeshProUGUI scoreText, levelText;

        #endregion

        #region UIScreen Overrides

        /// <summary>
        /// Shows the result screen and updates the score text.
        /// </summary>
        public override void Show()
        {
            base.Show();
            SoundManager.Instance.PlaySFX("GameEnd");
            scoreText.text = $"Score: {UserProgressTracker.Instance.GetPlayerHighScore()}";
        }

        #endregion

        #region UI Callbacks

        /// <summary>
        /// Called when the next level button is clicked.
        /// </summary>
        public void NextLevelClicked()
        {
            GameManager.Instance.RestartNewLevel();
            UIFactory.Instance.ShowScreen(UIScreenType.Gameplay);
        }

        /// <summary>
        /// Called when the home button is clicked.
        /// </summary>
        public void HomeButtonClicked()
        {
            UIFactory.Instance.ShowScreen(UIScreenType.MainMenu);
        }

        #endregion
    }
}
