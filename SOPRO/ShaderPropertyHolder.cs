﻿using UnityEngine;
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
        public string PropertyName { get { return this.propertyName; } }
        [Tooltip("Shader property name")]
        [SerializeField]
        private string propertyName = string.Empty;
        /// <summary>
        /// Calculated property id at load time
        /// </summary>
        public int PropertyHash { get { return this.propertyHash; } }
        [Tooltip("Shader property hash id")]
        [SerializeField]
        private int propertyHash;
        void OnEnable()
        {
            propertyHash = Shader.PropertyToID(propertyName);
        }
        /// <summary>
        /// Converts to calculated property id value
        /// </summary>
        /// <param name="prop">property to convert</param>
        public static implicit operator int(ShaderPropertyHolder prop)
        {
            return prop.propertyHash;
        }
        /// <summary>
        /// Converts to property name value
        /// </summary>
        /// <param name="prop">property to convert</param>
        public static implicit operator string(ShaderPropertyHolder prop)
        {
            return prop.propertyName;
        }
    }
}