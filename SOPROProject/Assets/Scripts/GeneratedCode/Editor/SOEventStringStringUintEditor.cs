using UnityEngine;
using UnityEditor;
    /// <summary>
    /// Class that modifies SOEvent inspector view
    /// </summary>
    [CustomEditor(typeof(SOEventStringStringUint))]
    public class SOEventStringStringUintEditor : UnityEditor.Editor
    {
			public string Value0 = default(string);
			public string Value1 = default(string);
			public uint Value2 = default(uint);
	        /// <summary>
        /// Method that modifies SOEvent inspector view
        /// </summary>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            SOEventStringStringUint e = target as SOEventStringStringUint;
            if (GUILayout.Button("Raise"))
                e.Raise(Value0 ,Value1 ,Value2);
        }
    }
