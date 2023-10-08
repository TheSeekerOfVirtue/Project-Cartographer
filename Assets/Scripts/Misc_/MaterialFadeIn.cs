using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script by The Seeker Of Virtue. Sets the opacity of a material over time.
/// </summary>

[HelpURL ("https://www.youtube.com/watch?v=DPEvF8l9LDM")]
public class MaterialFadeIn : MonoBehaviour {

    [Tooltip ("Which renderer's material do you want to change")]
    [SerializeField] new Renderer renderer;
    [Tooltip ("Rate at which the fade happens")]
    [SerializeField] float lerpSpeed;
    Coroutine curCoroutine;
    [Tooltip ("What the value is at the start of the game")]
    [SerializeField] float startOpacity;
    [Tooltip ("What shader value to set")]
    public string valueToSet = "_Opacity";

    ///<summary>
    /// Starts the fade coroutine. Cancels the coroutine of one is already running.
    ///</summary>
    /// <param name="goal"> The value you want to fade to. </param>
    public void DoFade (float goal) {
        if (curCoroutine != null)
            StopCoroutine (curCoroutine);
        cur = GetOpacity ();
        curCoroutine = StartCoroutine (OpacityTrans (goal, GetOpacity ()));
    }

    float cur;
    IEnumerator OpacityTrans (float goal, float initial) {
        cur = initial;
        while (cur != goal) {
            cur = Mathf.MoveTowards (cur, goal, Numbers.DeltaTime() * lerpSpeed);
            SetOpactiy (Mathf.Lerp (0, 1, cur));
            yield return new WaitForFixedUpdate ();
        }
    }

    void Start () {
        SetOpactiy (startOpacity);
    }

    /// <summary>
    /// Set the current state of the value you want to fade
    ///</summary>
    /// <param name="amount"> Sets the value you want to change of the shader to this. </param>
    public void SetOpactiy (float amount) => renderer.material.SetFloat (valueToSet, amount);

    /// <summary>
    /// Get the current state of the value you want to fade
    ///</summary>
    /// <returns>Returns current value you want to fade.</returns>
    float GetOpacity () => renderer.material.GetFloat (valueToSet);

    /// <summary>
    /// Initializes this script via code
    ///</summary>
    /// <param name="rend"> Which renderer's material do you want to change  </param>
    /// <param name="lerpSpd"> Rate at which the fade happens  </param>
    /// <param name="startOpac"> What the value is after init  </param>
    /// <param name="valToSet"> What shader value to set  </param>
    /// <param name="doFade"> Should the fade happen now?  </param>
    /// <param name="doFadeGoal"> If you want to fade now, to what value do you want to fade?  </param>
    public void Init (string valToSet = "_Opacity",Renderer rend = null, float lerpSpd = 1, float startOpac = 1, bool doFade = true, float doFadeGoal = 0) {
        if (rend == null)
            rend = GetComponent<Renderer> ();
        renderer = rend;
        lerpSpeed = lerpSpd;
        startOpacity = startOpac;
        valueToSet = valToSet;
        if (doFade)
            DoFade (doFadeGoal);
    }
}