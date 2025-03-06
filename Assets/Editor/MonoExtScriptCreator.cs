using UnityEngine;
using UnityEditor;
using System.IO;

public static class MonoExtScriptCreator
{
    private const string TEMPLATE_PATH = "Assets/Editor/MonoExtBaseClass.txt";

    [MenuItem("Assets/Create/Create a MonoExt Script", false, 0)]
    public static void CreateMyCustomScript()
    {
        // This will create a new file in the selected folder with this default name,
        // and immediately highlight it for renaming.
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(
            TEMPLATE_PATH,
            "NewMonoExtScript.cs"
        );
    }
}
