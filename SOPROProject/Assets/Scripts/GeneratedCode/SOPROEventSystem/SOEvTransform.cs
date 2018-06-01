using System.Collections.Generic;
using UnityEngine;
using System;
namespace SOPRO.Events 
{
    /// <summary>
    /// Scriptable Object event
    /// </summary>
    [CreateAssetMenu(fileName = "Event", menuName = "SOPRO/Events/Transform")]
    [Serializable]
    public class SOEvTransform : ScriptableObject
    {
		#if UNITY_EDITOR
					public Transform DEBUG_Transform_0 = default(Transform);
		#endif
	
        [SerializeField]
        private readonly List<SOEvTransformListener> listeners = new List<SOEvTransformListener>();

        /// <summary>
        /// Invokes all listeners of this event
        /// </summary>
        public void Raise(Transform Value0)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised(Value0);
        }
        /// <summary>
        /// Adds listener to the event
        /// </summary>
        /// <param name="listener">listener to add</param>
        internal void AddListener(SOEvTransformListener listener)
        {
            listeners.Add(listener);
        }
        /// <summary>
        /// Removes listener from the event
        /// </summary>
        /// <param name="listener">listener to remove</param>
        internal void RemoveListener(SOEvTransformListener listener)
        {
            listeners.Remove(listener);
        }
    }
}
