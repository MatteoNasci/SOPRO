using SOPRO.Editor.CodeGenerators;
using System.IO;
using UnityEditor;
using UnityEngine;
namespace SOPRO.Editor
{
    /// <summary>
    /// Class that generates SO var system code
    /// </summary>
    [CreateAssetMenu(fileName = "VarGenerator", menuName = "SOPRO/Variables/Generator")]
    public class SOPROVariableSystemGenerator : ScriptableObject
    {
        /// <summary>
        /// Folder path under Assets
        /// </summary>
        public static readonly string TargetFolderPath = Path.Combine(Path.Combine("Scripts", "GeneratedCode"), "SOPROVariablesSystem");
        /// <summary>
        /// Namespace used by default
        /// </summary>
        public const string DefaultNamespace = "SOPRO.Variables";
        /// <summary>
        /// Target folder for the output
        /// </summary>
        public string FullTargetFolderPath { get; private set; }
        /// <summary>
        /// Target Editor folder for the output
        /// </summary>
        public string FullTargetEditorFolderPath { get; private set; }
        /// <summary>
        /// Type used
        /// </summary>
        public string Type;
        /// <summary>
        /// True to generate code
        /// </summary>
        public bool GenerateCode = false;

        private SOReferenceEditorGenerator refEditorGen;
        private SOReferenceGenerator refGen;
        private SOVariableGenerator varGen;

        /// <summary>
        /// Generates scripts for SO var system
        /// </summary>
        public void GenerateVariableCode()
        {
            if (Type == null || Type.Length == 0)
                return;

            if (refEditorGen == null)
                refEditorGen = new SOReferenceEditorGenerator();
            if (refGen == null)
                refGen = new SOReferenceGenerator();
            if (varGen == null)
                varGen = new SOVariableGenerator();

            string editorClassName = "Reference" + (Type.Length > 1 ? char.ToUpper(Type[0]) + Type.Substring(1) : Type) + "Drawer";
            string varClassName = "SOVariable" + (Type.Length > 1 ? char.ToUpper(Type[0]) + Type.Substring(1) : Type);
            string refClassName = "Reference" + (Type.Length > 1 ? char.ToUpper(Type[0]) + Type.Substring(1) : Type);
            string assetFileName = "\"" + varClassName + "\"";
            string assetMenuName = "\"" + "SOPRO/Variables/" + (Type.Length > 1 ? char.ToUpper(Type[0]) + Type.Substring(1) : Type) + "\"";

            refEditorGen.ClassName = editorClassName;
            refEditorGen.Namespace = (DefaultNamespace == null || DefaultNamespace.Length == 0) ? DefaultNamespace : DefaultNamespace + ".Editor";
            refEditorGen.ReferenceTypeName = refClassName;


            refGen.ClassName = refClassName;
            refGen.Namespace = DefaultNamespace;
            refGen.Type = Type;
            refGen.VariableClassName = varClassName;


            varGen.AssetFileName = assetFileName;
            varGen.AssetMenuName = assetMenuName;
            varGen.ClassName = varClassName;
            varGen.Namespace = DefaultNamespace;
            varGen.Type = Type;

            string varCode = varGen.TransformText();
            string refCode = refGen.TransformText();
            string editorCode = refEditorGen.TransformText();

            if (!Directory.Exists(FullTargetFolderPath))
                Directory.CreateDirectory(FullTargetFolderPath);

            string fileName = Path.Combine(FullTargetFolderPath, Path.ChangeExtension(varClassName, ".cs"));

            if (!File.Exists(fileName))
                File.WriteAllText(fileName, varCode);
            else
                Debug.Log("Error occurred while attempting code generation from " + this + " , file " + fileName + " already exists");

            fileName = Path.Combine(FullTargetFolderPath, Path.ChangeExtension(refClassName, ".cs"));

            if (!File.Exists(fileName))
                File.WriteAllText(fileName, refCode);
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
            if (GenerateCode)
            {
                GenerateCode = false;
                GenerateVariableCode();
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