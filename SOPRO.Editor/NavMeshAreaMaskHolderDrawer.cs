using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
namespace SOPRO.Editor
{
    /// <summary>
    /// Class that draws a custom inspector for NavMeshAreaMaskHolder
    /// </summary>
    [CustomEditor(typeof(NavMeshAreaMaskHolder))]
    public class NavMeshAreaMaskHolderDrawer : UnityEditor.Editor
    {
        private NavMeshAreaMaskHolder mask;
        private string result;
        void OnEnable()
        {
            this.mask = target as NavMeshAreaMaskHolder;
            result = string.Empty;
        }
        /// <summary>
        /// Custom inspector draw
        /// </summary>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            result = EditorGUILayout.TextField("NavMesh Area name", result);

            int id = 1 << NavMesh.GetAreaFromName(result);

            EditorGUILayout.LabelField("Name to Id value: " + id);

            if (id > 0)
            {
                using (new GUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("Add new Layer"))
                        this.mask.AreaMaskId = this.mask | id;

                    GUILayout.FlexibleSpace();

                    if (GUILayout.Button("Remove Layer"))
                        this.mask.AreaMaskId = this.mask & ~id;
                }
            }
            else
            {
                EditorGUILayout.HelpBox("The inserted area name is not a valid name", MessageType.Warning);
            }
        }
    }
}