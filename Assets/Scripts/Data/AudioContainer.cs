using System.Collections.Generic;
using UnityEngine;

namespace MemoFun.Core.Audio
{
    /// <summary>
    /// ScriptableObject container for audio data list.
    /// </summary>
    [CreateAssetMenu(fileName = "AudioContainer", menuName = "Audio Config/AudioContainer")]
    public class AudioContainer : ScriptableObject
    {
        #region Fields
        /// <summary>
        /// List of audio data entries.
        /// </summary>
        public List<AudioData> audioDataList;
        #endregion
    }

    #region AudioData
    /// <summary>
    /// Serializable class for storing audio clip information.
    /// </summary>
    [System.Serializable]
    public class AudioData
    {
        /// <summary>
        /// Name of the audio clip.
        /// </summary>
        public string clipName;
        /// <summary>
        /// Reference to the audio clip asset.
        /// </summary>
        public AudioClip audioClip;
    }
    #endregion
}