using SA.Foundation.Editor;
using SA.Foundation.Publisher.Exporter;
using StansAssets.Foundation.OperatingSystem;
using StansAssets.Plugins.Editor;
using UnityEditor;
using UnityEngine;

namespace SA.Foundation.Publisher
{
    public class SA_PackagesExportTab : IMGUILayoutElement
    {
        const int k_ButtonWidth = 80;

        public override void OnGUI()
        {
            foreach (var pair in SA_PluginExportersManager.Exporters)
            {
                var title = pair.Key;
                var exporters = pair.Value;

                using (new SA_WindowBlockWithSpace(new GUIContent(title)))
                {
                    foreach (var pluginExporter in exporters)
                        using (new IMGUIBeginHorizontal())
                        {
                            EditorGUILayout.SelectableLabel(pluginExporter.ToString(), GUILayout.Height(16));
                            var pressed = GUILayout.Button("Export", EditorStyles.miniButton, GUILayout.Width(k_ButtonWidth));
                            if (pressed)
                            {
                                pluginExporter.UpgrageMinorVersion();
                                SA_PluginExportersManager.ExportPackage(pluginExporter);
                            }

                            pressed = GUILayout.Button("Release", EditorStyles.miniButton, GUILayout.Width(k_ButtonWidth));
                            if (pressed) SA_PluginExportersManager.ReleasePackage(pluginExporter);
                        }
                }
            }

            using (new SA_WindowBlockWithSpace(new GUIContent("Line Ending Resolver")))
            {
                using (new IMGUIBeginHorizontal())
                {
                    GUILayout.Label("Line Ending Resolver");
                    var pressed = GUILayout.Button("Resolve", EditorStyles.miniButton, GUILayout.Width(k_ButtonWidth));
                    if (pressed) SA_LineEndingResolver.Resolve();
                }
            }

            using (new IMGUIBeginHorizontal())
            {
                GUILayout.Label("Release all");
                var relesePressed = GUILayout.Button("Release all", EditorStyles.miniButton, GUILayout.Width(k_ButtonWidth));
                if (relesePressed) SA_PluginExportersManager.ReleaseAllPackages();
            }

            using (new SA_WindowBlockWithSpace(new GUIContent("Exporter Settings")))
            {
                EditorGUILayout.LabelField("Output folder: ");

                using (new IMGUIBeginHorizontal())
                {
                    SA_PluginExportersManager.BaseDir = EditorGUILayout.TextField(SA_PluginExportersManager.BaseDir);

                    var image = PluginsEditorSkin.GetGenericIcon("view.png");
                    using (new IMGUIChangeContentColor(SA_PluginSettingsWindowStyles.DefaultImageContentColor))
                    {
                        var view = GUILayout.Button(new GUIContent(string.Empty, image), GUILayout.Height(16), GUILayout.Width(24));
                        if (view)
                            FilesBrowser.OpenAtPath(SA_PluginExportersManager.BaseDir);
                    }
                }
            }
        }
    }
}
