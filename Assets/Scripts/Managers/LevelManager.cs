using System;
using Commands.Level;
using Data.ValueObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Self Variables

        #region Serilazed Variables

        [SerializeField] private Transform levelHolder;
        [SerializeField] private byte totalLevelCount;

        #endregion

        #region Private Variables

        private OnLevelLoaderCommand _levelLoaderCommand;
        private OnLevelDestroyerCommand _levelDestroyerCommand;
        
        private LevelData _levelData;
        private byte _currentLevel;

        #endregion

        #endregion

        private void Awake()
        {
            InitCommands();
            //_levelData = GetLevelData();
           // _currentLevel = GetActiveScene();
        }

        private void InitCommands()
        {
            _levelDestroyerCommand = new OnLevelDestroyerCommand(levelHolder);
            _levelLoaderCommand = new OnLevelLoaderCommand(levelHolder);
        }
        [Button]
        public void LevelLoader(byte levelIndex)
        {
            _levelLoaderCommand.Execute(levelIndex);
        }
        [Button]
        public void LevelDestroyer()
        {
            _levelDestroyerCommand.Execute();
        }
        private LevelData GetLevelData()
        {
            throw new NotImplementedException();
        }

        private byte GetActiveScene()
        {
            throw new NotImplementedException();
        }
    }
}