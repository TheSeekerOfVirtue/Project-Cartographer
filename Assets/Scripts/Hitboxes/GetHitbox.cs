///<summary>
/// Script by The Seeker Of Virtue
/// Container of HP. Can get hit.
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Carto.Hitboxes {

    public class GetHitbox : MonoBehaviour {
        [Tooltip ("Health Points!")]
        public byte hp;
        [Tooltip ("Max HP. Might be needed for some scripts. For instance the player should not be able to heal more then the max HP.")]
        public byte maxhp;
        [Tooltip ("How many seconds you will be unhittable, after getting hit")]
        public float invincibleTime = 0;
        public HitboxTeam myTeam = HitboxTeam.Enemy;
        public delegate void OnHitDel ();
        public OnHitDel onHitEv;
        public delegate void OnDeathDel (HitboxDeathType deathType);
        public OnDeathDel onDeathEv;
        public delegate void OnHeal ();
        public OnHeal onHealEv;
        [SerializeField] UnityEvent onHitUnityEv;
        [SerializeField] UnityEvent onDeathUnityEv;

        void OnTriggerEnter (Collider other) {

            if (CheckHitbox (other))
                return;

            if (CheckHealbox (other))
                return;
        }

        ///<returns> if false, the ontrigger code will continue to check for other hitbox types </returns>
        bool CheckHitbox (Collider other) {
            Hitbox hit = other.GetComponent<Hitbox> ();
            if (hit == null)
                return false;

            if (hit.damageType != HitboxDamageType.OnCollision)
                return true;
            if (RegisterHit (hit) == false)
                return true;

            OnHit (hit);
            return true;
        }

        ///<returns> if false, the ontrigger code will continue to check for other hitbox types </returns>
        bool CheckHealbox (Collider other) {
            // check healbox
            HealBox healBox = other.GetComponent<HealBox> ();
            if (healBox == null)
                return false;

            if (healBox.team != myTeam)
                return true;

            if (hp >= maxhp)
                return true;

            hp += healBox.healAmount;
            hp = (byte) Mathf.Min (hp, maxhp);
            healBox.healsBeforeDestroy--;
            healBox.CheckDestroy ();
            onHealEv?.Invoke ();

            return true;

        }

        void OnTriggerStay (Collider other) {
            if (IsInvoking ("NoHitInv"))
                return;

            Hitbox hit = other.GetComponent<Hitbox> ();
            if (hit == null)
                return;
            if (hit.damageType != HitboxDamageType.DamageOverTime)
                return;
            if (RegisterHit (hit) == false)
                return;

            Invoke ("NoHitInv", hit.hitRate);
            OnHit (hit);

        }

        void NoHitInv () { }

        ///<summary>
        /// Make this hitbox get hit
        ///</summary>
        ///<param name="hitbox"> The hitbox that hit me </param>
        public void OnHit (Hitbox hitbox) {
            hp = (byte) Mathf.Max (hp - hitbox.damage, 0);

            if (hp > 0) {
                invTime = invincibleTime;
                StartCoroutine (ResetTImer ());
                onHitEv?.Invoke ();
                onHitUnityEv.Invoke();
            } else
            {
                onDeathEv?.Invoke (hitbox.deathType);
                onDeathUnityEv.Invoke();
            }
            hitbox.onHit?.Invoke (this);
        }

        public float invTime = 0;
        ///<summary>
        /// Make this hitbox get hit
        ///</summary>
        ///<param name="dmg"> how much damage I get </param>
        ///<param name="knockbackType"> How to get knocked back </param>
        ///<param name="deathType"> If the player dies from this hit, what kind of event needs to activate? </param>
        public void OnHit (byte dmg, HitboxDeathType deathType) {
            hp = (byte) Mathf.Max (hp - dmg, 0);

            if (hp > 0) {
                invTime = invincibleTime;
                StartCoroutine (ResetTImer ());
                onHitEv?.Invoke ();
                onHitUnityEv.Invoke();
            } else
            {
                onDeathEv?.Invoke (deathType);
                onDeathUnityEv.Invoke();
            }
        }

        IEnumerator ResetTImer () {
            while (invTime != 0) {
                if (!CustomUpdateManager.instance.isPaused) {
                    invTime = Mathf.MoveTowards (invTime, 0, Numbers.DeltaTime ());
                    yield return new WaitForFixedUpdate ();
                }
            }
            Collider collider = GetComponent<Collider> ();
            if (collider.enabled == true) {
                collider.enabled = false;
                collider.enabled = true;
            }
        }

        void OnEnable () {
            //bug fix. When the object gets disabled, the inv timer doesn't count anymore
            if (invTime == 0)
                return;
            StopCoroutine ("ResetTimer");
            StartCoroutine (ResetTImer ());
        }

        bool RegisterHit (Hitbox hit) {

            //do not hit if you are invincible
            if (invTime > 0)
                return false;

            //do hit if the hitboxes comparisons are good
            switch (myTeam) {
                case HitboxTeam.HitAny:
                    return true;
                case HitboxTeam.Player:
                    return (hit.team != HitboxTeam.Player);
                case HitboxTeam.Enemy:
                    return (hit.team != HitboxTeam.Enemy);

            }

            //do not hit in other scenarios (in other words, scenarios I didn't think of)
            return false;
        }
        public static GetHitbox playerHitbox;

        void Awake () {
            if (myTeam == HitboxTeam.Player)
                playerHitbox = this;
        }
        void OnDestroy () {
            if (playerHitbox == this)
                playerHitbox = null;
        }
    }

}