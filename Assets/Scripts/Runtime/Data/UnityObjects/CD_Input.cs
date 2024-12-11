using Runtime.Data.ValueObjects;
using UnityEngine;

namespace Runtime.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Input", menuName = "Picker3D-Clone/CD_Input", order = 52)]
    public class CD_Input : ScriptableObject
    {
        public InputData Data;
    }
}