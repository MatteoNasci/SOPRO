using System;
using System.Collections.Generic;
using UnityEngine;
namespace SOPRO
{
	/// <summary>
    /// A class used to represent a shared container of objects
    /// </summary>
    [Serializable]
	[CreateAssetMenu(fileName = "Container", menuName = "SOPRO/Containers/SOArraylongContainer")]
    public class SOArraylongContainer : ScriptableObject
    {
        /// <summary>
        /// List of elements stored
        /// </summary>
        public long[] Elements = new long[0];
		        /// <summary>
        /// Get/Set element at the given index
        /// </summary>
        /// <param name="i">index</param>
        /// <returns>value stored</returns>
        public long this[int i]
        {
            get { return Elements[i]; }
            set { Elements[i] = value; }
        }
		    }
}
