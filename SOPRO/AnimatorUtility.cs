using UnityEngine;
namespace SOPRO
{
    /// <summary>
    /// Utility class for Unity animator
    /// </summary>
    public static class AnimatorUtility
    {
        /// <summary>
        /// Sets an animator trigger
        /// </summary>
        /// <param name="animator">animator</param>
        /// <param name="property">trigger property</param>
        public static void SetTrigger(Animator animator, AnimatorPropertyHolder property)
        {
            animator.SetTrigger((int)property);
        }
        /// <summary>
        /// Sets an animator boolean value
        /// </summary>
        /// <param name="animator">animator</param>
        /// <param name="property">boolean property</param>
        /// <param name="value">value to set</param>
        public static void SetBoolean(Animator animator, AnimatorPropertyHolder property, bool value)
        {
            animator.SetBool((int)property, value);
        }
        /// <summary>
        /// Sets an animator float value
        /// </summary>
        /// <param name="animator">animator</param>
        /// <param name="property">float property</param>
        /// <param name="value">value to set</param>
        public static void SetFloat(Animator animator, AnimatorPropertyHolder property, float value)
        {
            animator.SetFloat((int)property, value);
        }
        /// <summary>
        /// Sets an animator integer value
        /// </summary>
        /// <param name="animator">animator</param>
        /// <param name="property">integer property</param>
        /// <param name="value">value to set</param>
        public static void SetInteger(Animator animator, AnimatorPropertyHolder property, int value)
        {
            animator.SetInteger((int)property, value);
        }
    }
}