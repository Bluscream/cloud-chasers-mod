using MelonLoader;
using System;
using System.Collections.Generic;
using StormHackers;
using UnityEngine;

namespace StormHackers {
    internal partial class MapTweaks {
        internal static readonly Dictionary<string, Vector3> mapPositions = new Dictionary<string, Vector3>() {
            { "corwan", new Vector3((float)-1533.423, (float)40.3712, (float)-1838.434) },
            { "wakota", new Vector3((float)-1404.233, (float)17.855, (float)2016.92) },
            { "byron", new Vector3((float)2592.014, (float)25.1206, (float)-2448.835) },
            { "middle", new Vector3((float)-33.8434, (float)15.105, (float)35.0814) },
        };
        #region Map Methods
        internal static Vector3 GetMapPosition(string mapName) {
            if (mapPositions.ContainsKey(mapName)) return mapPositions[mapName];
            return Vector3.zero;
        }
        #endregion
    }
}