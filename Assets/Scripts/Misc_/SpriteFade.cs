///<summary>
/// Script by The Seeker Of Virtue
/// Fades s sprite to a certain color
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Carto {

    public class SpriteFade : MonoBehaviour {
        SpriteRenderer rend;
        public Color goalColor = Color.clear;
        public float fadeSpeed;

        void Start () {
            rend = GetComponent<SpriteRenderer> ();
            StartCoroutine(DoFade());
        }

        IEnumerator DoFade () {
            Color startColor = rend.color;
            float cur = 0;
            while (rend.color != goalColor) {
                rend.color = Color.Lerp (startColor, goalColor, cur);
                cur = Mathf.MoveTowards (cur, 1, Numbers.DeltaTime () * fadeSpeed);
                yield return new WaitForFixedUpdate ();
            }
        }

    }
}