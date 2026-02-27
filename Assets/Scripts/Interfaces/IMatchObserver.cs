using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoFun.Core
{
    /// <summary>
    /// Observer interface for card match and mismatch events.
    /// </summary>
    public interface IMatchObserver : IObserver
    {
        #region Methods
        /// <summary>
        /// Called when a pair of cards is matched.
        /// </summary>
        void OnMatchedPair();

        /// <summary>
        /// Called when a pair of cards is mismatched.
        /// </summary>
        void OnMismatchedPair();
        #endregion
    }
}
