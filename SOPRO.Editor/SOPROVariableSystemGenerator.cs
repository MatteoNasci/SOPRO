using SOPRO.Editor.CodeGenerators;
using System.IO;
using UnityEditor;
using UnityEngine;
namespace SOPRO.Editor
{
    /// <summary>
    /// Class that generates SO var system code
    /// </summary>
    [CreateAssetMenu(fileName = "SOPROVarGenerator", menuName = "SOPRO/Variables/Generator")]
    public class SOPROVariableSystemGenerator : ScriptableObject
    {
        /// <summary>
        /// Folder path under Assets
        /// </summary>
        public static readonly string TargetFolderPath = Path.Combine(Path.Combine("Scripts", "GeneratedCode"), "SOPROVariablesSystem");
        /// <summary>
        /// Type used
        /// </summary>
        public string Type { get { return type; } set { type = value; } }
        /// <summary>
        /// namespace used
        /// </summary>
        public string NameSpace { get { return nameSpace; } set { nameSpace = value; } }
        /// <summary>
        /// True to generate code
        /// </summary>
        public bool Generate { get { return generate; } set { generate = value; } }
        /// <summary>
        /// Target folder for the output
        /// </summary>
        public string FullTargetFolderPath { get; private set; }
        /// <summary>
        /// Target Editor folder for the output
        /// </summary>
        public string FullTargetEditorFolderPath { get; private set; }
        [SerializeField]
        private string type;
        [SerializeField]
        private string nameSpace;
        [SerializeField]
        private bool generate = false;

        private SOReferenceEditorGenerator refEditorGen;
        private SOReferenceGenerator refGen;
        private SOVariableGenerator varGen;

        /// <summary>
        /// Generates scripts for SO var system
        /// </summary>
        public void GenerateVariableCode()
        {
            if (type == null || type.Length == 0)
                return;

            if (refEditorGen == null)
                refEditorGen = new SOReferenceEditorGenerator();
            if (refGen == null)
                refGen = new SOReferenceGenerator();
            if (varGen == null)
                varGen = new SOVariableGenerator();

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

            if (!Directory.Exists(FullTargetFolderPath))
                Directory.CreateDirectory(FullTargetFolderPath);

            string fileName = Path.Combine(FullTargetFolderPath, Path.ChangeExtension(varClassName, ".cs"));

            if (!File.Exists(fileName))
                File.WriteAllText(fileName, varCode);
            else
                throw new UnityException("Error occurred while attempting code generation from " + this + " , file " + fileName + " already exists");

            fileName = Path.Combine(FullTargetFolderPath, Path.ChangeExtension(refClassName, ".cs"));

            if (!File.Exists(fileName))
                File.WriteAllText(fileName, refCode);
            else
                throw new UnityException("Error occurred while attempting code generation from " + this + " , file " + fileName + " already exists");

            if (!Directory.Exists(FullTargetEditorFolderPath))
                Directory.CreateDirectory(FullTargetEditorFolderPath);

            fileName = Path.Combine(FullTargetEditorFolderPath, Path.ChangeExtension(editorClassName, ".cs"));

            if (!File.Exists(fileName))
                File.WriteAllText(fileName, editorCode);
            else
                throw new UnityException("Error occurred while attempting code generation from " + this + " , file " + fileName + " already exists");
        }
        void OnValidate()
        {
            if (generate)
            {
                generate = false;
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