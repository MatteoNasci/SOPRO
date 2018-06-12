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
        public string LayerName { get { return this.layerName; } }
        [Tooltip("Layer name")]
        [SerializeField]
        private string layerName = string.Empty;
        /// <summary>
        /// Layer index calculated at Editor time
        /// </summary>
        public int LayerIndex { get { return this.layerIndex; } }
        [Tooltip("Layer id")]
        [SerializeField]
        private int layerIndex;
        void OnValidate()
        {
            layerIndex = LayerMask.NameToLayer(layerName);
        }
        /// <summary>
        /// Converts to calculated layer index value
        /// </summary>
        /// <param name="layer">layer to convert</param>
        public static implicit operator int(LayerHolder layer)
        {
            return layer.layerIndex;
        }
        /// <summary>
        /// Converts to layer name value
        /// </summary>
        /// <param name="layer">layer to convert</param>
        public static implicit operator string(LayerHolder layer)
        {
            return layer.layerName;
        }
    }
}