using System;
using System.Collections.Generic;
using UnityEngine;
namespace SOPRO.Containers
{
	/// <summary>
    /// A class used to represent a shared container of objects
    /// </summary>
    [Serializable]
	[CreateAssetMenu(fileName = "Container", menuName = "SOPRO/Containers/SOArrayuintContainer")]
    public class SOArrayuintContainer : ScriptableObject
    {
        /// <summary>
        /// List of elements stored
        /// </summary>
        public uint[] Elements = new uint[20];
		        /// <summary>
        /// Get/Set element at the given index
        /// </summary>
        /// <param name="i">index</param>
        /// <returns>value stored</returns>
        public uint this[int i]
        {
            get { return Elements[i]; }
            set { Elements[i] = value; }
        }
		    }
}
