using System.Collections.Generic;
using UnityEngine;
using System;
    /// <summary>
    /// Scriptable Object event
    /// </summary>
    [CreateAssetMenu(fileName = "Event", menuName = "SOPRO/Events/FloatIntStringByte")]
    [Serializable]
    public class SOEventFloatIntStringByte : ScriptableObject
    {
		#if UNITY_EDITOR
					public float DEBUG_Value0 = default(float);
				public int DEBUG_Value1 = default(int);
				public string DEBUG_Value2 = default(string);
				public byte DEBUG_Value3 = default(byte);
		#endif
	
        [SerializeField]
        private readonly List<SOEventFloatIntStringByteListener> listeners = new List<SOEventFloatIntStringByteListener>();

        /// <summary>
        /// Invokes all listeners of this event
        /// </summary>
        public void Raise(float Value0, int Value1, string Value2, byte Value3)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised(Value0, Value1, Value2, Value3);
        }
        /// <summary>
        /// Adds listener to the event
        /// </summary>
        /// <param name="listener">listener to add</param>
        internal void AddListener(SOEventFloatIntStringByteListener listener)
        {
            listeners.Add(listener);
        }
        /// <summary>
        /// Removes listener from the event
        /// </summary>
        /// <param name="listener">listener to remove</param>
        internal void RemoveListener(SOEventFloatIntStringByteListener listener)
        {
            listeners.Remove(listener);
        }
    }
