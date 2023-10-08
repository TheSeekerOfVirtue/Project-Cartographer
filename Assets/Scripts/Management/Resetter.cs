///<summary>
/// Script by The Seeker Of Virtue
/// Resets certain values at start, such as unpausing the game.
///</summary>

using System.Collections;
using System.Collections.Generic;
using Carto.Sound;
using UnityEngine;

namespace Carto {

    public class Resetter : MonoBehaviour {
        void Awake () {
            ResetAll ();
        }

        IEnumerator DelayedSetVolume () {
            // Must wait at least 1 frame, or the changes will not apply for whatever reason.
            // I assume this is related to Unity's Audio Mixer systems.
            yield return new WaitForFixedUpdate ();
            SoundManager.SetAllVolume ();
        }

        void ResetAll () {
            UnPause ();
            StartCoroutine (DelayedSetVolume ());
            VirtualController.instance.receiveInput = true;
        }

        void UnPause () {
            Numbers.TimeScale = 1;
            CustomUpdateManager.instance.isPaused = false;
            CustomUpdateManager.instance.isEnemyPaused = false;
            CustomUpdateManager.instance.isCameraPaused = false;
            CustomUpdateManager.instance.isPlayerPaused = false;
        }

    }

}