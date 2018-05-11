using System;
using UnityEngine.Events;
    /// <summary>
    /// Wrapper for a unity event, usefull in order to see generic unityevent-T- in inspector
    /// </summary>
    [Serializable]
    public class UnityEventFloatIntStringByteWrapper : UnityEvent<float, int, string, byte> { }
