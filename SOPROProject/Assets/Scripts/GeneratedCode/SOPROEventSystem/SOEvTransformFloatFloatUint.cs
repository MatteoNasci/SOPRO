using System.Collections.Generic;
using UnityEngine;
using System;
    /// <summary>
    /// Scriptable Object event
    /// </summary>
    [CreateAssetMenu(fileName = "Event", menuName = "SOPRO/Events/TransformFloatFloatUint")]
    [Serializable]
    public class SOEvTransformFloatFloatUint : ScriptableObject
    {
		#if UNITY_EDITOR
					public Transform DEBUG_Transform_0 = default(Transform);
				public float DEBUG_float_1 = default(float);
				public float DEBUG_float_2 = default(float);
				public uint DEBUG_uint_3 = default(uint);
		#endif
	
        [SerializeField]
        private readonly List<SOEvTransformFloatFloatUintListener> listeners = new List<SOEvTransformFloatFloatUintListener>();

        /// <summary>
        /// Invokes all listeners of this event
        /// </summary>
        public void Raise(Transform Value0, float Value1, float Value2, uint Value3)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised(Value0, Value1, Value2, Value3);
        }
        /// <summary>
        /// Adds listener to the event
        /// </summary>
        /// <param name="listener">listener to add</param>
        internal void AddListener(SOEvTransformFloatFloatUintListener listener)
        {
            listeners.Add(listener);
        }
        /// <summary>
        /// Removes listener from the event
        /// </summary>
        /// <param name="listener">listener to remove</param>
        internal void RemoveListener(SOEvTransformFloatFloatUintListener listener)
        {
            listeners.Remove(listener);
        }
    }
