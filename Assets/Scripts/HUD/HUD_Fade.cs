///<summary>
/// Script by The Seeker Of Virtue
/// Do fade in or out. Always activates when a new scene is loaded.
/// This script works statically.
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Carto {

    public class HUD_Fade : MonoBehaviour {

        #region Values

        static HUD_Fade getterFade;
        public static HUD_Fade fade {
            get {
                if (getterFade == null) {
                    InitGetterFade ();
                }
                return getterFade;
            }
            set {
                getterFade = value;
            }
        }

        static Image getterImage;
        static Image image {
            get {
                if (getterImage == null)
                    getterImage = fade.GetComponentInChildren<Image> ();
                return getterImage;
            }
            set { getterImage = value; }
        }

        #endregion

        #region Fading

        static void InitGetterFade () {

            //destroy old fade object
            if (getterFade != null && getterFade.gameObject != null)
                Destroy (getterFade.gameObject);

            //spawn new one
            GameObject g = Resources.Load ("Fade Canvas") as GameObject;
            getterFade = Instantiate (g, Vector3.zero, Quaternion.identity).GetComponent<HUD_Fade> ();
            DontDestroyOnLoad (getterFade.gameObject);
        }

        /// <summary>
        /// Fade to a color in x amount of time
        /// </summary>
        /// <param name="goalColor"> Color to fade to </param> 
        /// <param name="fadeSpeed"> How fast to fade (multiplier) </param> 
        public static void Fade (Color goalColor, float fadeSpeed) {
            fade.StopCoroutine ("FadeCoroutine");
            fade.StartCoroutine (FadeCoroutine (goalColor, fadeSpeed));
        }

        /// <summary>
        /// Fade to a color in x amount of time
        /// </summary>
        /// <param name="goalColor"> Color to fade to </param> 
        /// <param name="fadeSpeed"> How fast to fade (multiplier) </param> 
        /// <param name="startColor"> Set the color of the fade image to this at the start of the fade </param> 
        public static void Fade (Color goalColor, float fadeSpeed, Color startColor) {
            image.color = startColor;
            fade.StopCoroutine ("FadeCoroutine");
            fade.StartCoroutine (FadeCoroutine (goalColor, fadeSpeed));
        }

        static IEnumerator FadeCoroutine (Color goalColor, float fadeSpeed) {
            Color startColor = image.color;
            float cur = 0;
            while (image.color != goalColor) {
                cur = Mathf.MoveTowards (cur, 1, Numbers.DeltaTime () * 10 * fadeSpeed);
                image.color = Color.Lerp (startColor, goalColor, cur);
                yield return new WaitForFixedUpdate ();
            }

        }
        #endregion

        #region Initializing

        [RuntimeInitializeOnLoadMethod]
        static void InitFadeOnSceneLoad () {
            SceneManager.sceneLoaded += OnSceneLoaded;
            Fade (Color.clear, 0.1f, Color.black);
        }

        static void OnSceneLoaded (Scene scene, LoadSceneMode mode) {
            Fade (Color.clear, 0.1f, Color.black);
        }

        #endregion
    }

}