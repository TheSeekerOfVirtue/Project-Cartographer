///<summary>
/// Script by The Seeker Of Virtue
///  Spawns in a footstep sound effect. This is called from the animation itself.
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Carto.Sound {

    public class FootstepSound : MonoBehaviour {
        [Tooltip ("The clip of the footstep soundeffect.")]
        public AudioClip footStepClip;
        [Tooltip ("How loud the sound will be.")]
        [Range (0, 1)]
        public float volume = 1;
        [Tooltip ("Pitch of the sound. A random number between these two numbers.")]
        public Vector2 pitch = Vector2.one;
        void Footstep () {
            SoundManager.SFX (footStepClip, volume, Random.Range (pitch.x, pitch.y));
        }
    }
}