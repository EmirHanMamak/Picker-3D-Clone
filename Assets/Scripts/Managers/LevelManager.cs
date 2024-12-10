using UnityEngine;
using Data.UnityObjects;
using Data.ValueObjects;
using Commands.Level;
using Signals;
using Sirenix.OdinInspector;

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
        private short _currentLevel;

        #endregion

        #endregion

        #region Unity Methods

        private void Awake()
        {
            InitCommands();
            //_levelData = GetLevelData();
            // _currentLevel = GetActiveScene();
        }

        private void Start()
        {
            CoreGameSignals.Instance.onLevelInitialize?.Invoke((byte)(_currentLevel % totalLevelCount));
            //TO-DO UI Signal
        }

        private void OnEnable() => SubscribeEvents();


        private void OnDisable() => UnSubscribeEvents();

        #endregion

        private void InitCommands()
        {
            _levelDestroyerCommand = new OnLevelDestroyerCommand(levelHolder);
            _levelLoaderCommand = new OnLevelLoaderCommand(levelHolder);
        }

        private LevelData GetLevelData()
        {
            return Resources.Load<CD_Level>("Data/CD_Level").Levels[_currentLevel];
        }

        private byte GetActiveScene()
        {
            return (byte)_currentLevel; //To-Do Implement EasySave
        }

        #region Events

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize += _levelLoaderCommand.Execute;
            CoreGameSignals.Instance.onClearActiveLevel += _levelDestroyerCommand.Execute;

            CoreGameSignals.Instance.onGetLevelValue += OnGetLevelValue;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;
        }

        private void OnNextLevel()
        {
            _currentLevel++;
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onRestartLevel?.Invoke();
            CoreGameSignals.Instance.onLevelInitialize?.Invoke((byte)(_currentLevel % totalLevelCount));
        }

        private void OnRestartLevel()
        {
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onRestartLevel?.Invoke();
            CoreGameSignals.Instance.onLevelInitialize?.Invoke((byte)(_currentLevel % totalLevelCount));
        }

        private byte OnGetLevelValue() => (byte)_currentLevel;

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize -= _levelLoaderCommand.Execute;
            CoreGameSignals.Instance.onClearActiveLevel -= _levelDestroyerCommand.Execute;

            CoreGameSignals.Instance.onGetLevelValue -= OnGetLevelValue;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
            CoreGameSignals.Instance.onRestartLevel -= OnRestartLevel;
        }

        #endregion

        #region Odin

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

        #endregion
    }
}