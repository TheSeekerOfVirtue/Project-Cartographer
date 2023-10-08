///<summary>
/// Script by The Seeker Of Virtue
/// Rotates the attached transform over time
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carto {

    public class AutoRotate : CustomUpdate {

        [SerializeField] Vector3 vector3;
        protected override void OnUpdate () {
            transform.Rotate (vector3 * Numbers.DeltaTime ());
        }
    }

}