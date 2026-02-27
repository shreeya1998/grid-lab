using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MemoFun.Core.ObjectPool;

namespace MemoFun.Core
{
    /// <summary>
    /// Manages the card grid, handles card creation, shuffling, matching logic, and observer notifications.
    /// </summary>
    public class GridManager : MonoBehaviour, ICardObserver
    {
        #region Serialized Fields
        [SerializeField] private Transform cardsContainer, releaseContainer;
        [SerializeField] private GameObject cardObject;
        [SerializeField] private CardCollectionConfig cardCollectionConfig;
        [SerializeField] private GridLayoutGroup gridLayoutGroup;
        #endregion

        #region Private Fields
        private ObjectPool<Card> _cardsPool;
        private List<CardData> _cardSprites = new List<CardData>();
        private List<Card> _activeCards = new List<Card>();
        private List<Card> _openCards = new List<Card>();
        private List<Card> _matchedCards = new List<Card>();
        #endregion

        #region Events
        /// <summary>
        /// Event manager for grid events.
        /// </summary>
        public EventManager GridEventManager = new EventManager();
        #endregion

        #region Unity Methods
        /// <summary>
        /// Initializes the card object pool.
        /// </summary>
        private void Awake()
        {
            _cardsPool = new ObjectPool<Card>(cardObject, cardsContainer, releaseContainer);
        }
        #endregion

        #region Grid Creation
        /// <summary>
        /// Creates the card grid based on the provided grid data.
        /// </summary>
        /// <param name="gridData">Grid configuration data.</param>
        [ContextMenu("Create Card Grid")]
        public void CreateCardGrid(GridData gridData)
        {
            ReleaseCardPool();

            gridLayoutGroup.constraintCount = gridData.gridRows;
            int totalCards = gridData.gridRows * gridData.gridColumns;

            if (totalCards % 2 != 0)
            {
                Debug.LogError("Total cards must be even to form pairs.");
                return;
            }

            if (_cardSprites.Count < totalCards)
            {
                Debug.LogError("Not enough sprites prepared for the grid.");
                return;
            }

            for (int i = 0; i < totalCards; i++)
            {
                Card card = _cardsPool.GetObject();
                _activeCards.Add(card);
                card.SetCardFace(_cardSprites[i].sprite, _cardSprites[i].id);
                card.CardEventManager.AddObserver(this);
            }
        }
        #endregion

        #region Card Sprite Preparation
        /// <summary>
        /// Prepares and shuffles card sprites for the grid.
        /// </summary>
        /// <param name="gridData">Grid configuration data.</param>
        private void GetCardSprites(GridData gridData)
        {
            _cardSprites.Clear();

            int totalCards = gridData.gridRows * gridData.gridColumns;
            if (totalCards % 2 != 0)
            {
                Debug.LogError("Total cards must be even to form pairs.");
                return;
            }

            int pairCount = totalCards / 2;

            // Get all sprites from config and shuffle them
            List<CardData> shuffledAll = ShuffleAllCardSprites();

            if (shuffledAll.Count == 0)
            {
                Debug.LogError("No sprites found in CardCollectionConfig.");
                return;
            }

            // Clamp pairCount to available sprites
            if (pairCount > shuffledAll.Count)
            {
                Debug.LogWarning("Not enough unique sprites; some pairs will be reused.");
                pairCount = shuffledAll.Count;
            }

            // Take the first `pairCount` sprites and add each twice
            for (int i = 0; i < pairCount; i++)
            {
                CardData s = shuffledAll[i];
                _cardSprites.Add(s);
                _cardSprites.Add(s);
            }

            // If we had to clamp pairCount, we may have fewer than totalCards; guard above
            ShuffleSelectedCardSprites();
        }

