using System.Collections;
using MemoFun.Core.Audio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MemoFun.Core.UI
{
    /// <summary>
    /// Handles splash screen loading bar and progress display.
    /// </summary>
    public class SplashScreen : UIScreen
    {
        #region Serialized Fields

        [SerializeField] private Slider loadingBar;
        [SerializeField] private TextMeshProUGUI loadingPercentText;
        [SerializeField] private float loadingDuration = 3f;

        #endregion

        #region Unity Methods

        /// <summary>
        /// Starts the loading routine when the object awakens.
        /// </summary>
        private void Awake()
        {
            StartCoroutine(LoadingRoutine());
        }

        #endregion

        #region UIScreen Overrides

        /// <summary>
        /// Shows the splash screen.
        /// </summary>
        public override void Show()
        {
            base.Show();
        }

        /// <summary>
        /// Hides the splash screen and stops all coroutines.
        /// </summary>
        public override void Hide()
        {
            base.Hide();
            StopAllCoroutines();
        }

        #endregion

        #region Loading Logic

        /// <summary>
        /// Coroutine for loading progress simulation.
        /// </summary>
        private IEnumerator LoadingRoutine()
        {
            float timer = 0f;
            SoundManager.Instance.PlayBG("BG");
            while (timer < loadingDuration)
            {
                timer += Time.deltaTime;
                var currentProgress = Mathf.Clamp01(timer / loadingDuration);

                UpdateUI(currentProgress);

                yield return null;
            }

            UpdateUI(1f);
            yield return new WaitForSeconds(1f);
            UIFactory.Instance.ShowScreen(UIScreenType.MainMenu);
        }

        /// <summary>
        /// Updates the loading bar and percentage text.
        /// </summary>
        /// <param name="progress">Progress value between 0 and 1.</param>
        void UpdateUI(float progress)
        {
            loadingBar.value = progress;
            loadingPercentText.text = Mathf.RoundToInt(progress * 100f) + "%";
        }

        #endregion
    }
}
