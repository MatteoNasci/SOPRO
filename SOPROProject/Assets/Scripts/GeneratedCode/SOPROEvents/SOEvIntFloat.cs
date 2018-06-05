using System.Collections.Generic;
using UnityEngine;
using System;
namespace SOPRO 
{
    /// <summary>
    /// Scriptable Object event
    /// </summary>
    [CreateAssetMenu(fileName = "Event", menuName = "SOPRO/Events/IntFloat")]
    [Serializable]
    public class SOEvIntFloat : ScriptableObject
    {
				#if UNITY_EDITOR
		 				public int DEBUG_int_0 = default(int);
						public float DEBUG_float_1 = default(float);
				#endif
		        [SerializeField]
        private readonly List<SOEvIntFloatListener> listeners = new List<SOEvIntFloatListener>();

        /// <summary>
        /// Invokes all listeners of this event
        /// </summary>
        public void Raise(int Value0, float Value1)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised(Value0, Value1);
        }
        /// <summary>
        /// Adds listener to the event
        /// </summary>
        /// <param name="listener">listener to add</param>
        internal void AddListener(SOEvIntFloatListener listener)
        {
            listeners.Add(listener);
        }
        /// <summary>
        /// Removes listener from the event
        /// </summary>
        /// <param name="listener">listener to remove</param>
        internal void RemoveListener(SOEvIntFloatListener listener)
        {
            listeners.Remove(listener);
        }
    }
}
