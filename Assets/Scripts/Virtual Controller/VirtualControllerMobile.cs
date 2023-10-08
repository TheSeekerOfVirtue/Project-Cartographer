///<summary>
/// Script by The Seeker Of Virtue
///  Virtual Controller for Mobile
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carto {

    public class VirtualController_Mobile : VirtualController {

        protected override void UpdateInputValues () {
            SetInputs ();
        }

        protected override void UpdateInputValuesAlways () {

            //confirm
            foreach (var touch in Input.touches) {
                if (touch.phase == TouchPhase.Began) {
                    onConfirmAlways?.Invoke ();
                }
            }

        }

        void SetInputs () {

            //horizontal axis

            //vertical axis

            //shoot always

            //jump onclick

            //pause onclick
            if (controls.pauseInput) {
                onPauseEv?.Invoke ();
            }

            //confirm onclick

        }
    }
}