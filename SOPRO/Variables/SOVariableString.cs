using UnityEngine;
namespace SOPRO.Variables
{
    /// <summary>
    /// SO that holds a variable
    /// </summary>
    [CreateAssetMenu(fileName = "SOVariableString", menuName = "SOPRO/Variables/String")]
    public class SOVariableString : ScriptableObject
    {
#if UNITY_EDITOR
        /// <summary>
        /// Description of the variable, available only in UNITY_EDITOR
        /// </summary>
        public string DEBUG_DeveloperDescription { get { return developerDescription; } }
        [Multiline]
        [SerializeField]
        private string developerDescription = "";
#endif
        /// <summary>
        /// Value stored in the variable
        /// </summary>
        public string Value;

        /// <summary>
        /// Sets value to given value
        /// </summary>
        /// <param name="value">new value</param>
        public void SetValue(string value)
        {
            this.Value = value;
        }
        /// <summary>
        /// Sets value to given value
        /// </summary>
        /// <param name="value">new value</param>
        public void SetValue(SOVariableString value)
        {
            this.Value = value.Value;
        }
        /// <summary>
        /// Increases value of given amount
        /// </summary>
        /// <param name="amount">increase amount</param>
        public void ApplyChange(string amount)
        {
            this.Value += amount;
        }
        /// <summary>
        /// Increases value of given amount
        /// </summary>
        /// <param name="amount">increase amount</param>
        public void ApplyChange(SOVariableString amount)
        {
            this.Value += amount.Value;
        }
        /// <summary>
        /// Conversion between variable to underlying value
        /// </summary>
        /// <param name="reference">variable to convert</param>
        public static implicit operator string(SOVariableString variable)
        {
            return variable.Value;
        }
    }
}