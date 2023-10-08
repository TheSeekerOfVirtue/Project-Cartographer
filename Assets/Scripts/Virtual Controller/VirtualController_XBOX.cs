#if UNITY_ANDROID
#else

///<summary>
/// Script by The Seeker Of Virtue
///  Virtual Controller for the XBOX input
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

namespace Carto {

    public class VirtualController_XBOX : VirtualController {

        bool wasHoldingConfirm = false;
        bool wasHoldingPause = false;
        protected override void UpdateInputValues () {
            SetInputs ();
        }

        protected override void UpdateInputValuesAlways () {

            GamePadState gamepad = GamePad.GetState (0);

            //confirm
            if ((gamepad.Buttons.A == ButtonState.Pressed) && wasHoldingConfirm == false) {
                onConfirmAlways?.Invoke ();
            }
            wasHoldingConfirm = (gamepad.Buttons.A == ButtonState.Pressed);
        }

        void SetInputs () {

            GamePadState gamepad = GamePad.GetState (0);

            //horizontal
            controls.horInput = 0;
            //DPad
            if (gamepad.DPad.Left == ButtonState.Pressed)
                controls.horInput -= 1;
            if (gamepad.DPad.Right == ButtonState.Pressed)
                controls.horInput += 1;
            //if DPad input did not register, use the thumbstick instead
            if (controls.horInput == 0 && Mathf.Abs (gamepad.ThumbSticks.Left.X) > 0.6f) {
                if (gamepad.ThumbSticks.Left.X > 0)
                    controls.horInput += 1;
                else
                    controls.horInput -= 1;
            }

            //vertical
            controls.vertInput = 0;
            //DPad
            if (gamepad.DPad.Down == ButtonState.Pressed)
                controls.vertInput -= 1;
            if (gamepad.DPad.Up == ButtonState.Pressed)
                controls.vertInput += 1;
            //if DPad input did not register, use the thumbstick instead
            if (controls.vertInput == 0 && Mathf.Abs (gamepad.ThumbSticks.Left.Y) > 0.6f) {
                if (gamepad.ThumbSticks.Left.Y > 0)
                    controls.vertInput += 1;
                else
                    controls.vertInput -= 1;
            }

            //shoot
            controls.shootInput = (gamepad.Buttons.X == ButtonState.Pressed);

            //jump
            controls.jumpInput = ((gamepad.Buttons.A == ButtonState.Pressed) && controls.jumpInputHold == false);
            controls.jumpInputHold = (gamepad.Buttons.A == ButtonState.Pressed);

            //pause
            controls.pauseInput = ((gamepad.Buttons.Start == ButtonState.Pressed) && wasHoldingPause == false);
            wasHoldingPause = (gamepad.Buttons.Start == ButtonState.Pressed);
            if (controls.pauseInput) {
                onPauseEv?.Invoke ();
            }

            //confirm
            controls.onConfirm = controls.jumpInput;
            if (controls.onConfirm) {
                onConfirmEv?.Invoke ();
            }

            //bash
            print ("xbox bash input was never assigned, fix this bug later if ya want");

        }
    }
}
#endif