#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class OpenDefaultSceneOnProjectLaunch
{
    private const string DefaultScenePath =
        "Assets/Scenes/stormTemple_Main.unity";

    static OpenDefaultSceneOnProjectLaunch()
    {
        EditorApplication.delayCall += OpenDefaultSceneWhenNeeded;
    }

    private static void OpenDefaultSceneWhenNeeded()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode)
        {
            return;
        }

        if (AssetDatabase.LoadAssetAtPath<SceneAsset>(DefaultScenePath) == null)
        {
            return;
        }

        Scene activeScene = SceneManager.GetActiveScene();

        // 仅在首次打开、当前为 Untitled 空场景时自动加载主场景。
        if (string.IsNullOrEmpty(activeScene.path))
        {
            EditorSceneManager.OpenScene(
                DefaultScenePath,
                OpenSceneMode.Single
            );
        }
    }
}
#endif