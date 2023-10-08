///<summary>
/// Script by The Seeker Of Virtue
/// Handles what happens on certain events, like game over
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carto {

    public class GameRules : MonoBehaviour {

        static GameRules getterInstance;
        public static GameRules instance {
            get {
                if (getterInstance == null) {
                    getterInstance = SpawnStandardGameRules ();
                }
                return getterInstance;
            }
            set {
                if (getterInstance != null)
                    Destroy (getterInstance.gameObject);
                getterInstance = value;
            }
        }

        static GameRules_Standard SpawnStandardGameRules () {
            GameObject g = new GameObject ("GameRules");
            return g.AddComponent<GameRules_Standard> ();
        }

        #region Events
        public delegate void OnEvent ();

        //game over

        public OnEvent onGameOver;
        ///<summary>
        /// Activates on Game Over. Invokes onGameOver (delegate) as well.
        ///</summary>
        public virtual void OnGameOver () {
            onGameOver?.Invoke ();
        }

        #endregion
    }

}