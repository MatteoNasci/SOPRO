using UnityEngine;
using UnityEngine.AI;
namespace SOPRO
{
    /// <summary>
    /// Class that holds an animator property name and calculates its hash value at load time
    /// </summary>
    [CreateAssetMenu(fileName = "NavMeshArea", menuName = "SOPRO/Unity/NavMeshArea")]
    public class NavMeshAreaHolder : ScriptableObject
    {
        /// <summary>
        /// Area Name
        /// </summary>
        public string AreaName = string.Empty;
        /// <summary>
        /// Calculated area id at load time
        /// </summary>
        public int AreaId;
        void OnEnable()
        {
            AreaId = 1 << NavMesh.GetAreaFromName(AreaName);
        }
        void OnValidate()
        {
            OnEnable();
        }
        /// <summary>
        /// Converts to calculated area id value
        /// </summary>
        /// <param name="area">area to convert</param>
        public static implicit operator int(NavMeshAreaHolder area)
        {
            return area.AreaId;
        }
        /// <summary>
        /// Converts to area name value
        /// </summary>
        /// <param name="area">area to convert</param>
        public static implicit operator string(NavMeshAreaHolder area)
        {
            return area.AreaName;
        }
    }
}