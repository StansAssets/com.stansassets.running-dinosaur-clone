using System.Collections.Generic;
using UnityEditor;

namespace SA.Foundation.Publisher.Exporter
{
    public static class SA_PluginExportersManager
    {
        public const string PREFS_KEY_EXPORTER_BASE_DIR = "ExporterBaseDir";

        static readonly Dictionary<string, List<SA_PluginExporter>> s_exporters = new Dictionary<string, List<SA_PluginExporter>>();

        static SA_PluginExportersManager()
        {
            var сrossPlatform = new List<SA_PluginExporter>();
            сrossPlatform.Add(new SA_UM_Exporter());
            s_exporters.Add("CrossPlatform", сrossPlatform);

            var nativePlugins = new List<SA_PluginExporter>();
            nativePlugins.Add(new SA_ISN_Exporter());
            nativePlugins.Add(new SA_AN_Exporter());
            s_exporters.Add("Native Plugins", nativePlugins);

            var nativeUtility = new List<SA_PluginExporter>();
            nativeUtility.Add(new SA_ISD_Exporter());
            nativeUtility.Add(new SA_AMM_Exporter());
            s_exporters.Add("Native Utility", nativeUtility);
        }

        public static Dictionary<string, List<SA_PluginExporter>> Exporters => s_exporters;

        public static string BaseDir
        {
            get => EditorPrefs.GetString(PREFS_KEY_EXPORTER_BASE_DIR);
            set
            {
                var oldValue = BaseDir;
                if(oldValue.Equals(value))
                    return;

                EditorPrefs.SetString(PREFS_KEY_EXPORTER_BASE_DIR, value);
            }
        }

        public static void ExportPackage(SA_PackageExporter exporter)
        {
            exporter.Export(BaseDir);
        }

        public static void ReleaseAllPackages()
        {
            if (SA_FilesetManager.IsFoundationChanged()) SA_FilesetManager.UpdateFoundationVersion();
            foreach (var exporterList in s_exporters.Values)
            foreach (var exporter in exporterList)
                ReleasePackage(exporter);
        }

        public static void ReleasePackage(SA_PluginExporter exporter)
        {
            if (SA_FilesetManager.IsFoundationChanged())
            {
                EditorUtility.DisplayDialog("Error", "Release canceled, Foundation has changes", "OK");
                return;
            }

            exporter.PrepareToRelease();
            exporter.Export(BaseDir);
        }
    }
}
