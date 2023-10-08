///<summary>
/// Script by The Seeker Of Virtue
/// Moves object up and down over time
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carto {

    public class SineMovement : CustomUpdate {
        float baseY;
        [Tooltip("Curve of the up and down movement.")]
        [SerializeField] AnimationCurve curve;
        [Tooltip("How fast to go over the curve. (Multiplier)")]

        [SerializeField] float speed = 1;
        [Tooltip("Multiplies the up and down movement.")]
        [SerializeField] float amount = 1;
        void Start () {
            baseY = transform.position.y;
        }

        protected override void OnUpdate () {
            base.OnUpdate ();
            transform.position = new Vector3 (transform.position.x, baseY + (curve.Evaluate (Mathf.Repeat (Time.time * speed, curve.keys[curve.length - 1].time)) * amount), transform.position.z);
        }
    }

}