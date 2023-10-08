using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Numbers : MonoBehaviour {
    ///<summary> The delta time without any timescale issues </summary>
    public static float DeltaTime () => (1f / Application.targetFrameRate) * TimeScale;
    public static float TimeScale = 1;
}