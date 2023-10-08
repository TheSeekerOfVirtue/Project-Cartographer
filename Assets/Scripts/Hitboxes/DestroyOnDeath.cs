///<summary>
/// Script by The Seeker Of Virtue
/// Destroys object when dying
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Carto.Hitboxes {

    public class DestroyOnDeath : MonoBehaviour {
        GetHitbox getterGetHitbox;
        GetHitbox getHitbox {
            get {
                if (getterGetHitbox == null)
                    getterGetHitbox = GetComponent<GetHitbox> ();
                return getterGetHitbox;
            }
            set {
                getterGetHitbox = value;
            }
        }

        [SerializeField] GameObject toDestroy;

        //init event
        void Awake () {
            getHitbox.onDeathEv += OnDeath;
        }

        //Do actual destroy
        void OnDeath (HitboxDeathType deathType) {
            Destroy ((toDestroy == null) ? gameObject : toDestroy);
        }

        //un-init event to prevent memory leaks
        void OnDestroy () {
            getHitbox.onDeathEv -= OnDeath;
        }
    }

}