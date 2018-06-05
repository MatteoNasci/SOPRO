using System;
namespace SOPRO.Editor
{
    [Serializable]
    internal class VarHolder
    {
        public string FolderPath;
        public string Type;
        public string EditorFolderPath;
        public VarHolder()
        {
            FolderPath = string.Empty;
            EditorFolderPath = string.Empty;
            Type = string.Empty;
        }
    }
}