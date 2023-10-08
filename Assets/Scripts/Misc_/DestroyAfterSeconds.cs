///<summary>
/// Script by The Seeker Of Virtue
/// Destroy self after seconds
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Carto {

    public class DestroyAfterSeconds : CustomUpdate {
        [Tooltip ("Time until the object gets destroyed")]
        [SerializeField] float destroyTime = 1;
        [Tooltip ("Which objects to destroy when time runs out")]
        [SerializeField] GameObject[] toDestroy;
        protected override void OnUpdate () {
            base.OnUpdate ();
            Timer ();

        }
        void Timer () {
            destroyTime -= Numbers.DeltaTime ();
            if (destroyTime < 0)
                DoDestroy ();
        }

        void DoDestroy () {
            foreach (var obj in toDestroy) {
                Destroy (obj);
            }
            Destroy (this);
        }

        ///<summary>
        /// Initializes this
        ///</summary>
        ///<param name="upType"> The update type, to make sure it doesn't get destroyed when pausing </param>
        ///<param name="lifeTime"> How many seconds until the object gets destroyed </param>
        ///<param name="toDestroyObjects"> What objects to destroy </param>
        public void Init (UpdateType upType, float lifeTime, GameObject[] toDestoyObjects) {
            updateType = upType;
            destroyTime = lifeTime;
            toDestroy = toDestoyObjects;
        }

        ///<summary>
        /// Initializes this
        ///</summary>
        ///<param name="upType"> The update type, to make sure it doesn't get destroyed when pausing </param>
        ///<param name="lifeTime"> How many seconds until the object gets destroyed </param>
        ///<param name="toDestroyObjects"> What object to destroy </param>
        public void Init (UpdateType upType, float lifeTime, GameObject toDestoyObject) {
            updateType = upType;
            destroyTime = lifeTime;
            GameObject[] objects = new GameObject[1];
            objects[0] = toDestoyObject;
            toDestroy = objects;
        }
    }

}