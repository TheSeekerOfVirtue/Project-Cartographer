///<summary>
/// Script by The Seeker Of Virtue
///  Base for the virtual controller
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_ANDROID
#else
using XInputDotNetPure;
#endif
namespace Carto {

    public class VirtualController : MonoBehaviour {
        public static ControlType controlType;

        static VirtualController getterInstance;
        public static VirtualController instance {
            get {
                if (getterInstance == null) {

                    GameObject g = new GameObject ("Virtual Controller");

                    controlType = GetControlType ();

                    switch (controlType) {
                        case ControlType.Keyboard:
                            getterInstance = g.AddComponent<VirtualController_Keyboard> ();
                            break;
                        case ControlType.PS4:
                            getterInstance = g.AddComponent<VirtualController_PS4> ();
                            break;
                        case ControlType.XBOX:
#if UNITY_ANDROID
#else
                            getterInstance = g.AddComponent<VirtualController_XBOX> ();
#endif
                            break;
                        case ControlType.Mobile:
                            getterInstance = g.AddComponent<VirtualController_Mobile> ();
                            break;

                    }

                    DontDestroyOnLoad (getterInstance.gameObject);
                }

                return getterInstance;

            }
            set {

                if (value != instance)
                    Destroy (instance);

                getterInstance = value;
            }
        }

        public delegate void OnInput ();
        public OnInput onConfirmEv;
        public OnInput onPauseEv;
        public OnInput onConfirmAlways;

        public struct Inputs {
            public float horInput;
            public float vertInput;
            public bool shootInput;
            public bool jumpInput;
            public bool jumpInputHold;
            public bool pauseInput;
            public bool onConfirm;
            public bool bashInput;
        }

        public bool receiveInput = true;

        public Inputs controls;

        void Update () {
            if (receiveInput)
                UpdateInputs ();
            else
                SetNullValues ();

            UpdateInputValuesAlways ();
        }

        static void UpdateInputs () {
            instance.UpdateInputValues ();
        }

        protected virtual void UpdateInputValues () {
            SetNullValues ();
        }

        protected virtual void UpdateInputValuesAlways () {

        }

        void SetNullValues () {

            Inputs newInputs = new Inputs ();

            newInputs.horInput = 0;
            newInputs.vertInput = 0;
            newInputs.shootInput = false;
            newInputs.jumpInput = false;
            newInputs.jumpInputHold = false;
            newInputs.pauseInput = false;
            newInputs.bashInput = false;

            controls = newInputs;
        }

        static ControlType GetControlType () {

#if UNITY_ANDROID
            return ControlType.Mobile;
#endif

#if UNITY_ANDROID
#else
            //check xbox
            if (GamePad.GetState (PlayerIndex.One).IsConnected == true)
                return ControlType.XBOX;
#endif

            //check ps4

            string[] names = Input.GetJoystickNames ();

            for (int x = 0; x < names.Length; x++) {
                if (names[x].Length == 19) {
                    return ControlType.PS4;
                }
            }

            return ControlType.Keyboard;
        }

    }

}