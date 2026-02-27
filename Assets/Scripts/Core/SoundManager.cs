using System.Collections.Generic;
using UnityEngine;

namespace MemoFun.Core.Audio
{
    /// <summary>
    /// Manages background music and sound effects for the game.
    /// </summary>
    public class SoundManager : MonoBehaviour
    {
        #region Singleton
        public static SoundManager Instance;
        #endregion

        #region Serialized Fields
        [Header("Audio Sources")] [SerializeField]
        private AudioSource musicSource;

        [SerializeField] private AudioSource sfxSource;

        [Header("Volume Settings")] [Range(0f, 1f)]
        public float musicVolume = 1f;

        [Range(0f, 1f)] public float sfxVolume = 1f;

        [SerializeField] private AudioContainer audioClips;
        #endregion

        #region Unity Methods
        /// <summary>
        /// Initializes the singleton instance.
        /// </summary>
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        /// <summary>
        /// Sets the initial volume for music and SFX.
        /// </summary>
        private void Start()
        {
            musicSource.volume = musicVolume;
            sfxSource.volume = sfxVolume;
        }
        #endregion

        #region Sound Methods
        /// <summary>
        /// Plays background music.
        /// </summary>
        /// <param name="clip">AudioClip to play.</param>
        /// <param name="loop">Should the music loop.</param>
        public void PlayMusic(AudioClip clip, bool loop = true)
        {
            if (clip == null) return;

            musicSource.clip = clip;
            musicSource.loop = loop;
            musicSource.volume = musicVolume;
            musicSource.Play();
        }

        /// <summary>
        /// Plays a sound effect.
        /// </summary>
        /// <param name="clip">AudioClip to play.</param>
        public void PlaySFX(AudioClip clip)
        {
            if (clip == null) return;

            sfxSource.PlayOneShot(clip, sfxVolume);
        }

        /// <summary>
        /// Plays background music by clip name.
        /// </summary>
        /// <param name="clipName">Name of the clip.</param>
        public void PlayBG(string clipName)
        {
            audioClips.audioDataList.ForEach(audioData =>
            {
                if (audioData.clipName == clipName)
                {
                    PlayMusic(audioData.audioClip);
                }
            });
        }

        /// <summary>
        /// Plays a sound effect by clip name.
        /// </summary>
        /// <param name="clipName">Name of the clip.</param>
        public void PlaySFX(string clipName)
        {
            audioClips.audioDataList.ForEach(audioData =>
            {
                if (audioData.clipName == clipName)
                {
                    PlaySFX(audioData.audioClip);
                }
            });
        }
        #endregion
    }
}