using UnityEditor;
using SA.Foundation.Editor;

public static class DeveloperBuildDefine
{
    [InitializeOnLoadMethod]
    static void Init()
    {
        SA_EditorDefines.AddCompileDefine("SA_DEVELOPMENT_PROJECT");
    }
}
