///<summary>
/// Script by The Seeker Of Virtue
/// Pauses game. Also does an event.
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carto {

    public class PauseInput : MonoBehaviour {

        public delegate void OnPauseDel ();
        public OnPauseDel onPause;
        public OnPauseDel onUnPause;
        public static PauseInput instance;

        void Awake () {
            instance = this;
            VirtualController.instance.onPauseEv += OnInput;
        }

        void OnDestroy () {
            instance = null;
            VirtualController.instance.onPauseEv -= OnInput;
            onPause = null;
            onUnPause = null;
        }

        void OnInput () {
            if (VirtualController.instance.controls.pauseInput) {
                CustomUpdateManager.instance.isPaused = !CustomUpdateManager.instance.isPaused;
                if (CustomUpdateManager.instance.isPaused)
                    OnPause ();
                else
                    OnUnPause ();
            }
        }

        void OnPause () {
            CustomUpdateManager.instance.isCameraPaused = true;
            onPause?.Invoke ();
        }

        void OnUnPause () {
            CustomUpdateManager.instance.isCameraPaused = false;
            onUnPause?.Invoke ();
        }
    }

}