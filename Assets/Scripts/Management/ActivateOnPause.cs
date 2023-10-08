///<summary>
/// Script by The Seeker Of Virtue
/// Activate or deactive when pausing
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carto {

    public class ActivateOnPause : MonoBehaviour {
        [SerializeField] bool activeOnPause = true;
        void Start () {
            if (PauseInput.instance == null)
                return;

            PauseInput.instance.onPause += OnPause;
            PauseInput.instance.onUnPause += OnUnPause;

            if (CustomUpdateManager.instance.isPaused)
                OnPause ();
            else
                OnUnPause ();
        }

        void OnPause () {
            gameObject.SetActive (activeOnPause);
        }

        void OnUnPause () {
            gameObject.SetActive (!activeOnPause);
        }
    }

}