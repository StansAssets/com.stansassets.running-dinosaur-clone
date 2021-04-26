using UnityEngine;
using System.Collections;
using SA.Foundation.Publisher.Exporter;
using System.Collections.Generic;

namespace SA.Foundation.Publisher.Exporter
{
    public abstract class SA_PluginBundleExporter : SA_PackageExporter
    {
        readonly List<SA_PluginFileset> m_filesets = new List<SA_PluginFileset>();

        protected void AddFileSet(SA_PluginFileset fileset)
        {
            m_filesets.Add(fileset);
        }

        protected override List<string> GetDirsIncludedPaths()
        {
            var result = new List<string>();
            m_filesets.ForEach(fileset =>
            {
                result.AddRange(fileset.GetDirsIncludedPaths());
            });
            return result;
        }

        protected override List<string> GetFilesIncludedPaths()
        {
            var result = new List<string>();
            m_filesets.ForEach(fileset =>
            {
                result.AddRange(fileset.GetFilesIncludedPaths());
            });
            return result;
        }

        protected override List<string> GetExcludedPaths()
        {
            var result = new List<string>();
            m_filesets.ForEach(fileset =>
            {
                result.AddRange(fileset.GetExcludedPaths());
            });
            return result;
        }

        protected override string GetPackageFileName()
        {
            return PackageName.Replace(" ", "_");
        }

        protected override string GetPackageSubfolder()
        {
            return string.Empty;
        }

        public abstract string PackageName { get; }

        public override string ToString()
        {
            return PackageName;
        }
    }
}
