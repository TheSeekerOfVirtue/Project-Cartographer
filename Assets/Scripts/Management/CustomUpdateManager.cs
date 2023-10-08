///<summary>
/// Script by The Seeker Of Virtue
///  Does the actual updating. Calls events for the updates.
/// All updates are called in FixedUpdate, 60 times per second, regardless of the actual framerate. 
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carto {

    public class CustomUpdateManager : MonoBehaviour {
        static CustomUpdateManager getterInstance;
        public static CustomUpdateManager instance {
            get {
                if (getterInstance == null && Application.isPlaying) {
                    getterInstance = new GameObject ("Update Manager").AddComponent<CustomUpdateManager> ();
                    DontDestroyOnLoad (getterInstance.gameObject);
                }
                return getterInstance;
            }
            set {
                getterInstance = value;
            }
        }

        public delegate void UpdateEV ();
        public UpdateEV alwaysUpdate;
        public UpdateEV normalUpdate;
        public UpdateEV cameraUpdate;
        public UpdateEV playerUpdate;
        public UpdateEV enemyUpdate;
        public UpdateEV pausedUpdate;

        public bool isPaused;
        public bool isCameraPaused;
        public bool isPlayerPaused;
        public bool isEnemyPaused;

        [RuntimeInitializeOnLoadMethod]
        static void InitPhysics () {
            Application.targetFrameRate = 60;
            Time.fixedDeltaTime = 1f / 60f;
        }

        //done for debugging.
        void OnApplicationQuit () {

            try {
                Destroy (GameObject.Find ("Update Manager"));
            } catch { }
        }

        void FixedUpdate () {
            Always ();
        }

        void Always () {
            alwaysUpdate?.Invoke ();

            if (!isPaused)
                Normal ();
            else
                Paused ();

            if (!isCameraPaused)
                Cam ();

        }

        void Normal () {
            normalUpdate?.Invoke ();

            if (!isPlayerPaused)
                Player ();

            if (!isEnemyPaused)
                Enemy ();
        }

        void Player () {
            playerUpdate?.Invoke ();
        }

        void Enemy () {
            enemyUpdate?.Invoke ();
        }

        void Paused () {
            pausedUpdate?.Invoke ();
        }

        void Cam () {
            cameraUpdate?.Invoke ();
        }
    }
}