using UnityEngine;
using UnityEditor;
    /// <summary>
    /// Class that modifies SOEvent inspector view
    /// </summary>
    [CustomEditor(typeof(SOEventFloatIntStringByte))]
    public class SOEventFloatIntStringByteEditor : UnityEditor.Editor
    {
			public float Value0 = default(float);
			public int Value1 = default(int);
			public string Value2 = default(string);
			public byte Value3 = default(byte);
	        /// <summary>
        /// Method that modifies SOEvent inspector view
        /// </summary>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            SOEventFloatIntStringByte e = target as SOEventFloatIntStringByte;
            if (GUILayout.Button("Raise"))
                e.Raise(Value0 ,Value1 ,Value2 ,Value3);
        }
    }
