///<summary>
/// Script by The Seeker Of Virtue
/// Standard events
///</summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Carto.Load;

namespace Carto {

    public class GameRules_Standard : GameRules {
        
        public override void OnGameOver()
        {
            base.OnGameOver();
            LoadScene.Reload();
        }

    }
}