using SA.Foundation.Utility;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using StansAssets.Foundation.Extensions;
using UnityEditor;
using UnityEngine;

namespace SA.Foundation.Publisher.Exporter
{
    public abstract class SA_PackageExporter
    {
        public void Export(string outputFolder)
        {
            var directory = $"{outputFolder}{GetPackageSubfolder()}";
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

            var fullFileExportName = $"{directory}{GetPackageFileName()}.unityPackage".Trim();

            var pathsToExport = GetPathsToExport();
            if (pathsToExport.Length != 0)
            {
                SA_LineEndingResolver.Resolve(pathsToExport);
                Debug.Log("exported to: " + fullFileExportName);
                AssetDatabase.ExportPackage(GetPathsToExport(), fullFileExportName, ExportPackageOptions.Interactive);
            }
        }

        protected abstract string GetPackageFileName();
        protected abstract string GetPackageSubfolder();

        static string RemoveTrailingSlash(string path)
        {
            if (path.EndsWith("/")) path = path.RemoveLast(1);
            return path;
        }

        string[] GetPathsToExport()
        {
            var excludes = GetExcludedPaths();
            var includedAssets = AssetDatabase.FindAssets(string.Empty, GetDirsIncludedPaths().Select(path => RemoveTrailingSlash(path)).ToArray())
                .Select(assetGuid => AssetDatabase.GUIDToAssetPath(assetGuid))
                .Where(assetPath => !excludes.Exists(filterPattern => Regex.IsMatch(assetPath, filterPattern)))
                .ToList();

            includedAssets.AddRange(GetFilesIncludedPaths());

            return includedAssets.ToArray();
        }

        protected abstract List<string> GetDirsIncludedPaths();
        protected abstract List<string> GetFilesIncludedPaths();

        protected virtual List<string> GetExcludedPaths()
        {
            return new List<string>();
        }
    }
}
