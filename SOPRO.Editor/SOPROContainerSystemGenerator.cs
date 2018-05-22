using SOPRO.Editor.CodeGenerators;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
namespace SOPRO.Editor
{
    /// <summary>
    /// Class that generates SO container system code
    /// </summary>
    [CreateAssetMenu(fileName = "ContGenerator", menuName = "SOPRO/Containers/Generator")]
    public class SOPROContainerSystemGenerator : ScriptableObject
    {
        /// <summary>
        /// Folder path under Assets
        /// </summary>
        public static readonly string TargetFolderPath = Path.Combine(Path.Combine("Scripts", "GeneratedCode"), "SOPROContainersSystem");
        /// <summary>
        /// Namespace used by default
        /// </summary>
        public const string DefaultNamespace = "SOPRO.Containers";
        /// <summary>
        /// Target folder for the output
        /// </summary>
        public string FullTargetFolderPath { get; private set; }
        /// <summary>
        /// If true an array of the underlying type will be used as a container
        /// </summary>
        public bool UseArray = false;
        /// <summary>
        /// If UseArray is true this value will be the array size
        /// </summary>
        public int ArraySize = 0;
        /// <summary>
        /// Container Type used. Examble : List
        /// </summary>
        public string ContainerType;
        /// <summary>
        /// Underlying Type used
        /// </summary>
        public string UnderlyingType;
        /// <summary>
        /// Full container Type used . Example : <see cref="List{T}"/> with typeName instead of T
        /// </summary>
        public string FullContainerType;
        /// <summary>
        /// If true containers will be generated with an indexer to the underlying type
        /// </summary>
        public bool GenerateIndexers = true;
        /// <summary>
        /// True to generate code
        /// </summary>
        public bool GenerateCode = false;

        private SOContainerGenerator contGenerator;
        private SORuntimeContainerGenerator contRunGenerator;

        /// <summary>
        /// Generates scripts for SO container system
        /// </summary>
        public void GenerateVariableCode()
        {
            if (UnderlyingType == null || UnderlyingType.Length == 0)
                return;
            if (!UseArray && (ContainerType == null || ContainerType.Length == 0 || FullContainerType == null || FullContainerType.Length == 0))
                return;

            if (contGenerator == null)
                contGenerator = new SOContainerGenerator();
            if (contRunGenerator == null)
                contRunGenerator = new SORuntimeContainerGenerator();

            string ContClassName = "SO" + (UseArray ? "Array" : ContainerType) + UnderlyingType + "Container";
            string ContRunClassName = "SORuntime" + (UseArray ? "Array" : ContainerType) + UnderlyingType + "Container";


            contGenerator.ClassName = ContClassName;
            contGenerator.FullContainerTypeName = (UseArray ? UnderlyingType + "[]" : FullContainerType);
            contGenerator.FullContainerTypeNameInit = (UseArray ? UnderlyingType + "[" + ArraySize + "]" : FullContainerType + "()");
            contGenerator.GenerateIndexer = GenerateIndexers;
            contGenerator.Namespace = DefaultNamespace;
            contGenerator.UnderlyingTypeName = UnderlyingType;

            contRunGenerator.ClassName = ContRunClassName;
            contRunGenerator.FullContainerTypeName = (UseArray ? UnderlyingType + "[]" : FullContainerType);
            contRunGenerator.FullContainerTypeNameInit = (UseArray ? UnderlyingType + "[" + ArraySize + "]" : FullContainerType + "()");
            contRunGenerator.GenerateIndexer = GenerateIndexers;
            contRunGenerator.Namespace = DefaultNamespace;
            contRunGenerator.UnderlyingTypeName = UnderlyingType;

            string contCode = contGenerator.TransformText();
            string contRunCode = contRunGenerator.TransformText();

            if (!Directory.Exists(FullTargetFolderPath))
                Directory.CreateDirectory(FullTargetFolderPath);

            string fileName = Path.Combine(FullTargetFolderPath, Path.ChangeExtension(ContClassName, ".cs"));

            if (!File.Exists(fileName))
                File.WriteAllText(fileName, contCode);
            else
                Debug.Log("Error occurred while attempting code generation from " + this + " , file " + fileName + " already exists");

            fileName = Path.Combine(FullTargetFolderPath, Path.ChangeExtension(ContRunClassName, ".cs"));

            if (!File.Exists(fileName))
                File.WriteAllText(fileName, contRunCode);
            else
                Debug.Log("Error occurred while attempting code generation from " + this + " , file " + fileName + " already exists");
        }
        void OnValidate()
        {
            if (ArraySize < 0)
                ArraySize = 0;

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
        }
    }
}