using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoFun.Core
{
    /// <summary>
    /// Observer interface for card flip events.
    /// </summary>
    public interface ICardObserver : IObserver
    {
        #region Methods
        /// <summary>
        /// Called when a card is flipped.
        /// </summary>
        /// <param name="card">The card that was flipped.</param>
        void OnCardFlipped(Card card);
        #endregion
    }
}
