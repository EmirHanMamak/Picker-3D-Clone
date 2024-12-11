using System;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class CoreGameSignals : MonoBehaviour
    {
        public UnityAction<byte> onLevelInitialize = delegate { };
        public Func<byte> onGetLevelValue = delegate { return 0; };
        public UnityAction onClearActiveLevel = delegate { };
        public UnityAction onNextLevel = delegate { };
        public UnityAction onRestartLevel = delegate { };

        #region Singelton

        public static CoreGameSignals Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        #endregion
    }
}