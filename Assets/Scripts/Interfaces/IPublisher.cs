using UnityEngine;

namespace MemoFun.Core
{
    /// <summary>
    /// Publisher interface for managing observers and sending notifications.
    /// </summary>
    public interface IPublisher
    {
        #region Methods
        /// <summary>
        /// Adds an observer to the publisher.
        /// </summary>
        /// <param name="observer">The observer to add.</param>
        public void AddObserver(IObserver observer);

        /// <summary>
        /// Removes an observer from the publisher.
        /// </summary>
        /// <param name="observer">The observer to remove.</param>
        public void RemoveObserver(IObserver observer);

        /// <summary>
        /// Notifies all observers of an event.
        /// </summary>
        /// <param name="publisher">The publisher sending the notification.</param>
        /// <param name="eventType">The event type or data.</param>
        public void NotifyObservers(MonoBehaviour publisher, object eventType);
        #endregion
    }
}
