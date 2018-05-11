using System.Collections.Generic;
using UnityEngine;
using System;
    /// <summary>
    /// Scriptable Object event
    /// </summary>
    [CreateAssetMenu(fileName = "Event", menuName = "SOPRO/Events/StringStringUint")]
    [Serializable]
    public class SOEventStringStringUint : ScriptableObject
    {
        [SerializeField]
        private readonly List<SOEventStringStringUintListener> listeners = new List<SOEventStringStringUintListener>();

        /// <summary>
        /// Invokes all listeners of this event
        /// </summary>
        public void Raise(string Value0, string Value1, uint Value2)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised(Value0, Value1, Value2);
        }
        /// <summary>
        /// Adds listener to the event
        /// </summary>
        /// <param name="listener">listener to add</param>
        internal void AddListener(SOEventStringStringUintListener listener)
        {
            listeners.Add(listener);
        }
        /// <summary>
        /// Removes listener from the event
        /// </summary>
        /// <param name="listener">listener to remove</param>
        internal void RemoveListener(SOEventStringStringUintListener listener)
        {
            listeners.Remove(listener);
        }
    }
