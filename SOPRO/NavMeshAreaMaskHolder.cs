﻿using UnityEngine;
namespace SOPRO
{
    /// <summary>
    /// Class that holds a navmesh mask value and calculates its hash value at editor time
    /// </summary>
    [CreateAssetMenu(fileName = "NavMeshAreaMask", menuName = "SOPRO/Unity/NavMeshAreaMask")]
    public class NavMeshAreaMaskHolder : ScriptableObject
    {
        /// <summary>
        /// Calculated area mask id at editor time
        /// </summary>
        public int AreaMaskId { get { return this.areaMaskId; } }
        [Tooltip("NavMesh area mask id")]
        [SerializeField]
        private int areaMaskId;
        /// <summary>
        /// Converts to calculated area id value
        /// </summary>
        /// <param name="area">area to convert</param>
        public static implicit operator int(NavMeshAreaMaskHolder area)
        {
            return area.areaMaskId;
        }
    }
}