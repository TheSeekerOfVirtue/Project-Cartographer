///<summary>
/// Script by The Seeker Of Virtue
/// Rotates the attached transform over time
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carto {

    public class AutoMove : CustomUpdate {
        public Vector3 vector3;
        protected override void OnUpdate () {
            transform.position += transform.TransformDirection (vector3 * Numbers.DeltaTime ());
        }
    }

}