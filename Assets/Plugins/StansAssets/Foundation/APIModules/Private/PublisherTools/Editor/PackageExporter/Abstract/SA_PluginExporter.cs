using System.Collections.Generic;

namespace SA.Foundation.Publisher.Exporter
{
    public abstract class SA_PluginExporter : SA_PackageExporter
    {
        readonly string m_PackageName = "undefined";

        SA_PluginFileset m_Fileset;
        readonly SA_PluginFileset m_FoundationFileset;

        protected SA_PluginExporter(string packageName)
            : base()
        {
            m_PackageName = packageName;
            m_FoundationFileset = SA_FilesetManager.GetFileset(SA_FoundationFileset.ID);
        }

        protected void SetFileSet(SA_PluginFileset fileset)
        {
            m_Fileset = fileset;
        }

        protected sealed override List<string> GetDirsIncludedPaths()
        {
            var result = new List<string>();
            if (m_FoundationFileset != null) result.AddRange(m_FoundationFileset.GetDirsIncludedPaths());
            if (m_Fileset != null) result.AddRange(m_Fileset.GetDirsIncludedPaths());
            return result;
        }

        protected sealed override List<string> GetFilesIncludedPaths()
        {
            var result = new List<string>();
            if (m_Fileset != null) result.AddRange(m_Fileset.GetFilesIncludedPaths());
            if (m_FoundationFileset != null) result.AddRange(m_FoundationFileset.GetFilesIncludedPaths());
            return result;
        }

        protected sealed override List<string> GetExcludedPaths()
        {
            var result = new List<string>();
            if (m_FoundationFileset != null) result.AddRange(m_FoundationFileset.GetExcludedPaths());
            if (m_Fileset != null) result.AddRange(m_Fileset.GetExcludedPaths());
            return result;
        }

        protected override string GetPackageFileName()
        {
            var name = m_PackageName.ToLower();
            name = name.Replace(" ", "_");
            name = string.Format("{0}_{1}", name, Version);
            return name;
        }

        protected override string GetPackageSubfolder()
        {
            return string.Empty;
        }

        public abstract string Version { get; }

        public override string ToString()
        {
            return $"{m_PackageName} (v. {Version})";
        }

        public void PrepareToRelease()
        {
            m_Fileset.UpgradeVersionIfNeed();
        }

        public void UpgrageMinorVersion()
        {
            m_Fileset.UpgrageMinorVersion();
        }
    }
}
