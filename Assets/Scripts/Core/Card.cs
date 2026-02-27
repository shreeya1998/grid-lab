using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MemoFun.Core.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace MemoFun.Core
{
    /// <summary>
    /// Represents a card in the game, handles flipping, closing, and matching logic.
    /// </summary>
    public class Card : MonoBehaviour
    {
        #region Serialized Fields
        [SerializeField] private Image cardFace;
        [SerializeField] private Sprite cardBackSprite;
        #endregion

        #region Private Fields
        private int id;
        private Sprite _originalFace;
        private Enums.CardState _currentState = Enums.CardState.Hidden;
        #endregion

        #region Events
        public EventManager CardEventManager = new();
        #endregion

        #region Properties
        /// <summary>
        /// Gets the card's unique identifier.
        /// </summary>
        public int Id => id;

        /// <summary>
        /// Gets or sets the card's current state.
        /// </summary>
        public Enums.CardState State
        {
            get => _currentState;
            private set
            {
                _currentState = value;
                CardEventManager.NotifyObservers(this, Enums.CardEventType.CardFlipped);
            }
        }
        #endregion

        #region Card Setup
        /// <summary>
        /// Sets the card's face sprite and id, and hides the card.
        /// </summary>
        /// <param name="newFace">The sprite for the card face.</param>
        /// <param name="id">The unique card id.</param>
        internal void SetCardFace(Sprite newFace, int id)
        {
            State = Enums.CardState.Hidden;
            _originalFace = newFace;
            cardFace.sprite = cardBackSprite;
            this.id = id;
        }
        #endregion

        #region Card Actions
        /// <summary>
        /// Flips the card to reveal its face.
        /// </summary>
        public void FlipCard()
        {
            if (State == Enums.CardState.Matched) return; // Don't flip if already matched
            cardFace.transform.DOKill();
            SoundManager.Instance.PlaySFX("CardFlip");
            PlayCardFlipAnimation();
        }

        /// <summary>
        /// Plays the card flip animation.
        /// </summary>
        private void PlayCardFlipAnimation()
        {

            // first half: scale X to 0
            cardFace.transform
                .DOScaleX(0f, 0.5f)
                .SetEase(Ease.InQuad)
                .OnComplete(() =>
                {
                    cardFace.sprite = _originalFace;
                    // second half: scale X back to 1
                    cardFace.transform
                        .DOScaleX(1f, 0.5f)
                        .SetEase(Ease.OutQuad)
                        .OnComplete(() => { State = Enums.CardState.Visible; });
                });
        }

        /// <summary>
        /// Closes the card, hiding its face.
        /// </summary>
        public void CloseCard()
        {
            if (State == Enums.CardState.Visible)
            {
                cardFace.transform
                    .DOScaleX(0f, 0.5f)
                    .SetEase(Ease.InQuad)
                    .OnComplete(() =>
                    {
                        cardFace.sprite = cardBackSprite;

                        // second half: scale X back to 1
                        cardFace.transform
                            .DOScaleX(1f, 0.5f)
                            .SetEase(Ease.OutQuad)
                            .OnComplete(() => { State = Enums.CardState.Hidden; });
                    });
            }
        }

        /// <summary>
        /// Marks the card as matched.
        /// </summary>
        public void MatchCard()
        {
            State = Enums.CardState.Matched;
        }
        #endregion
    }
}
