using System;
using UnityEngine;
namespace SOPRO
{
    /// <summary>
    /// Class that holds a reference to a value
    /// </summary>
    [Serializable]
    public class ReferenceVector2
    {
        /// <summary>
        /// Determines whenever reference should use a given value or a Variable value
        /// </summary>
        public bool UseConstant;
        /// <summary>
        /// Variable currently stored
        /// </summary>
        public SOVariableVector2 Variable;
        /// <summary>
        /// Current value
        /// </summary>
        public Vector2 Value
        {
            get { return UseConstant ? constantValue : Variable.Value; }
			set
			{
				if (UseConstant)
				    constantValue = value;
				else
				    Variable.Value = value;
			}
        }

        [SerializeField]
        private Vector2 constantValue;
        /// <summary>
        /// Construct a reference with default state
        /// </summary>
        public ReferenceVector2()
        {
        }
        /// <summary>
        /// Construct reference given an initial value
        /// </summary>
        /// <param name="value"></param>
        public ReferenceVector2(Vector2 value)
        {
            UseConstant = true;
            constantValue = value;
        }
        /// <summary>
        /// Conversion between reference to underlying value
        /// </summary>
        /// <param name="reference">reference to convert</param>
        public static implicit operator Vector2(ReferenceVector2 reference)
        {
            return reference.Value;
        }
    }
}
