using UnityEngine;
namespace SOPRO
{
    /// <summary>
    /// Class that holds a Layer name and calculates its index at Editor time
    /// </summary>
    [CreateAssetMenu(fileName = "Layer", menuName = "SOPRO/Unity/Layer")]
    public class LayerHolder : ScriptableObject
    {
        /// <summary>
        /// Layer name
        /// </summary>
        [Tooltip("Layer name")]
        public string LayerName = string.Empty;
        /// <summary>
        /// Layer index calculated at Editor time
        /// </summary>
        [Tooltip("Layer id")]
        public int LayerIndex;
        void OnValidate()
        {
            OnEnable();
        }
        void OnEnable()
        {
            LayerIndex = LayerMask.NameToLayer(LayerName);
        }
        /// <summary>
        /// Converts to calculated layer index value
        /// </summary>
        /// <param name="layer">layer to convert</param>
        public static implicit operator int(LayerHolder layer)
        {
            return layer.LayerIndex;
        }
        /// <summary>
        /// Converts to layer name value
        /// </summary>
        /// <param name="layer">layer to convert</param>
        public static implicit operator string(LayerHolder layer)
        {
            return layer.LayerName;
        }
    }
}