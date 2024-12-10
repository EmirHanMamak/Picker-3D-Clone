using Sirenix.OdinInspector;
using UnityEngine;

namespace Commands.Level
{
    public class OnLevelDestroyerCommand
    {
        private Transform _levelHolder;
        public OnLevelDestroyerCommand(Transform levelHolder)
        {
            _levelHolder = levelHolder;
        }
        [Button]
        public void Execute()
        {
            if(_levelHolder.transform.childCount <= 0) return;
            Object.Destroy(_levelHolder.transform.GetChild(0).gameObject);
        }
    }
}