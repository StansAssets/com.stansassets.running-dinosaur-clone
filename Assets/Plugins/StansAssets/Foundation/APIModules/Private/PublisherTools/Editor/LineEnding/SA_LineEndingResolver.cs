using System.Collections;
using System.Collections.Generic;
using SA.Foundation.UtilitiesEditor;
using UnityEngine;
using System.IO;
using System;

namespace SA.Foundation.Publisher
{
    /// <summary>
    /// This object update line endings for all .cs files in project "Assets" folder.
    /// </summary>
    class SA_LineEndingResolver
    {
        /// <summary>
        /// Method that update line endings for all .cs files in project.
        /// </summary>
        internal static void Resolve()
        {
            var m_Files = SA_AssetDatabase.FindAssetsWithExtentions("Assets", ".cs");
            foreach (var file in m_Files)
                if (File.Exists(file))
                    UpdateFile(file);
        }

        /// <summary>
        /// Method that update line endings for given files.
        /// </summary>
        internal static void Resolve(string[] files)
        {
            foreach (var file in files)
                if (SA_AssetDatabase.GetExtension(file).Equals(".cs"))
                    UpdateFile(file);
        }

        static void UpdateFile(string path)
        {
            var data = File.ReadAllText(path);
            data = data.Replace("\n", "\r\n");
            data = data.Replace("\r\r\n", "\r\n");
            File.WriteAllText(path, data);
        }
    }
}
