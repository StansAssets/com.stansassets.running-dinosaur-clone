using UnityEditor;

namespace SA.CrossPlatform.Editor
{
    class UM_AssetPostprocessor : AssetPostprocessor
    {
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            foreach (var assetPath in importedAssets)
                UM_DefinesResolver.ProcessAssetImport(assetPath);

            foreach (var assetPath in deletedAssets)
                UM_DefinesResolver.ProcessAssetDelete(assetPath);
        }
    }
}
