using UnityEngine;
using SOPRO.Variables;
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
        public ReferenceString PropertyName;
        /// <summary>
        /// Calculated property hash at load time
        /// </summary>
        public int PropertyHash;
        void OnEnable()
        {
            PropertyHash = Animator.StringToHash(PropertyName.Value);
        }
        /// <summary>
        /// Converts to calculated property hash value
        /// </summary>
        /// <param name="prop">property to convert</param>
        public static implicit operator int(AnimatorPropertyHolder prop)
        {
            return prop.PropertyHash;
        }
        /// <summary>
        /// Converts to property name value
        /// </summary>
        /// <param name="prop">property to convert</param>
        public static implicit operator string(AnimatorPropertyHolder prop)
        {
            return prop.PropertyName.Value;
        }
    }
}