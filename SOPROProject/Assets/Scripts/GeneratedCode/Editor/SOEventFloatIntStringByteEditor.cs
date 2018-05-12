using UnityEngine;
using UnityEditor;
    /// <summary>
    /// Class that modifies SOEvent inspector view
    /// </summary>
    [CustomEditor(typeof(SOEventFloatIntStringByte))]
    public class SOEventFloatIntStringByteEditor : UnityEditor.Editor
    {
        /// <summary>
        /// Method that modifies SOEvent inspector view
        /// </summary>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            SOEventFloatIntStringByte e = target as SOEventFloatIntStringByte;
            if (GUILayout.Button("Raise"))
                e.Raise(e.DEBUG_Value0 ,e.DEBUG_Value1 ,e.DEBUG_Value2 ,e.DEBUG_Value3);
        }
    }
