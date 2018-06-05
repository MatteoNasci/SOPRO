using UnityEngine;
using UnityEditor;
using SOPRO.Editor.CodeGenerators;
using System.IO;
using System;
using System.Collections.Generic;
namespace SOPRO.Editor
{
    /// <summary>
    /// EditorWindow class that generates SOPRO code
    /// </summary>
    public sealed class GeneratorEditorWindow : EditorWindow
    {
        /// <summary>
        /// Number of types required by event generation
        /// </summary>
        public const int EventTypesNumber = 4;
        /// <summary>
        /// Default namespace used
        /// </summary>
        public const string DefaultNamespace = "SOPRO";
        /// <summary>
        /// Default folder path
        /// </summary>
        public static readonly string DefaultFolderPath = Path.Combine("Scripts", "GeneratedCode");

        [SerializeField]
        SOPROGeneratorType currentGenerationType = SOPROGeneratorType.None;
        [SerializeField]
        EvHolder evHolder = new EvHolder(EventTypesNumber);
        [SerializeField]
        ContHolder contHolder = new ContHolder();
        [SerializeField]
        VarHolder varHolder = new VarHolder();

        [MenuItem("Window/SOPRO/CodeGenerator")]
        static void CreateWindow()
        {
            GeneratorEditorWindow window = EditorWindow.GetWindow<GeneratorEditorWindow>(true, "SOPRO Generator", true);
            window.minSize = new Vector2(200, 200);
            window.ShowUtility();
        }
        void OnEnable()
        {
            evHolder.FolderPath = Path.Combine(Application.dataPath, Path.Combine(DefaultFolderPath, "SOPROEvents"));
            evHolder.EditorFolderPath = Path.Combine(evHolder.FolderPath, "Editor");

            contHolder.FolderPath = Path.Combine(Application.dataPath, Path.Combine(DefaultFolderPath, "SOPROContainers"));

            varHolder.FolderPath = Path.Combine(Application.dataPath, Path.Combine(DefaultFolderPath, "SOPROVariables"));
            varHolder.EditorFolderPath = Path.Combine(varHolder.FolderPath, "Editor");
        }
        void OnGUI()
        {
            using (new GUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();
                EditorGUILayout.LabelField("SOPRO Code Generator");
                GUILayout.FlexibleSpace();
            }

            currentGenerationType = (SOPROGeneratorType)EditorGUILayout.EnumPopup("Generator type", currentGenerationType);

            if (currentGenerationType == SOPROGeneratorType.None || !Enum.IsDefined(typeof(SOPROGeneratorType), currentGenerationType))
                return;

            switch (currentGenerationType)
            {
                case SOPROGeneratorType.None:
                    break;
                case SOPROGeneratorType.Containers:
                    ShowContainer();
                    break;
                case SOPROGeneratorType.Events:
                    ShowEvent();
                    break;
                case SOPROGeneratorType.Variables:
                    ShowVariable();
                    break;
                default:
                    break;
            }
        }
        void ShowEvent()
        {
            for (int i = 0; i < evHolder.Types.Length; i++)
            {
                evHolder.Types[i] = EditorGUILayout.TextField("Type " + i + " name", evHolder.Types[i]);
            }
            evHolder.ListenerMode = (SOPROListenerGenMode)EditorGUILayout.EnumPopup("Listener mode", evHolder.ListenerMode);
            if (!Enum.IsDefined(typeof(SOPROListenerGenMode), evHolder.ListenerMode))
                evHolder.ListenerMode = SOPROListenerGenMode.Default;

            if (GUILayout.Button("Generate Code"))
            {
                GenerateEventCode(evHolder.ListenerMode, evHolder.Types, DefaultNamespace, evHolder.FolderPath, evHolder.EditorFolderPath);
                AssetDatabase.Refresh();
            }
        }
        void ShowVariable()
        {
            varHolder.Type = EditorGUILayout.TextField("Type name", varHolder.Type);

            if (GUILayout.Button("Generate Code"))
            {
                GenerateVariableCode(varHolder.Type, DefaultNamespace, varHolder.FolderPath, varHolder.EditorFolderPath);
                AssetDatabase.Refresh();
            }
        }
        void ShowContainer()
        {
            contHolder.UnderlyingType = EditorGUILayout.TextField("Underlying type", contHolder.UnderlyingType);
            contHolder.GenerateIndexer = EditorGUILayout.Toggle("Create indexer", contHolder.GenerateIndexer);
            contHolder.UseArray = EditorGUILayout.Toggle("Use array", contHolder.UseArray);

            if (!contHolder.UseArray)
            {
                contHolder.ContainerType = EditorGUILayout.TextField("Container type", contHolder.ContainerType);
                contHolder.FullContainerType = EditorGUILayout.TextField("Full container type", contHolder.FullContainerType);
            }

            if (GUILayout.Button("Generate Code"))
            {
                GenerateContainerCode(contHolder.UnderlyingType, contHolder.UseArray, contHolder.ContainerType, contHolder.FullContainerType, contHolder.GenerateIndexer, DefaultNamespace, contHolder.FolderPath);
                AssetDatabase.Refresh();
            }
        }
        void GenerateContainerCode(string underlyingType, bool useArray, string containerType, string fullContainerType, bool generateIndexer, string nameSpace, string fullTargetFolderPath)
        {
            if (underlyingType == null || underlyingType.Length == 0)
                return;
            if (!useArray && (containerType == null || containerType.Length == 0 || fullContainerType == null || fullContainerType.Length == 0))
                return;

            SOContainerGenerator contGenerator = new SOContainerGenerator();
            //SORuntimeContainerGenerator contRunGenerator = new SORuntimeContainerGenerator();

            string ContClassName = "SO" + (useArray ? "Array" : containerType) + underlyingType + "Container";
            //string ContRunClassName = "SORuntime" + (UseArray ? "Array" : ContainerType) + UnderlyingType + "Container";


            contGenerator.ClassName = ContClassName;
            contGenerator.FullContainerTypeName = (useArray ? underlyingType + "[]" : fullContainerType);
            contGenerator.FullContainerTypeNameInit = (useArray ? underlyingType + "[0]" : fullContainerType + "()");
            contGenerator.GenerateIndexer = generateIndexer;
            contGenerator.Namespace = nameSpace;
            contGenerator.UnderlyingTypeName = underlyingType;
            contGenerator.AssetFileName = "\"" + "Container" + "\"";
            contGenerator.AssetMenuName = "\"" + "SOPRO/Containers/" + ContClassName + "\"";

            /*
            contRunGenerator.ClassName = ContRunClassName;
            contRunGenerator.FullContainerTypeName = (UseArray ? UnderlyingType + "[]" : FullContainerType);
            contRunGenerator.FullContainerTypeNameInit = (UseArray ? UnderlyingType + "[" + ArraySize + "]" : FullContainerType + "()");
            contRunGenerator.GenerateIndexer = GenerateIndexers;
            contRunGenerator.Namespace = DefaultNamespace;
            contRunGenerator.UnderlyingTypeName = UnderlyingType;
            contRunGenerator.AssetFileName = "\"" + "RuntimeContainer" + "\"";
            contRunGenerator.AssetMenuName = "\"" + "SOPRO/Containers/" + ContRunClassName + "\"";
            */

            string contCode = contGenerator.TransformText();
            //string contRunCode = contRunGenerator.TransformText();

            if (!Directory.Exists(fullTargetFolderPath))
                Directory.CreateDirectory(fullTargetFolderPath);

            string fileName = Path.Combine(fullTargetFolderPath, Path.ChangeExtension(ContClassName, ".cs"));

            if (!File.Exists(fileName))
                File.WriteAllText(fileName, contCode);
            else
                Debug.Log("Error occurred while attempting code generation from " + this + " , file " + fileName + " already exists");

            //fileName = Path.Combine(FullTargetFolderPath, Path.ChangeExtension(ContRunClassName, ".cs"));

            //if (!File.Exists(fileName))
            //    File.WriteAllText(fileName, contRunCode);
            //else
            //    Debug.Log("Error occurred while attempting code generation from " + this + " , file " + fileName + " already exists");
        }
        void GenerateEventCode(SOPROListenerGenMode listenerMode, string[] types, string nameSpace, string fullTargetFolderPath, string fullTargetEditorFolderPath)
        {
            if (types.Length != EventTypesNumber)
                throw new ArgumentException("Internal error. Event generator requires " + EventTypesNumber + " number of types as input.");

            string allTypes = "Void";

            string registerMethod = string.Empty;
            string unregisterMethod = string.Empty;

            if (listenerMode == SOPROListenerGenMode.AwakeDestroy)
            {
                registerMethod = "protected virtual void Awake()";
                unregisterMethod = "protected virtual void OnDestroy()";
            }
            else if (listenerMode == SOPROListenerGenMode.OnEnableOnDisable)
            {
                registerMethod = "protected virtual void OnEnable()";
                unregisterMethod = "protected virtual void OnDisable()";
            }

            for (int i = 0; i < types.Length; i++)
            {
                string read = types[i];
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
            if (types[0] != null && types[0] != string.Empty)
            {
                genericTypes = "<";
                for (int j = 0; j < types.Length; j++)
                {
                    if (types[j] != null && types[j] != string.Empty)
                    {
                        genericTypes = genericTypes + types[j];
                        arguments = arguments + "Value" + j;
                        argumentsWithTypes = argumentsWithTypes + types[j] + " Value" + j;
                        if (j != 3 && types[j + 1] != null && types[j + 1] != string.Empty)
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

            SOEventGenerator eventGenerator = new SOEventGenerator();
            SOEventListenerGenerator listenerGenerator = new SOEventListenerGenerator();
            UnityEventWrapperGenerator wrapperGenerator = new UnityEventWrapperGenerator();
            SOEventEditorGenerator editorGenerator = new SOEventEditorGenerator();

            List<string> validTypes = new List<string>();
            for (int i = 0; i < types.Length; i++)
            {
                string res = types[i];
                if (res != null && res.Length > 0)
                    validTypes.Add(res);
            }

            wrapperGenerator.ClassName = wrapperClassName;
            wrapperGenerator.Namespace = nameSpace;
            wrapperGenerator.UnityEventTypeName = unityEventClassName;

            listenerGenerator.ClassName = listenerClassName;
            listenerGenerator.GenericArguments = arguments;
            listenerGenerator.GenericArgumentsWithTypes = argumentsWithTypes;
            listenerGenerator.Namespace = nameSpace;
            listenerGenerator.RegisterMethodSignature = registerMethod;
            listenerGenerator.UnregisterMethodSignature = unregisterMethod;
            listenerGenerator.SOEventTypeName = eventClassName;
            listenerGenerator.UnityEventWrapperTypeName = wrapperClassName;

            eventGenerator.AssetFileName = eventAssetFileName;
            eventGenerator.AssetMenuName = eventAssetMenuName;
            eventGenerator.ClassName = eventClassName;
            eventGenerator.GenericArguments = arguments;
            eventGenerator.GenericArgumentsWithTypes = argumentsWithTypes;
            eventGenerator.Namespace = nameSpace;
            eventGenerator.SOEventListenerTypeName = listenerClassName;
            eventGenerator.ValidTypes = validTypes.ToArray();

            editorGenerator.ClassName = editorClassName;
            editorGenerator.Namespace = (nameSpace == null || nameSpace.Length == 0) ? nameSpace : nameSpace + ".Editor";
            editorGenerator.SOEventTypeName = eventClassName;
            editorGenerator.AllValidTypes = validTypes.ToArray();

            string eventCode = eventGenerator.TransformText();
            string listenerCode = listenerGenerator.TransformText();
            string wrapperCode = wrapperGenerator.TransformText();
            string editorCode = editorGenerator.TransformText();

            if (!Directory.Exists(fullTargetFolderPath))
                Directory.CreateDirectory(fullTargetFolderPath);

            string fileName = Path.Combine(fullTargetFolderPath, Path.ChangeExtension(eventClassName, ".cs"));

            if (!File.Exists(fileName))
                File.WriteAllText(fileName, eventCode);
            else
                Debug.Log("Error occurred while attempting code generation from " + this + " , file " + fileName + " already exists");

            fileName = Path.Combine(fullTargetFolderPath, Path.ChangeExtension(listenerClassName, ".cs"));

            if (!File.Exists(fileName))
                File.WriteAllText(fileName, listenerCode);
            else
                Debug.Log("Error occurred while attempting code generation from " + this + " , file " + fileName + " already exists");

            fileName = Path.Combine(fullTargetFolderPath, Path.ChangeExtension(wrapperClassName, ".cs"));

            if (!File.Exists(fileName))
                File.WriteAllText(fileName, wrapperCode);
            else
                Debug.Log("Error occurred while attempting code generation from " + this + " , file " + fileName + " already exists");

            if (!Directory.Exists(fullTargetEditorFolderPath))
                Directory.CreateDirectory(fullTargetEditorFolderPath);

            fileName = Path.Combine(fullTargetEditorFolderPath, Path.ChangeExtension(editorClassName, ".cs"));

            if (!File.Exists(fileName))
                File.WriteAllText(fileName, editorCode);
            else
                Debug.Log("Error occurred while attempting code generation from " + this + " , file " + fileName + " already exists");
        }
        void GenerateVariableCode(string type, string nameSpace, string fullTargetFolderPath, string fullTargetEditorFolderPath)
        {
            if (type == null || type.Length == 0)
                return;

            SOReferenceEditorGenerator refEditorGen = new SOReferenceEditorGenerator();
            SOReferenceGenerator refGen = new SOReferenceGenerator();
            SOVariableGenerator varGen = new SOVariableGenerator();

            string editorClassName = "Reference" + (type.Length > 1 ? char.ToUpper(type[0]) + type.Substring(1) : type) + "Drawer";
            string varClassName = "SOVariable" + (type.Length > 1 ? char.ToUpper(type[0]) + type.Substring(1) : type);
            string refClassName = "Reference" + (type.Length > 1 ? char.ToUpper(type[0]) + type.Substring(1) : type);
            string assetFileName = "\"" + varClassName + "\"";
            string assetMenuName = "\"" + "SOPRO/Variables/" + (type.Length > 1 ? char.ToUpper(type[0]) + type.Substring(1) : type) + "\"";

            refEditorGen.ClassName = editorClassName;
            refEditorGen.Namespace = (nameSpace == null || nameSpace.Length == 0) ? nameSpace : nameSpace + ".Editor";
            refEditorGen.ReferenceTypeName = refClassName;


            refGen.ClassName = refClassName;
            refGen.Namespace = nameSpace;
            refGen.Type = type;
            refGen.VariableClassName = varClassName;


            varGen.AssetFileName = assetFileName;
            varGen.AssetMenuName = assetMenuName;
            varGen.ClassName = varClassName;
            varGen.Namespace = nameSpace;
            varGen.Type = type;

            string varCode = varGen.TransformText();
            string refCode = refGen.TransformText();
            string editorCode = refEditorGen.TransformText();

            if (!Directory.Exists(fullTargetFolderPath))
                Directory.CreateDirectory(fullTargetFolderPath);

            string fileName = Path.Combine(fullTargetFolderPath, Path.ChangeExtension(varClassName, ".cs"));

            if (!File.Exists(fileName))
                File.WriteAllText(fileName, varCode);
            else
                Debug.Log("Error occurred while attempting code generation from " + this + " , file " + fileName + " already exists");

            fileName = Path.Combine(fullTargetFolderPath, Path.ChangeExtension(refClassName, ".cs"));

            if (!File.Exists(fileName))
                File.WriteAllText(fileName, refCode);
            else
                Debug.Log("Error occurred while attempting code generation from " + this + " , file " + fileName + " already exists");

            if (!Directory.Exists(fullTargetEditorFolderPath))
                Directory.CreateDirectory(fullTargetEditorFolderPath);

            fileName = Path.Combine(fullTargetEditorFolderPath, Path.ChangeExtension(editorClassName, ".cs"));

            if (!File.Exists(fileName))
                File.WriteAllText(fileName, editorCode);
            else
                Debug.Log("Error occurred while attempting code generation from " + this + " , file " + fileName + " already exists");
        }
    }
}