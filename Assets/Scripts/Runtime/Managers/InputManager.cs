using System.Collections.Generic;
using Runtime.Data.UnityObjects;
using Runtime.Data.ValueObjects;
using Runtime.Signals;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private InputData _inputData;
        private float3 _moveVector;
        private Vector2? _mousePosition;
        private float _currentVelocity;
        private bool _isAvailableForTouch, _isFirstTimeTouch, _isTouching;

        #endregion

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _inputData = GetInputData();
        }

        private void Update()
        {
            if (!_isAvailableForTouch) return;
            if (Input.GetMouseButtonUp(0) && !IsPointerOverUIElement())
            {
                _isTouching = false;
                InputSignals.Instance.onInputReleased?.Invoke();
                Debug.LogWarning("Executed ------> InputSignals onInputReleased");
            }

            if (Input.GetMouseButtonDown(0) && !IsPointerOverUIElement())
            {
                _isTouching = true;
                InputSignals.Instance.onInputTaken?.Invoke();
                Debug.LogWarning("Executed ------> InputSignals onInputTaken");
                if (!_isFirstTimeTouch)
                {
                    _isAvailableForTouch = true;
                    InputSignals.Instance.onFirstTimeTouchTaken?.Invoke();
                    Debug.LogWarning("Executed ------> InputSignals onFirstTimeTouchTaken");
                }

                _mousePosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0) && !IsPointerOverUIElement())
            {
                if (_isTouching)
                {
                    if (_mousePosition != null)
                    {
                        Vector2 mouseDeltaPos = (Vector2)Input.mousePosition - _mousePosition.Value;
                        if (mouseDeltaPos.x > _inputData.HorizontalInputSpeed)
                        {
                            _moveVector.x = _inputData.HorizontalInputSpeed / 10f * mouseDeltaPos.x;
                        }
                        else if (mouseDeltaPos.x < _inputData.HorizontalInputSpeed)
                        {
                            _moveVector.x = -_inputData.HorizontalInputSpeed / 10f * mouseDeltaPos.x;
                        }
                        else
                        {
                            _moveVector.x = Mathf.SmoothDamp(-_moveVector.x, 0f, ref _currentVelocity,
                                _inputData.ClampSpeed);
                        }
                    }
                }
            }
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion

        #region Methods

        private InputData GetInputData()
        {
            return Resources.Load<CD_Input>("Data/CD_Input").Data;
        }

        private bool IsPointerOverUIElement()
        {
            var eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            return results.Count > 0;
        }

        #endregion

        #region Event Methods

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onRestartLevel += OnReset;
            InputSignals.Instance.onEnableInput += OnEnableInput;
            InputSignals.Instance.onDisableInput += OnDisableInput;
        }


        private void OnEnableInput()
        {
            _isAvailableForTouch = true;
        }

        private void OnDisableInput()
        {
            _isAvailableForTouch = false;
        }

        private void OnReset()
        {
            _isAvailableForTouch = false;
            _isFirstTimeTouch = false;
            _isTouching = false;
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onRestartLevel -= OnReset;
            InputSignals.Instance.onEnableInput -= OnEnableInput;
            InputSignals.Instance.onDisableInput -= OnDisableInput;
        }

        #endregion
    }
}