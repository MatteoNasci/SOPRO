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
        public LayerMask LayerMask = new LayerMask();
        /// <summary>
        /// Layer mask index calculated at Editor time
        /// </summary>
        public int LayerMaskIndex;
        void OnValidate()
        {
            OnEnable();
        }
        void OnEnable()
        {
            LayerMaskIndex = LayerMask.value;
        }
        /// <summary>
        /// Converts to calculated layer index value
        /// </summary>
        /// <param name="layer">layer to convert</param>
        public static implicit operator int(LayerMaskHolder layer)
        {
            return layer.LayerMaskIndex;
        }
        /// <summary>
        /// Converts to layer mask
        /// </summary>
        /// <param name="layer">layer to convert</param>
        public static implicit operator LayerMask(LayerMaskHolder layer)
        {
            return layer.LayerMask;
        }
    }
}