using UnityEngine;
namespace SOPRO
{
    /// <summary>
    /// Class that holds an animator property name and calculates its hash value at load time
    /// </summary>
    [CreateAssetMenu(fileName = "AnimatorProperty", menuName = "SOPRO/Unity/AnimatorProperty")]
    public class AnimatorPropertyHolder : ScriptableObject
    {
        /// <summary>
        /// Property Name
        /// </summary>
        public string PropertyName { get { return this.propertyName; } }
        [Tooltip("Animator property name")]
        [SerializeField]
        private string propertyName = string.Empty;
        /// <summary>
        /// Calculated property hash at load time
        /// </summary>
        public int PropertyHash { get { return this.propertyHash; } }
        [Tooltip("Animator property hash id")]
        [SerializeField]
        private int propertyHash;
        void OnEnable()
        {
            propertyHash = Animator.StringToHash(propertyName);
        }
        /// <summary>
        /// Converts to calculated property hash value
        /// </summary>
        /// <param name="prop">property to convert</param>
        public static implicit operator int(AnimatorPropertyHolder prop)
        {
            return prop.propertyHash;
        }
        /// <summary>
        /// Converts to property name value
        /// </summary>
        /// <param name="prop">property to convert</param>
        public static implicit operator string(AnimatorPropertyHolder prop)
        {
            return prop.propertyName;
        }
    }
}