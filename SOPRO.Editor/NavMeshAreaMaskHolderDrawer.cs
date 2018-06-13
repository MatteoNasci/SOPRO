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
        private SerializedProperty areaId;
        private string result;
        void OnEnable()
        {
            this.mask = target as NavMeshAreaMaskHolder;
            result = string.Empty;
            areaId = serializedObject.FindProperty("areaMaskId");
        }
        /// <summary>
        /// Custom inspector draw
        /// </summary>
        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("Current Area Mask Value: " + areaId.intValue);

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            result = EditorGUILayout.TextField("NavMesh Area name", result);

            int id = 1 << NavMesh.GetAreaFromName(result);

            EditorGUILayout.LabelField("Name to Id value: " + id);

            if (id > 0)
            {
                int prev = areaId.intValue;
                bool present = (prev & id) != 0;

                using (new GUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("Add new Area"))
                        areaId.intValue = areaId.intValue | id;

                    GUILayout.FlexibleSpace();

                    if (GUILayout.Button("Remove Area"))
                        areaId.intValue = areaId.intValue & ~id;

                }

                using (new GUILayout.HorizontalScope())
                {
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("Clear all Areas"))
                        areaId.intValue = 0;
                    GUILayout.FlexibleSpace();
                }

                if (present)
                    EditorGUILayout.HelpBox("The inserted area name is present in the mask", MessageType.Info);

                if (prev != areaId.intValue)
                    serializedObject.ApplyModifiedProperties();
            }
            else
            {
                EditorGUILayout.HelpBox("The inserted area name is not a valid name", MessageType.Warning);
            }
        }
    }
}