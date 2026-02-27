using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoFun.Core
{
    #region CardData
    /// <summary>
    /// Serializable class for card data containing id and sprite.
    /// </summary>
    [System.Serializable]
    public class CardData
    {
        /// <summary>
        /// Unique identifier for the card.
        /// </summary>
        public int id;
        /// <summary>
        /// Sprite representing the card.
        /// </summary>
        public Sprite sprite;
    }
    #endregion

    /// <summary>
    /// ScriptableObject container for card collection configuration.
    /// </summary>
    [CreateAssetMenu(fileName = "CardCollectionConfig", menuName = "Card Collection/Card Collection Config")]
    public class CardCollectionConfig : ScriptableObject
    {
        #region Fields
        /// <summary>
        /// List of card data entries.
        /// </summary>
        public List<CardData> cards;
        #endregion
    }
}
