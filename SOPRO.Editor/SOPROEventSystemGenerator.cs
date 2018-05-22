using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using SOPRO.Editor.CodeGenerators;
namespace SOPRO.Editor
{
    /// <summary>
    /// Class that generates event code
    /// </summary>
    [CreateAssetMenu(menuName = "SOPRO/Events/Generator", fileName = "EvGenerator")]
    public class SOPROEventSystemGenerator : ScriptableObject
    {
        /// <summary>
        /// Max number of types allowed
        /// </summary>
        public const int MaxTypes = 4;
        /// <summary>
        /// Folder path under Assets
        /// </summary>
        public static readonly string TargetFolderPath = Path.Combine(Path.Combine("Scripts", "GeneratedCode"), "SOPROEventSystem");
        /// <summary>
        /// Namespace used by default
        /// </summary>
        public const string DefaultNamespace = "SOPRO.Events";
        /// <summary>
        /// Target folder for the output
        /// </summary>
        public string FullTargetFolderPath { get; private set; }
        /// <summary>
        /// Target Editor folder for the output
        /// </summary>
        public string FullTargetEditorFolderPath { get; private set; }

        /// <summary>
        /// Set to true in order to generate code
        /// </summary>
        public bool GenerateCode = false;
        /// <summary>
        /// Types used in the next code generation
        /// </summary>
        public string[] Types = new string[MaxTypes];
        /// <summary>
        /// Listener mode for the next code generation
        /// </summary>
        public SOPROListenerGenMode ListenerMode = SOPROListenerGenMode.OnEnableOnDisable;

        private SOEventGenerator eventGenerator;
        private SOEventListenerGenerator listenerGenerator;
        private UnityEventWrapperGenerator wrapperGenerator;
        private SOEventEditorGenerator editorGenerator;

