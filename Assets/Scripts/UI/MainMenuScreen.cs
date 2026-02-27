using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MemoFun.Core.UI
{
    /// <summary>
    /// Handles main menu UI logic, including level and best score display.
    /// </summary>
    public class MainMenuScreen : UIScreen
    {
        #region Serialized Fields

        [SerializeField] private TextMeshProUGUI levelText, bestScoreText;

        #endregion

        #region Private Fields

        private int _currentLevel;

        #endregion

        #region UIScreen Overrides

        /// <summary>
        /// Shows the main menu screen and updates level and best score text.
        /// </summary>
        public override void Show()
        {
            base.Show();
            _currentLevel = UserProgressTracker.Instance.GetPlayerLevel();
            levelText.text = $"Level {_currentLevel}";
            bestScoreText.text = $"{UserProgressTracker.Instance.GetPlayerHighScore()}";
        }

        #endregion

        #region UI Callbacks

        /// <summary>
        /// Called when the game start button is clicked.
        /// </summary>
        public void OnGameStartClicked()
        {
            GameManager.Instance.StartGame();
            UIFactory.Instance.ShowScreen(UIScreenType.Gameplay);
        }

        #endregion
    }
}
