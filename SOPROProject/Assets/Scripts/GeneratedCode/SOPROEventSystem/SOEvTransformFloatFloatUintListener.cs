using System;
using UnityEngine;
    /// <summary>
    /// Listener for Scriptable Object event
    /// </summary>
    [Serializable]
    public class SOEvTransformFloatFloatUintListener : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Event to register with.")]
        private SOEvTransformFloatFloatUint Event;
        [SerializeField]
        [Tooltip("Response to invoke when Event is raised.")]
        private UnEvTransformFloatFloatUint Actions;

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
        internal void OnEventRaised(Transform Value0, float Value1, float Value2, uint Value3)
        {
            Actions.Invoke(Value0, Value1, Value2, Value3);
        }
    }
