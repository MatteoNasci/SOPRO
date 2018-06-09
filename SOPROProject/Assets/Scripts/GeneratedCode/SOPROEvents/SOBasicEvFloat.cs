using System.Collections.Generic;
using UnityEngine;
using System;
using SOPRO;
namespace SOPRO 
{
    /// <summary>
    /// Basic Scriptable Object event
    /// </summary>
    [CreateAssetMenu(fileName = "BasicEvent", menuName = "SOPRO/BasicEvents/Float")]
    [Serializable]
    public class SOBasicEvFloat : BaseSOEvFloat
    {
#if UNITY_EDITOR
        /// <summary>
        /// Description of the event, available only in UNITY_EDITOR
        /// </summary>
        [Multiline]
		[SerializeField]
        private string DEBUG_DeveloperDescription = "";
#endif

		#if UNITY_EDITOR
		 			    /// <summary>
				/// Debug field for inspector view, available only in UNITY_EDITOR
				/// </summary>
				public float DEBUG_float_0 = default(float);
		#endif
		        public delegate void SOBasicEvFloatDel(float Value0);
        public event SOBasicEvFloatDel Event;

        /// <summary>
        /// Invokes all listeners of this event
        /// </summary>
        public override void Raise(float Value0)
        {
			Event.Invoke(Value0);
        }
    }
}
