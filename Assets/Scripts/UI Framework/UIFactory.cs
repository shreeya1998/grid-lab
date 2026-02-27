using System;
using System.Collections.Generic;
using UnityEngine;

namespace MemoFun.Core.UI
{
    /// <summary>
    /// Factory for managing and displaying UI screens.
    /// </summary>
    public class UIFactory : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] private List<ScreenData> screens;
        [SerializeField] private UIScreen defaultScreen;

        #endregion

        #region Private Fields

        private Dictionary<UIScreenType, UIScreen> screenInstances = new();
        private UIScreen currentScreen;

        #endregion

        #region Singleton

        /// <summary>
        /// Singleton instance of UIFactory.
        /// </summary>
        public static UIFactory Instance { get; private set; }

        #endregion

        #region Unity Methods

        /// <summary>
        /// Initializes the UIFactory singleton and screen instances.
        /// </summary>
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }

            foreach (var screenData in screens)
            {
                if (!screenInstances.ContainsKey(screenData.screenType))
                {
                    screenInstances.Add(screenData.screenType, screenData.prefab);
                }
            }

            currentScreen = defaultScreen;
        }

        #endregion

        #region Screen Management

        /// <summary>
        /// Shows the specified UI screen and hides the current one.
        /// </summary>
        /// <param name="type">Type of the screen to show.</param>
        public void ShowScreen(UIScreenType type)
        {
            // Hide current screen
            if (currentScreen != null)
            {
                currentScreen.Hide();
            }

            currentScreen = screenInstances[type];
            currentScreen.Show();
        }

        #endregion

        #region Screen Data

        [System.Serializable]
        public class ScreenData
        {
            /// <summary>
            /// Type of the UI screen.
            /// </summary>
            public UIScreenType screenType;

            /// <summary>
            /// Prefab reference for the UI screen.
            /// </summary>
            public UIScreen prefab;
        }

        #endregion
    }
}