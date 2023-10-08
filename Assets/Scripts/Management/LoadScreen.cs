///<summary>
/// Script by The Seeker Of Virtue
/// Manages loading a scene
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Carto {

    public class LoadScreen : MonoBehaviour {
        [Tooltip ("The back. It fades in over time")]
        [SerializeField] Image background;
        [SerializeField] float fadeInSpeed = 1;
        [SerializeField] float fadeOutSpeed = 1;
        delegate void OnFadeEnd ();
        OnFadeEnd onFadeEnd;
        void Start () {
            DontDestroyOnLoad (gameObject);
            StartCoroutine (FadeBackground (Color.black, fadeInSpeed));
        }

        ///<summary>
        /// Fades out, then destroys self
        ///</summary>        
        public void DestroyMe () {
            StopCoroutine ("FadeBackground");
            onFadeEnd += OnEndDestroy;
            StartCoroutine (FadeBackground (Color.clear, fadeOutSpeed));
        }

        void OnEndDestroy () {
            Destroy (gameObject);
            onFadeEnd -= OnEndDestroy;
        }

        void OnDestroy () {
            onFadeEnd -= OnEndDestroy;
        }

        IEnumerator FadeBackground (Color goalColor, float speed) {
            Color startColor = background.color;
            float cur = 0;
            while (cur != 1) {
                cur = Mathf.MoveTowards (cur, 1, Numbers.DeltaTime ());
                background.color = Color.Lerp (startColor, goalColor, cur);
                yield return new WaitForFixedUpdate ();
            }
            onFadeEnd?.Invoke ();
        }
    }

}