        /// <summary>
        /// Generate all event code
        /// </summary>
        public void GenerateEventCode()
        {
            string allTypes = "Void";

            string registerMethod = string.Empty;
            string unregisterMethod = string.Empty;

            if (ListenerMode == SOPROListenerGenMode.AwakeDestroy)
            {
                registerMethod = "protected virtual void Awake()";
                unregisterMethod = "protected virtual void OnDestroy()";
            }
            else if (ListenerMode == SOPROListenerGenMode.OnEnableOnDisable)
            {
                registerMethod = "protected virtual void OnEnable()";
                unregisterMethod = "protected virtual void OnDisable()";
            }

            for (int i = 0; i < Types.Length; i++)
            {
                string read = Types[i];
                if (read != null && read.Length > 0)
                {
                    if (i == 0)
                    {
                        allTypes = string.Empty;
                    }

                    if (read.Length > 1)
                    {
                        allTypes = allTypes + char.ToUpper(read[0]) + read.Substring(1);
                    }
                    else
                    {
                        allTypes = allTypes + read.ToUpper();
                    }
                }
            }

            //Iterate through all recovered types and create usefull strings
            string genericTypes = string.Empty;
            string argumentsWithTypes = string.Empty;
            string arguments = string.Empty;
            if (Types[0] != null && Types[0] != string.Empty)
            {
                genericTypes = "<";
                for (int j = 0; j < Types.Length; j++)
                {
                    if (Types[j] != null && Types[j] != string.Empty)
                    {
                        genericTypes = genericTypes + Types[j];
                        arguments = arguments + "Value" + j;
                        argumentsWithTypes = argumentsWithTypes + Types[j] + " Value" + j;
                        if (j != 3 && Types[j + 1] != null && Types[j + 1] != string.Empty)
                        {
                            genericTypes = genericTypes + ", ";
                            arguments = arguments + ", ";
                            argumentsWithTypes = argumentsWithTypes + ", ";
                        }
                    }
                }
                genericTypes = genericTypes + ">";
            }
            string eventAssetFileName = "\"Event\"";
            string eventAssetMenuName = "\"" + "SOPRO/Events/" + allTypes + "\"";
            string eventClassName = "SOEv" + allTypes;
            string wrapperClassName = "UnEv" + allTypes;
            string editorClassName = eventClassName + "Drawer";
            string listenerClassName = eventClassName + "Listener";
            string unityEventClassName = "UnityEvent" + genericTypes;

            if (eventGenerator == null)
                eventGenerator = new SOEventGenerator();
            if (listenerGenerator == null)
                listenerGenerator = new SOEventListenerGenerator();
            if (wrapperGenerator == null)
                wrapperGenerator = new UnityEventWrapperGenerator();
            if (editorGenerator == null)
                editorGenerator = new SOEventEditorGenerator();

            List<string> validTypes = new List<string>();
            for (int i = 0; i < Types.Length; i++)
            {
                string res = Types[i];
                if (res != null && res.Length > 0)
                    validTypes.Add(res);
            }

            wrapperGenerator.ClassName = wrapperClassName;
            wrapperGenerator.Namespace = DefaultNamespace;
            wrapperGenerator.UnityEventTypeName = unityEventClassName;

            listenerGenerator.ClassName = listenerClassName;
            listenerGenerator.GenericArguments = arguments;
            listenerGenerator.GenericArgumentsWithTypes = argumentsWithTypes;
            listenerGenerator.Namespace = DefaultNamespace;
            listenerGenerator.RegisterMethodSignature = registerMethod;
            listenerGenerator.UnregisterMethodSignature = unregisterMethod;
            listenerGenerator.SOEventTypeName = eventClassName;
            listenerGenerator.UnityEventWrapperTypeName = wrapperClassName;

            eventGenerator.AssetFileName = eventAssetFileName;
            eventGenerator.AssetMenuName = eventAssetMenuName;
            eventGenerator.ClassName = eventClassName;
            eventGenerator.GenericArguments = arguments;
            eventGenerator.GenericArgumentsWithTypes = argumentsWithTypes;
            eventGenerator.Namespace = DefaultNamespace;
            eventGenerator.SOEventListenerTypeName = listenerClassName;
            eventGenerator.ValidTypes = validTypes.ToArray();

            editorGenerator.ClassName = editorClassName;
            editorGenerator.Namespace = (DefaultNamespace == null || DefaultNamespace.Length == 0) ? DefaultNamespace : DefaultNamespace + ".Editor";
            editorGenerator.SOEventTypeName = eventClassName;
            editorGenerator.AllValidTypes = validTypes.ToArray();

            string eventCode = eventGenerator.TransformText();
            string listenerCode = listenerGenerator.TransformText();
            string wrapperCode = wrapperGenerator.TransformText();
            string editorCode = editorGenerator.TransformText();

            if (!Directory.Exists(FullTargetFolderPath))
                Directory.CreateDirectory(FullTargetFolderPath);

            string fileName = Path.Combine(FullTargetFolderPath, Path.ChangeExtension(eventClassName, ".cs"));

            if (!File.Exists(fileName))
                File.WriteAllText(fileName, eventCode);
            else
                Debug.Log("Error occurred while attempting code generation from " + this + " , file " + fileName + " already exists");

            fileName = Path.Combine(FullTargetFolderPath, Path.ChangeExtension(listenerClassName, ".cs"));

            if (!File.Exists(fileName))
                File.WriteAllText(fileName, listenerCode);
            else
                Debug.Log("Error occurred while attempting code generation from " + this + " , file " + fileName + " already exists");

            fileName = Path.Combine(FullTargetFolderPath, Path.ChangeExtension(wrapperClassName, ".cs"));

            if (!File.Exists(fileName))
                File.WriteAllText(fileName, wrapperCode);
            else
                Debug.Log("Error occurred while attempting code generation from " + this + " , file " + fileName + " already exists");

            if (!Directory.Exists(FullTargetEditorFolderPath))
                Directory.CreateDirectory(FullTargetEditorFolderPath);

            fileName = Path.Combine(FullTargetEditorFolderPath, Path.ChangeExtension(editorClassName, ".cs"));

            if (!File.Exists(fileName))
                File.WriteAllText(fileName, editorCode);
            else
                Debug.Log("Error occurred while attempting code generation from " + this + " , file " + fileName + " already exists");
        }
        void OnValidate()
        {
            if (Types == null)
                Types = new string[MaxTypes];

            if (!Enum.IsDefined(typeof(SOPROListenerGenMode), ListenerMode))
                ListenerMode = SOPROListenerGenMode.Default;

            if (Types.Length != MaxTypes)
            {
                string[] temp = new string[MaxTypes];
                for (int i = 0; i < temp.Length; i++)
                {
                    string res = i < Types.Length ? Types[i] : string.Empty;
                    temp[i] = res;
                }
                Types = temp;
            }

            if (GenerateCode)
            {
                GenerateCode = false;
                GenerateEventCode();
                AssetDatabase.Refresh();
            }
        }
        void Awake()
        {
            FullTargetFolderPath = Path.Combine(Application.dataPath, TargetFolderPath);
            FullTargetEditorFolderPath = Path.Combine(FullTargetFolderPath, "Editor");
        }
    }
}