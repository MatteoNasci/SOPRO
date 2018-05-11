using System;
using UnityEngine;
    /// <summary>
    /// Listener for Scriptable Object event
    /// </summary>
    [Serializable]
    public class SOEventStringStringUintListener : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Event to register with.")]
        private SOEventStringStringUint Event;
        [SerializeField]
        [Tooltip("Response to invoke when Event is raised.")]
        private UnityEventStringStringUintWrapper Actions;

        /// <summary>
        /// Adds listener to event
        /// </summary>
        protected virtual void OnEnable()
        {
            Event.AddListener(this);
        }
        /// <summary>
        /// Removes listener from event
        /// </summary>
        protected virtual void OnDisable()
        {
            Event.RemoveListener(this);
        }
        /// <summary>
        /// Invokes unity event
        /// </summary>
        internal void OnEventRaised(string Value0, string Value1, uint Value2)
        {
            Actions.Invoke(Value0, Value1, Value2);
        }
    }
