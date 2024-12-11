using Runtime.Keys;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class InputSignals : MonoBehaviour
    {
        public UnityAction<HorizontalInputParams> onInputDragged = delegate { };
        public UnityAction onFirstTimeTouchTaken = delegate { };
        public UnityAction onInputTaken = delegate { };
        public UnityAction onInputReleased = delegate { };
        public UnityAction onEnableInput = delegate { };
        public UnityAction onDisableInput = delegate { };
        #region Singelton

        public static InputSignals Instance;

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