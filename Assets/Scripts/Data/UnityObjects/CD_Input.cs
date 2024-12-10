using Data.ValueObjects;
using UnityEngine;

namespace Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Input", menuName = "Picker3D-Clone/CD_Input", order = 52)]
    public class CD_Input : ScriptableObject
    {
        public InputData Data;
    }
}