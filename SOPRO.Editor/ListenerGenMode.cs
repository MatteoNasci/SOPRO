using System;
namespace SOPRO.Editor
{
    /// <summary>
    /// Describes how a listener will register/unregister to SOEvents
    /// </summary>
    [Serializable]
    public enum ListenerGenMode
    {
        /// <summary>
        /// Listener will not automatically register/unregister
        /// </summary>
        Default,
        /// <summary>
        /// Listener will register/unregister on Awake/OnDestroy
        /// </summary>
        AwakeDestroy,
        /// <summary>
        /// Listener will register/unregister on OnEnable/OnDisable
        /// </summary>
        OnEnableOnDisable
    }
}