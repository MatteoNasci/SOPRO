using UnityEngine;
using SOPRO.Variables;
namespace SOPRO
{
    /// <summary>
    /// Class that holds a shader property name and calculates its id value at load time
    /// </summary>
    [CreateAssetMenu(fileName = "ShaderProperty", menuName = "SOPRO/Unity/ShaderProperty")]
    public class ShaderPropertyHolder : ScriptableObject
    {
        /// <summary>
        /// Property Name
        /// </summary>
        public ReferenceString PropertyName;
        /// <summary>
        /// Calculated property id at load time
        /// </summary>
        public int PropertyID;
        void OnEnable()
        {
            PropertyID = Shader.PropertyToID(PropertyName.Value);
        }
        /// <summary>
        /// Converts to calculated property id value
        /// </summary>
        /// <param name="prop">property to convert</param>
        public static implicit operator int(ShaderPropertyHolder prop)
        {
            return prop.PropertyID;
        }
        /// <summary>
        /// Converts to property name value
        /// </summary>
        /// <param name="prop">property to convert</param>
        public static implicit operator string(ShaderPropertyHolder prop)
        {
            return prop.PropertyName.Value;
        }
    }
}