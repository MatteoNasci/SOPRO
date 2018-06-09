using System;
namespace SOPRO.Editor
{
    /// <summary>
    /// Enum representing the available modes for event gen code
    /// </summary>
    [Serializable]
    public enum EventGenerationMode
    {
        /// <summary>
        /// No code generated
        /// </summary>
        None = 0,
        /// <summary>
        /// Code for basic events will be generated
        /// </summary>
        BasicEvent = 1,
        /// <summary>
        /// Code for unity events will be generated
        /// </summary>
        UnityEvent = 1 << 1,
    }
}