using UnityEngine;
namespace SOPRO
{
    /// <summary>
    /// Class that holds a set of layers and calculates its index at Editor time
    /// </summary>
    [CreateAssetMenu(fileName = "LayerMask", menuName = "SOPRO/Unity/LayerMask")]
    public class LayerMaskHolder : ScriptableObject
    {
        /// <summary>
        /// Layer mask
        /// </summary>
        public LayerMask LayerMask { get { return this.layerMask; } }
        [Tooltip("LayerMask value")]
        [SerializeField]
        private LayerMask layerMask = new LayerMask();
        /// <summary>
        /// Layer mask index calculated at Editor time
        /// </summary>
        public int LayerMaskIndex { get { return this.layerMaskIndex; } }
        [Tooltip("LayerMask id")]
        [SerializeField]
        private int layerMaskIndex;
        void OnValidate()
        {
            layerMaskIndex = layerMask.value;
        }
        /// <summary>
        /// Converts to calculated layer index value
        /// </summary>
        /// <param name="layer">layer to convert</param>
        public static implicit operator int(LayerMaskHolder layer)
        {
            return layer.layerMaskIndex;
        }
        /// <summary>
        /// Converts to layer mask
        /// </summary>
        /// <param name="layer">layer to convert</param>
        public static implicit operator LayerMask(LayerMaskHolder layer)
        {
            return layer.layerMask;
        }
    }
}