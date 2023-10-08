///<summary>
/// Script by The Seeker Of Virtue
/// Set music volume etc.
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carto.Sound {

    public class MusicManager : MonoBehaviour {

        [HideInInspector] public AudioSource source;

        void Awake () {
            source = gameObject.AddComponent<AudioSource> ();
            source.volume = 0.1f;
            source.outputAudioMixerGroup = SoundManager.mixer.FindMatchingGroups ("Music") [0];
            source.loop = true;
            DontDestroyOnLoad(gameObject);
        }

        void OnDestroy () {
            if (SoundManager.musicManager == this)
                SoundManager.musicManager = null;
        }

        #region Fading

        public void FadeInMusic (float fadeSpeed) {
            StopCoroutine ("FadeMusic");
            source.Play ();
            StartCoroutine (FadeMusic (1, fadeSpeed, false));

        }

        ///<summary>
        /// Smoothly stop playing music
        ///</summary>
        /// <paran name="fadeSpeed"> How fast to fade out. (multiplier) </param>
        /// <paran name="destroyOnDone"> When done fading out, destroy this object? </param>
        public void FadeOutMusic (float fadeSpeed, bool destroyOnDone) {
            StopCoroutine ("FadeMusic");
            StartCoroutine (FadeMusic (0, fadeSpeed, destroyOnDone));
        }

        IEnumerator FadeMusic (float volumeGoal, float fadeSpeed, bool destroyOnDone) {
            while (source.volume != volumeGoal) {
                source.volume = Mathf.MoveTowards (source.volume, volumeGoal, Numbers.DeltaTime () * fadeSpeed);
                yield return new WaitForFixedUpdate ();
            }

            if (destroyOnDone)
                Destroy (gameObject);
        }

        #endregion

    }

}