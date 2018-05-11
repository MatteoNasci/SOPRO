using System;
using UnityEngine;
    /// <summary>
    /// Listener for Scriptable Object event
    /// </summary>
    [Serializable]
    public class SOEventFloatIntStringByteListener : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Event to register with.")]
        private SOEventFloatIntStringByte Event;
        [SerializeField]
        [Tooltip("Response to invoke when Event is raised.")]
        private UnityEventFloatIntStringByteWrapper Actions;

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
        internal void OnEventRaised(float Value0, int Value1, string Value2, byte Value3)
        {
            Actions.Invoke(Value0, Value1, Value2, Value3);
        }
    }
