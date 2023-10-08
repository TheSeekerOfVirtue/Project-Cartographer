///<summary>
/// Script by The Seeker Of Virtue
/// Holds information of a hitbox
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Carto.Hitboxes;

namespace Carto.Hitboxes {

    [RequireComponent (typeof (Rigidbody))]
    public class Hitbox : MonoBehaviour {

        [Tooltip ("Amount of damage that this hitbox does.")]
        public byte damage;
        [Tooltip ("Who this hitbox belongs to.")]
        public HitboxTeam team = HitboxTeam.Enemy;
        [Tooltip ("What kind of hit this is.")]
        [HideInInspector] public HitboxDamageType damageType = HitboxDamageType.OnCollision;
        [Tooltip ("How many times does this hitbox activate per second?")]
        [HideInInspector] public float hitRate = 1;
        [Tooltip ("If the player dies from hitting this hitbox, what kind of death animation will he play?")]
        [HideInInspector] public HitboxDeathType deathType = HitboxDeathType.Normal;
        public delegate void OnHit (GetHitbox getHitbox);
        public OnHit onHit;

        ///<summary>
        /// Initialize hitbox
        ///</summary>
        public void Init () {
            GetComponent<Collider> ().isTrigger = true;
            Rigidbody rb = GetComponent<Rigidbody> ();
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            gameObject.layer = 7;
            if (deathType == HitboxDeathType.FallOffStage)
                gameObject.tag = "IgnoreDownwardKick";
        }

    }

}