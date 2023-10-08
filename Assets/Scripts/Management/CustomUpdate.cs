///<summary>
/// Script by The Seeker Of Virtue
/// Subscribes OnUpdate to the correct update type.
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carto {
    public class CustomUpdate : MonoBehaviour {

        [Tooltip ("Which kind of update to perform. OnUpdate is called by this update type.")]
        public UpdateType updateType;

        protected virtual void OnUpdate () {

        }

        protected virtual void Awake () {
            SubscribeToUpdateType ();
        }

        protected virtual void OnDestroy () {
            UnSubscribeToUpdateType ();
        }

        void SubscribeToUpdateType () {
            switch (updateType) {
                case UpdateType.Always:
                    CustomUpdateManager.instance.alwaysUpdate += OnUpdate;
                    break;
                case UpdateType.Camera:
                    CustomUpdateManager.instance.cameraUpdate += OnUpdate;
                    break;
                case UpdateType.Enemy:
                    CustomUpdateManager.instance.enemyUpdate += OnUpdate;
                    break;
                case UpdateType.Normal:
                    CustomUpdateManager.instance.normalUpdate += OnUpdate;
                    break;
                case UpdateType.OnPaused:
                    CustomUpdateManager.instance.pausedUpdate += OnUpdate;
                    break;
                case UpdateType.Player:
                    CustomUpdateManager.instance.playerUpdate += OnUpdate;
                    break;
            }
        }

        void UnSubscribeToUpdateType () {
            switch (updateType) {
                case UpdateType.Always:
                    CustomUpdateManager.instance.alwaysUpdate -= OnUpdate;
                    break;
                case UpdateType.Camera:
                    CustomUpdateManager.instance.cameraUpdate -= OnUpdate;
                    break;
                case UpdateType.Enemy:
                    CustomUpdateManager.instance.enemyUpdate -= OnUpdate;
                    break;
                case UpdateType.Normal:
                    CustomUpdateManager.instance.normalUpdate -= OnUpdate;
                    break;
                case UpdateType.OnPaused:
                    CustomUpdateManager.instance.pausedUpdate -= OnUpdate;
                    break;
                case UpdateType.Player:
                    CustomUpdateManager.instance.playerUpdate -= OnUpdate;
                    break;
            }
        }

    }
}