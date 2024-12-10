using System;
using UnityEngine;
namespace Data.ValueObjects
{
    [Serializable]
    public struct InputData
    {
        public Vector2 ClampValues;
        public float HorizontalInputSpeed;
        public float ClampSpeed;
    }
}