        /// <summary>
        /// Shuffles the selected card sprites.
        /// </summary>
        private void ShuffleSelectedCardSprites()
        {
            for (int i = 0; i < _cardSprites.Count; i++)
            {
                int randIndex = Random.Range(i, _cardSprites.Count);
                (_cardSprites[i], _cardSprites[randIndex]) = (_cardSprites[randIndex], _cardSprites[i]);
            }
        }

        /// <summary>
        /// Shuffles all card sprites from the collection config.
        /// </summary>
        /// <returns>Shuffled list of card data.</returns>
        private List<CardData> ShuffleAllCardSprites()
        {
            var allCardSprites = cardCollectionConfig.cards;
            List<CardData> temp = new List<CardData>(allCardSprites.Count);

            foreach (var cardData in allCardSprites)
            {
                temp.Add(cardData);
            }

            for (int i = 0; i < temp.Count; i++)
            {
                int randIndex = Random.Range(i, temp.Count);
                (temp[i], temp[randIndex]) = (temp[randIndex], temp[i]);
            }

            return temp;
        }
        #endregion

        #region Observer Methods
        /// <summary>
        /// Handles notifications from card events.
        /// </summary>
        /// <param name="publisher">The card publisher.</param>
        /// <param name="eventType">The event type.</param>
        public void OnNotify(MonoBehaviour publisher, object eventType)
        {
            Card card = publisher as Card;
            if (card == null)
            {
                Debug.LogError("GridEventManager is not a Card!");
                return;
            }

            if (card.State == Enums.CardState.Matched) return;
            if (eventType is Enums.CardEventType.CardFlipped)
            {
                OnCardFlipped(card); // Call the OnCardFlipped function to handle the flipping card event
            }
        }

        /// <summary>
        /// Handles logic when a card is flipped.
        /// </summary>
        /// <param name="card">The flipped card.</param>
        public void OnCardFlipped(Card card)
        {
            if (card.State != Enums.CardState.Visible)
            {
                return;
            }


            if (_openCards.Count < 2)
            {
                _openCards.Add(card);
            }

            if (_openCards.Count == 2)
            {
                Card firstCard = _openCards[0];
                Card secondCard = _openCards[1];
                CheckPair(firstCard, secondCard);
            }
        }
        #endregion

        #region Pair Checking
        /// <summary>
        /// Checks if two cards form a matching pair and updates their state.
        /// </summary>
        /// <param name="firstCard">First card.</param>
        /// <param name="secondCard">Second card.</param>
        void CheckPair(Card firstCard, Card secondCard)
        {
            _openCards.Clear();

            bool isMatched = firstCard.Id == secondCard.Id;
            if (isMatched)
            {
                firstCard.MatchCard();
                secondCard.MatchCard();

                _matchedCards.Add(firstCard);
                _matchedCards.Add(secondCard);

                GridEventManager.NotifyObservers(this,
                    Enums.GridEventType.CardMatched);
                if (_matchedCards.Count == _activeCards.Count)
                {
                    GridEventManager.NotifyObservers(this,
                        Enums.GridEventType.AllCardsMatched); 
                }
            }
            else
            {
                firstCard.CloseCard();
                secondCard.CloseCard();
                GridEventManager.NotifyObservers(this,
                    Enums.GridEventType.CardFailed); 
            }

        }
        #endregion

        #region Pool Management
        /// <summary>
        /// Releases all cards from the pool and clears lists.
        /// </summary>
        private void ReleaseCardPool()
        {
            _cardsPool?.ReleaseAllObjects();
            foreach (var card in _activeCards)
            {
                card.CardEventManager.RemoveObserver(this);
            }

            _activeCards?.Clear();
            _matchedCards?.Clear();
            _openCards?.Clear();
        }
        #endregion

        #region Shuffle Trigger
        /// <summary>
        /// Triggers card shuffling for the current grid data.
        /// </summary>
        /// <param name="currentGridData">Current grid configuration data.</param>
        public void TriggerCardShuffle(GridData currentGridData)
        {
            _cardSprites?.Clear();
            GetCardSprites(currentGridData);
        }
        #endregion
    }
}