using UnityEngine;
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
        [Tooltip("Shader property name")]
        public string PropertyName = string.Empty;
        /// <summary>
        /// Calculated property id at load time
        /// </summary>
        [Tooltip("Shader property hash id")]
        public int PropertyID;
        void OnEnable()
        {
            PropertyID = Shader.PropertyToID(PropertyName);
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
            return prop.PropertyName;
        }
    }
}