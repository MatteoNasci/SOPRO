using UnityEngine;
using UnityEditor;
    /// <summary>
    /// Class that modifies SOEvent inspector view
    /// </summary>
    [CustomEditor(typeof(SOEvTransformFloatFloatUint))]
    public class SOEvTransformFloatFloatUintDrawer : UnityEditor.Editor
    {
        /// <summary>
        /// Method that modifies SOEvent inspector view
        /// </summary>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            SOEvTransformFloatFloatUint e = target as SOEvTransformFloatFloatUint;
            if (GUILayout.Button("Raise"))
                e.Raise(e.DEBUG_Transform_0 ,e.DEBUG_float_1 ,e.DEBUG_float_2 ,e.DEBUG_uint_3);
        }
    }
