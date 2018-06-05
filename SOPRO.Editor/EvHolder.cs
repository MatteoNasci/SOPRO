﻿using System;
namespace SOPRO.Editor
{
    [Serializable]
    internal class EvHolder
    {
        public string FolderPath;
        public string EditorFolderPath;
        public SOPROListenerGenMode ListenerMode;
        public readonly string[] Types;
        public EvHolder(int typesLength)
        {
            FolderPath = string.Empty;
            EditorFolderPath = string.Empty;
            ListenerMode = SOPROListenerGenMode.Default;
            Types = new string[typesLength];
            for (int i = 0; i < Types.Length; i++)
            {
                Types[i] = string.Empty;
            }
        }
    }
}