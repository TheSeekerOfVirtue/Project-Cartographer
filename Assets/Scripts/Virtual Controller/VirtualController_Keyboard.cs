///<summary>
/// Script by The Seeker Of Virtue
///  Virtual Controller for the Keyboard input
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carto {

    public class VirtualController_Keyboard : VirtualController {
        protected override void UpdateInputValues () {
            SetInputs ();
        }

        protected override void UpdateInputValuesAlways () {
            //confirm
            if (Input.GetKeyDown (KeyCode.Mouse0))
            {
                onConfirmAlways?.Invoke ();
            }
        }

        void SetInputs () {

            //horizontal
            controls.horInput = 0;
            if (Input.GetKey (KeyCode.A) == true)
                controls.horInput -= 1;
            if (Input.GetKey (KeyCode.D) == true)
                controls.horInput += 1;

            //vertical
            controls.vertInput = 0;
            if (Input.GetKey (KeyCode.S) == true)
                controls.vertInput -= 1;
            if (Input.GetKey (KeyCode.W) == true)
                controls.vertInput += 1;

            //shoot
            controls.shootInput = Input.GetKey (KeyCode.Mouse0);

            //jump
            controls.jumpInput = Input.GetKeyDown (KeyCode.Space);
            controls.jumpInputHold = Input.GetKey (KeyCode.Space);

            //pause
            controls.pauseInput = Input.GetKeyDown (KeyCode.Escape);
            if (controls.pauseInput) {
                onPauseEv?.Invoke ();
            }

            //confirm
            controls.onConfirm = Input.GetKeyDown (KeyCode.Mouse0);
            if (controls.onConfirm) {
                onConfirmEv?.Invoke ();
            }

            //bash
            controls.bashInput = Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.LeftShift);

        }
    }
}