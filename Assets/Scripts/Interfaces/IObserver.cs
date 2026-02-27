using UnityEngine;

namespace MemoFun.Core
{
    /// <summary>
    /// Observer interface for receiving notifications from publishers.
    /// </summary>
    public interface IObserver
    {
        #region Methods
        /// <summary>
        /// Called when a publisher notifies observers of an event.
        /// </summary>
        /// <param name="publisher">The publisher sending the notification.</param>
        /// <param name="eventType">The event type or data.</param>
        void OnNotify(MonoBehaviour publisher, object eventType);
        #endregion
    }
}
