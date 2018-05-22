﻿using UnityEngine;
using SOPRO.Variables;
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
        public ReferenceString LayerName;
        /// <summary>
        /// Layer index calculated at Editor time
        /// </summary>
        public int LayerIndex;
        void OnValidate()
        {
            LayerIndex = LayerMask.NameToLayer(LayerName.Value);
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
            return layer.LayerName.Value;
        }
    }
}