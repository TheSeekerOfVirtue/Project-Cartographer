///<summary>
/// Script by The Seeker Of Virtue
///  Virtual Controller for the Keyboard input
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carto {

    public class VirtualController_PS4 : VirtualController {
        protected override void UpdateInputValues () {
            SetInputs ();
        }

        protected override void UpdateInputValuesAlways () {
            //confirm
            if (Input.GetButtonDown ("PS4_Confirm")) {
                onConfirmAlways?.Invoke ();
            }
        }

        void SetInputs () {

            //horizontal
            controls.horInput = 0;
            if (Mathf.Abs (Input.GetAxisRaw ("PS4_Hor")) > 0.6) {
                if (Input.GetAxisRaw ("PS4_Hor") > 0)
                    controls.horInput += 1;
                else
                    controls.horInput -= 1;
            }

            //vertical
            controls.vertInput = 0;
            if (Mathf.Abs (Input.GetAxisRaw ("PS4_Vert")) > 0.6) {
                if (Input.GetAxisRaw ("PS4_Vert") < 0)
                    controls.vertInput += 1;
                else
                    controls.vertInput -= 1;
            }

            //shoot
            controls.shootInput = (Input.GetAxis ("PS4_Shoot") != 0); //PS4_Jump

            //jump
            controls.jumpInput = Input.GetButtonDown ("PS4_Jump");
            controls.jumpInputHold = (Input.GetAxis ("PS4_Jump") != 0);

            //pause
            controls.pauseInput = Input.GetButtonDown ("PS4_Pause");
            if (controls.pauseInput) {
                onPauseEv?.Invoke ();
            }

            //confirm
            controls.onConfirm = Input.GetButtonDown ("PS4_Confirm");
            if (controls.onConfirm) {
                onConfirmEv?.Invoke ();
            }

             //bash
            //controls.bashInput = Input.GetButtonDown ("PS4_Bash");
            print("ps4 bash input was never assigned, fix this bug later if ya want");

        }
    }
}