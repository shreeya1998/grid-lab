using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MemoFun.Core.UI
{
    public class GamePlayScreen : UIScreen
    {
        [SerializeField] private TextMeshProUGUI currentLevelText, scoreText;

        public override void Show()
        {
            base.Show();
            ScoreTracker.UpdateScore += UpdateScoreText;
            currentLevelText.text = $"Level {UserProgressTracker.Instance.GetPlayerLevel()}";
            scoreText.text = "0";
        }

        public override void Hide()
        {
            base.Hide();
            ScoreTracker.UpdateScore -= UpdateScoreText;
        }

        private void UpdateScoreText(int score)
        {
            scoreText.text = score.ToString();
        }

        public void OnHomeButtonClicked()
        {
            UIFactory.Instance.ShowScreen(UIScreenType.MainMenu);
        }
    }
}
