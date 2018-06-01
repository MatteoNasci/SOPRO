using UnityEngine;
using UnityEditor;
namespace SOPRO.Events.Editor
{
    /// <summary>
    /// Class that modifies SOEvent inspector view
    /// </summary>
    [CustomEditor(typeof(SOEvTransform))]
    public class SOEvTransformDrawer : UnityEditor.Editor
    {
        /// <summary>
        /// Method that modifies SOEvent inspector view
        /// </summary>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            SOEvTransform e = target as SOEvTransform;
            if (GUILayout.Button("Raise"))
                e.Raise(e.DEBUG_Transform_0);
        }
    }
}
