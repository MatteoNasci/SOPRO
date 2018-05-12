using System;
using System.Collections.Generic;
using UnityEngine;
namespace SOPRO
{
    /// <summary>
    /// Scriptable Object event
    /// </summary>
    [CreateAssetMenu(fileName = "Event", menuName = "SOPRO/Events/Void")]
    [Serializable]
    public class SOEventVoid : ScriptableObject
    {
        [SerializeField]
        private readonly List<SOEventVoidListener> listeners = new List<SOEventVoidListener>();

        /// <summary>
        /// Invokes all listeners of this event
        /// </summary>
        public void Raise()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised();
        }
        /// <summary>
        /// Adds listener to the event
        /// </summary>
        /// <param name="listener">listener to add</param>
        internal void AddListener(SOEventVoidListener listener)
        {
            listeners.Add(listener);
        }
        /// <summary>
        /// Removes listener from the event
        /// </summary>
        /// <param name="listener">listener to remove</param>
        internal void RemoveListener(SOEventVoidListener listener)
        {
            listeners.Remove(listener);
        }
    }
}