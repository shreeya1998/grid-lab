using System.Collections.Generic;
using UnityEngine;

namespace MemoFun.Core
{
    /// <summary>
    /// Manages event observers and notifies them of events.
    /// </summary>
    public class EventManager : IPublisher
    {
        #region Fields
        private List<IObserver> observers = new List<IObserver>();
        #endregion

        #region Observer Management
        /// <summary>
        /// Adds an observer to the event manager.
        /// </summary>
        /// <param name="observer">The observer to add.</param>
        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        /// <summary>
        /// Removes an observer from the event manager.
        /// </summary>
        /// <param name="observer">The observer to remove.</param>
        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }
        #endregion

        #region Notification
        /// <summary>
        /// Notifies all observers of an event.
        /// </summary>
        /// <param name="publisher">The publisher of the event.</param>
        /// <param name="eventType">The event type or data.</param>
        public void NotifyObservers(MonoBehaviour publisher, object eventType)
        {
            foreach (var observer in observers)
            {
                observer.OnNotify(publisher, eventType);
            }
        }
        #endregion
    }
}
