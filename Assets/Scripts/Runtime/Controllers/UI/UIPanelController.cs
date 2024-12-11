using System;
using System.Collections.Generic;
using Runtime.Enums;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Controllers.UI
{
    public class UIPanelController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Fields

        [SerializeField] private List<Transform> layers = new List<Transform>();

        #endregion

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion

        #region Event Methods

        private void SubscribeEvents()
        {
            CoreUISignals.Instance.onOpenPanel += OnOpenPanel;
            CoreUISignals.Instance.onClosePanel += OnClosePanel;
            CoreUISignals.Instance.onCloseAllPanels += OnCloseAllPanel;
        }

        [Button("Open Panel")]
        private void OnOpenPanel(UIPanelTypes panalType, int value)
        {
            OnClosePanel(value);
            Instantiate(Resources.Load<GameObject>($"Screens/{panalType}Panel"),
                layers[value]);
        }

        [Button("Close Panel")]
        private void OnClosePanel(int value)
        {
            if (layers[value].childCount <= 0) return;
#if UNITY_EDITOR
            DestroyImmediate(layers[value].GetChild(0).gameObject); //Destroy Directly in Editor
#else
            Destroy(layers[value].GetChild(0).gameObject);
#endif
        }

        [Button("Close All Panel")]
        private void OnCloseAllPanel()
        {
            foreach (var layerTemp in layers)
            {
                if (layerTemp.childCount <= 0) return;
#if UNITY_EDITOR
                DestroyImmediate(layerTemp.GetChild(0).gameObject); //Destroy Directly in Editor
#else
            Destroy(layers[value].GetChild(0).gameObject);
#endif
            }
        }

        private void UnSubscribeEvents()
        {
            CoreUISignals.Instance.onOpenPanel -= OnOpenPanel;
            CoreUISignals.Instance.onClosePanel -= OnClosePanel;
            CoreUISignals.Instance.onCloseAllPanels -= OnCloseAllPanel;
        }

        #endregion
    }
}