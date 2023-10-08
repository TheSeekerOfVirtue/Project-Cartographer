///<summary>
/// Script by The Seeker Of Virtue
/// Creates after images
///</summary>

using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Carto {

    public class AfterImages : MonoBehaviour {
        [SerializeField] float waitTime;
        [SerializeField] float modelDestroyTime;
        [SerializeField] Material fadingMat;
        [SerializeField] float minWalkDistance = 0;
        [HideInInspector] public bool isEnabled = false;

        [Header ("Model Applying")]
        [SerializeField] string fadeMatValue = "_Opacity";
        [SerializeField] Component[] dontCopy;
        [SerializeField] Animator myAnimator;
        [SerializeField] Renderer[] myRends;
        [SerializeField] SpriteRenderer[] mySprites;
        [SerializeField] GameObject[] toDestroy;
        async void Start () {
            await Task.Delay ((int) (waitTime * 1000));
            lastPos = transform.position;
            while (true) {
                await Task.Delay ((int) (waitTime * 1000));
                if (isEnabled)
                    DoAfterImage ();
            }
        }

        void OnEnable () {
            lastPos = transform.position;
        }

        Vector3 lastPos;
        void DoAfterImage () {
            try {
                if (!isActiveAndEnabled)
                    return;
                if (Vector3.Distance (transform.position, lastPos) < minWalkDistance) {
                    lastPos = transform.position;
                    return;
                }

                lastPos = transform.position;
                Transform t = Instantiate (gameObject, transform.position, transform.rotation).transform;
                Destroy (t.gameObject, modelDestroyTime);
                t.GetComponent<AfterImages> ().DisableComponents ();
            } catch { }

        }

        public void DisableComponents () {
            transform.localScale *= 0.99f;
            foreach (var item in dontCopy) {
                DestroyImmediate (item);
            }
            if (myAnimator != null)
                myAnimator.enabled = false;

            foreach (var item in myRends) {
                item.sharedMaterial = fadingMat;
                item.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                item.gameObject.layer = 0;
                MaterialFadeIn fadeEffect = item.gameObject.AddComponent<MaterialFadeIn> ();
                fadeEffect.Init (fadeMatValue, null, 2);
            }

            foreach (var item in mySprites) {
                //   item.sharedMaterial = fadingMat;
                // MaterialFadeIn fadeEffect = item.gameObject.AddComponent<MaterialFadeIn> ();
                // fadeEffect.Init (fadeMatValue,null,2);
                SpriteFade fade = item.gameObject.AddComponent<SpriteFade> ();
                fade.fadeSpeed = 2;
                fade.goalColor = Color.clear;
                item.transform.position += Vector3.forward * 0.1f;
            }

            foreach (var item in toDestroy)
                Destroy (item);

            Destroy (this);
        }

    }
}