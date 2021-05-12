using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using SA.Foundation.Config;
using UnityEditor;
using UnityEngine;

namespace SA.Foundation.Publisher.Exporter
{
    public abstract class SA_PluginFileset
    {
        public abstract string Id { get; }

        public virtual List<string> GetDirsIncludedPaths()
        {
            return new List<string>();
        }

        public virtual List<string> GetFilesIncludedPaths()
        {
            return new List<string>();
        }

        public virtual List<string> GetExcludedPaths()
        {
            return new List<string>
            {
                ".*obj.meta",
                ".*obj"
            };
        }

        public bool HasChanges => SA_Config.FoundationVersion.HasChanges();

        protected SA_PluginFileset()
        {
            m_dirsIncludedPaths = GetDirsIncludedPaths();
            m_filesIncludedPaths = GetFilesIncludedPaths();
            m_excludedPaths = GetExcludedPaths();
            m_excludedPaths.Add(".*pluginVersion\\.json");
        }

        readonly List<string> m_dirsIncludedPaths;
        readonly List<string> m_filesIncludedPaths;
        readonly List<string> m_excludedPaths;

        bool IsAssetBelongToFileSet(string assetPath)
        {
            if (m_filesIncludedPaths.Contains(assetPath)) return true;

            if (AssetIsFolder(assetPath)) return false;
            if (!m_dirsIncludedPaths.Exists(assetPath.StartsWith)) return false;

            if (m_excludedPaths.Exists(filterPattern => Regex.IsMatch(assetPath, filterPattern))) return false;

            return true;
        }

        bool AssetIsFolder(string assetPath)
        {
            return AssetDatabase.IsValidFolder(assetPath);
        }

        protected abstract PluginVersionHandler GetPluginVersion();

        public void UpgradeVersionIfNeed()
        {
            GetPluginVersion().UpgrageMajorVersionIfNeed();
        }

        public void UpgrageMinorVersion()
        {
            GetPluginVersion().UpgrageMinorVersion();
        }
    }
}
