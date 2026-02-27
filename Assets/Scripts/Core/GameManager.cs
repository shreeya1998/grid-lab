using System.Collections.Generic;
using MemoFun.Core.Audio;
using MemoFun.Core.UI;
using UnityEngine;

namespace MemoFun.Core
{
    /// <summary>
    /// Manages the game state, handles card matching events, score tracking, and level progression.
    /// </summary>
    public class GameManager : MonoBehaviour, IMatchObserver
    {
        #region Singleton
        /// <summary>
        /// Singleton instance of GameManager.
        /// </summary>
        public static GameManager Instance { get; private set; }
        #endregion

        #region Serialized Fields
        [SerializeField] private GridManager gridManager;
        [SerializeField] private ScoreTracker scoreTracker;
        [SerializeField] private GridConfig gridConfigData;
        #endregion

        #region Private Fields
        private List<GridData> gridDataList;
        private GridData _currentGridData;
        #endregion

        #region Unity Methods
        /// <summary>
        /// Initializes the singleton instance.
        /// </summary>
        private void Awake()
        {
            if (Instance == null) Instance = this;
        }

        /// <summary>
        /// Sets up observers and initializes grid data for the current level.
        /// </summary>
        private void Start()
        {
            gridManager.GridEventManager.AddObserver(this);
            gridDataList = gridConfigData.gridConfigData;
            _currentGridData = gridDataList.Find(data => data.level == UserProgressTracker.Instance.GetPlayerLevel());
            gridManager.TriggerCardShuffle(_currentGridData);
        }
        #endregion

        #region Game Flow
        /// <summary>
        /// Starts the game by creating the card grid.
        /// </summary>
        public void StartGame()
        {
            gridManager.CreateCardGrid(_currentGridData);
        }

        /// <summary>
        /// Handles notifications from the grid manager about card events.
        /// </summary>
        /// <param name="publisher">The publisher of the event.</param>
        /// <param name="eventType">The event type.</param>
        public void OnNotify(MonoBehaviour publisher, object eventType)
        {
            if (eventType is Enums.GridEventType.CardMatched) OnMatchedPair();
            else if (eventType is Enums.GridEventType.CardFailed) OnMismatchedPair();
            else if (eventType is Enums.GridEventType.AllCardsMatched)
            {
                Debug.Log("All cards matched! Game won!");
                UIFactory.Instance.ShowScreen(UIScreenType.ResultScreen);
                UserProgressTracker.Instance.IncrementPlayerLevel();
                scoreTracker.SaveScore();
                _currentGridData =
                    gridDataList.Find(data => data.level == UserProgressTracker.Instance.GetPlayerLevel());
                gridManager.TriggerCardShuffle(_currentGridData);
            }
        }

        /// <summary>
        /// Called when a pair of cards is matched.
        /// </summary>
        public void OnMatchedPair()
        {
            scoreTracker.IncrementMatchScore();
            SoundManager.Instance.PlaySFX("CardMatched");
        }

        /// <summary>
        /// Called when a pair of cards is mismatched.
        /// </summary>
        public void OnMismatchedPair()
        {
            scoreTracker.DecreaseScore();
            SoundManager.Instance.PlaySFX("CardMismatch");

        }

        /// <summary>
        /// Restarts the game for a new level.
        /// </summary>
        public void RestartNewLevel()
        {
            StartGame();
        }
        #endregion
    }
}
