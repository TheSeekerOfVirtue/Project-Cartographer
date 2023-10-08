///<summary>
/// Script by The Seeker Of Virtue
/// Manages sound effects and music
///</summary>

using System.Collections;
using System.Collections.Generic;
using Carto.Savedata;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace Carto.Sound {

    public class SoundManager : MonoBehaviour {

        #region Values

        /*
        // not sure if I'm going to need this.
        static SoundManager getterInstance;
        public static SoundManager instance {
            get {
                if (getterInstance == null)
                    getterInstance = new SoundManager ();

                return getterInstance;
            }
            set {
                getterInstance = value;
            }
        }
        */

        static AudioMixer getterMixer;
        static public AudioMixer mixer {
            get {
                if (getterMixer == null)
                    getterMixer = Resources.Load ("MainVolume") as AudioMixer;

                return getterMixer;
            }
            set {
                getterMixer = value;
            }
        }

        static MusicManager getterMusicManager;
        public static MusicManager musicManager {
            get {
                if (getterMusicManager == null) {
                    getterMusicManager = new GameObject ("Music Manager").AddComponent<MusicManager> ();
                }
                return getterMusicManager;
            }
            set {
                getterMusicManager = value;
            }
        }

        #endregion

        #region Music

        public static void FadeToMusic (AudioClip newClip) {
            //fade out old music if it exists
            if (getterMusicManager != null)
                musicManager.FadeOutMusic (1, true);
            //create new manager, and fade in
            getterMusicManager = new GameObject ("Music Manager").AddComponent<MusicManager> ();
            musicManager.source.clip = newClip;
            musicManager.source.volume = 0;
            musicManager.FadeInMusic (1);
        }

        public static void SetMusic (AudioClip newClip) {
            musicManager.source.clip = newClip;
            musicManager.source.Play ();
        }

        #endregion

        #region Sound Effects

        ///<summary>
        /// Spawn in an audio source with a clip. Destroy when done playing
        ///</summary>
        ///<param name="clip"> the clip to play </param>
        ///<param name="volume"> volume of the sound, from 0 to 1 </param>
        ///<param name="pitch"> pitch of the sound</param>
        ///<param name="delay"> How many seconds to wait until the clip plays</param>
        ///<param name="mixerGroup"> Mixer group to use. </param>
        ///<param name="ignorePause"> Ignore audiolistener.pause? </param>
        ///<param name="threeDim"> Use 3D Sound? </param>
        ///<param name="xPos"> X Position of object </param>
        ///<param name="yPos"> Y Position of object </param>
        ///<param name="zPos"> Z Position of object </param>
        public static void SFX (AudioClip clip = null, float volume = 1, float pitch = 1, float delay = 0, string mixerGroup = "SFX", bool ignorePause = false, float threeDim = 0, float xPos = 0, float yPos = 0, float zPos = 0) {

            if (clip == null)
                return;

            AudioSource source = new GameObject ("sound " + clip.name).AddComponent<AudioSource> ();
            source.clip = clip;
            source.outputAudioMixerGroup = mixer.FindMatchingGroups (mixerGroup) [0];
            source.volume = volume;
            source.ignoreListenerPause = ignorePause;
            source.pitch = pitch;

            source.spatialBlend = threeDim;
            source.transform.position = new Vector3 (xPos, yPos, zPos);

            source.PlayDelayed (delay);
            Destroy (source.gameObject, clip.length * pitch + delay);
        }

        #endregion

        #region Setting Volume

        [RuntimeInitializeOnLoadMethod]
        static void InitVolumeOnSceneLoad () {
            SceneManager.sceneLoaded += OnSceneLoaded;
            //SetAllVolume();
        }

        static void OnSceneLoaded (Scene scene, LoadSceneMode mode) {
            SetAllVolume ();
        }

        public static void SetAllVolume () {
            SaveData data = SaveSystem.dataRef;
            SetMasterVolume (data.masterVolume);
            SetSFXVolume (data.sfxVolume);
            SetMusicVolume (data.musicVolume);
            SetVoiceVolume (data.voiceVolume);
        }

        ///<summary>
        /// Set Master Volume
        ///<summary>
        ///<param name="newVolume"> The new volume. Expects number between 0 and 1 </param>
        public static void SetMasterVolume (float newVolume) {
            mixer.SetFloat ("masterVolume", ConvertVolume (newVolume));

            if (newVolume != SaveSystem.dataRef.masterVolume) {
                SaveData data = SaveSystem.dataRef;
                data.masterVolume = newVolume;
                SaveSystem.dataRef = data;
            }
        }
        ///<summary>
        /// Set Sound Effect Volume
        ///<summary>
        ///<param name="newVolume"> The new volume. Expects number between 0 and 1 </param>
        public static void SetSFXVolume (float newVolume) {
            mixer.SetFloat ("sfxVolume", ConvertVolume (newVolume));

            if (newVolume != SaveSystem.dataRef.sfxVolume) {
                SaveData data = SaveSystem.dataRef;
                data.sfxVolume = newVolume;
                SaveSystem.dataRef = data;
            }
        }
        ///<summary>
        /// Set Music Volume
        ///<summary>
        ///<param name="newVolume"> The new volume. Expects number between 0 and 1 </param>
        public static void SetMusicVolume (float newVolume) {
            mixer.SetFloat ("musicVolume", ConvertVolume (newVolume));

            if (newVolume != SaveSystem.dataRef.musicVolume) {
                SaveData data = SaveSystem.dataRef;
                data.musicVolume = newVolume;
                SaveSystem.dataRef = data;
            }
        }

        ///<summary>
        /// Set Voice Volume
        ///<summary>
        ///<param name="newVolume"> The new volume. Expects number between 0 and 1 </param>
        public static void SetVoiceVolume (float newVolume) {
            mixer.SetFloat ("voiceVolume", ConvertVolume (newVolume));

            if (newVolume != SaveSystem.dataRef.voiceVolume) {
                SaveData data = SaveSystem.dataRef;
                data.voiceVolume = newVolume;
                SaveSystem.dataRef = data;
            }
        }

        ///<summary>
        /// Convert Volume so that it can be used with Unity's volume mixers.
        ///<summary>
        ///<param name="baseVol"> The input volume. Expects number between 0 and 1 </param>
        public static float ConvertVolume (float baseVol) => (Mathf.Log10 (Mathf.Lerp (0.0001f, 1f, baseVol)) * 20);

        #endregion
    }

}