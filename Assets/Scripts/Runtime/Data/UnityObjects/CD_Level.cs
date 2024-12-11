using System.Collections.Generic;
using Runtime.Data.ValueObjects;
using UnityEngine;

namespace Runtime.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Level", menuName = "Picker3D-Clone/CD_Level", order = 51)]
    public class CD_Level : ScriptableObject
    {
        public List<LevelData> Levels;
    }
}