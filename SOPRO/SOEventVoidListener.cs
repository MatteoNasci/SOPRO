using System;
using UnityEngine;
namespace SOPRO
{
    /// <summary>
    /// Listener for Scriptable Object event
    /// </summary>
    [Serializable]
    public class SOEventVoidListener : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Event to register with.")]
        private SOEventVoid Event;
        [SerializeField]
        [Tooltip("Response to invoke when Event is raised.")]
        protected internal UnityEventWrapperVoid Actions;

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
        internal void OnEventRaised()
        {
            Actions.Invoke();
        }
    }
}