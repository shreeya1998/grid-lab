using UnityEngine;

namespace MemoFun.Core.UI
{
    /// <summary>
    /// Abstract base class for UI screens.
    /// </summary>
    public abstract class UIScreen : MonoBehaviour
    {
        #region Methods

        /// <summary>
        /// Shows the UI screen.
        /// </summary>
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Hides the UI screen.
        /// </summary>
        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        #endregion
    }

    #region Enums

    /// <summary>
    /// Enum representing different UI screen types.
    /// </summary>
    public enum UIScreenType
    {
        SplashScreen,
        MainMenu,
        Gameplay,
        ResultScreen,
        Settings
    }

    #endregion
}
