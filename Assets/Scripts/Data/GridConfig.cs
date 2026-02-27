using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoFun.Core
{
    /// <summary>
    /// ScriptableObject container for grid configuration data.
    /// </summary>
    [CreateAssetMenu(fileName = "GridConfig", menuName = "Grid Data/Grid Config")]
    public class GridConfig : ScriptableObject
    {
        #region Fields
        /// <summary>
        /// List of grid data entries.
        /// </summary>
        public List<GridData> gridConfigData;
        #endregion
    }

    #region GridData
    /// <summary>
    /// Serializable class for grid configuration per level.
    /// </summary>
    [System.Serializable]
    public class GridData
    {
        /// <summary>
        /// Enum representing the game level.
        /// </summary>
        public Enums.GameLevel gameLevel;
        /// <summary>
        /// Level number.
        /// </summary>
        public int level;
        /// <summary>
        /// Number of rows in the grid.
        /// </summary>
        public int gridRows;
        /// <summary>
        /// Number of columns in the grid.
        /// </summary>
        public int gridColumns;
    }
    #endregion
}
