using UnityEngine;
using UnityEngine.AI;
namespace SOPRO
{
    /// <summary>
    /// Class that holds a navmesh area name and calculates its hash value at load time
    /// </summary>
    [CreateAssetMenu(fileName = "NavMeshArea", menuName = "SOPRO/Unity/NavMeshArea")]
    public class NavMeshAreaHolder : ScriptableObject
    {
        /// <summary>
        /// Area Name
        /// </summary>
        public string AreaName { get { return this.areaName; } }
        [Tooltip("NavMesh area name")]
        [SerializeField]
        private string areaName = string.Empty;
        /// <summary>
        /// Calculated area id at load time
        /// </summary>
        public int AreaId { get { return this.areaId; } }
        [Tooltip("NavMesh area id")]
        [SerializeField]
        private int areaId;
        void OnValidate()
        {
            areaId = 1 << NavMesh.GetAreaFromName(areaName);
        }
        /// <summary>
        /// Converts to calculated area id value
        /// </summary>
        /// <param name="area">area to convert</param>
        public static implicit operator int(NavMeshAreaHolder area)
        {
            return area.areaId;
        }
        /// <summary>
        /// Converts to area name value
        /// </summary>
        /// <param name="area">area to convert</param>
        public static implicit operator string(NavMeshAreaHolder area)
        {
            return area.areaName;
        }
    }
}