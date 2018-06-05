using System;
namespace SOPRO.Editor
{
    [Serializable]
    internal class ContHolder
    {
        public string FolderPath;
        public string UnderlyingType;
        public string ContainerType;
        public string FullContainerType;
        public bool UseArray;
        public bool GenerateIndexer;
        public ContHolder()
        {
            FolderPath = string.Empty;
            UnderlyingType = string.Empty;
            ContainerType = string.Empty;
            FullContainerType = string.Empty;
            UseArray = true;
            GenerateIndexer = true;
        }
    }
}