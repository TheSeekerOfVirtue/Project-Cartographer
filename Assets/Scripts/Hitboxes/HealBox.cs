///<summary>
/// Script by The Seeker Of Virtue
/// When a hitbox for a certain team (usually the player team) touches this, that hitbox will heal.
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carto.Hitboxes {
    [RequireComponent (typeof (Rigidbody))]
    public class HealBox : MonoBehaviour {

        [Tooltip ("The Hitbox team that gets effected by this healbox")]
        public HitboxTeam team = HitboxTeam.Player;
        [Tooltip ("How much the target gets healed")]
        public byte healAmount = 1;
        [Tooltip ("How many times does this healbox activate before it gets destroyed")]
        public byte healsBeforeDestroy = 1;

        public void CheckDestroy () {
            if (healsBeforeDestroy < 1)
                Destroy (gameObject);
        }

        ///<summary>
        /// Initialize healbox
        ///</summary>
        public void Init () {
            GetComponent<Collider> ().isTrigger = true;
            Rigidbody rb = GetComponent<Rigidbody> ();
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            gameObject.layer = 7;
        }
    }
}