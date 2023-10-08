///<summary>
/// Script by The Seeker Of Virtue
/// Set BGM on start
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carto.Sound {

    public class SetMusicOnStart : MonoBehaviour {
        [Tooltip("Which music to play")]
        [SerializeField] AudioClip music;
        [Tooltip("How loud to play the song")]
        [Range (0, 1)]
        public float volume = 1;
        void Start () {
            SetBGM (music);
        }

        ///<summary>
        ///Set the music
        ///</summary>
        void SetBGM (AudioClip clip) {
            if (SoundManager.musicManager.source.clip != clip) {
                SoundManager.SetMusic (clip);
                SoundManager.musicManager.source.volume = volume;
            }
        }
    }

}