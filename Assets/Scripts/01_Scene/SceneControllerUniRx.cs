using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public partial class SceneController
{
    public async UniTask SwitchPrimaryScene(string before, string next)
    {
        // Unload Main Scene
        await SceneManager.UnloadSceneAsync(before);

        // Unload Sub Scene
        while (LoadedSceneCount > 1)
        {
            await SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(LoadedSceneCount - 1).name);
        }

        // Load New Main Scene
        await SceneManager.LoadSceneAsync(next, LoadSceneMode.Additive);
    }

    public static async UniTask UnloadSceneAsync(string sceneName)
    {
        if (AlreadyLoadScene(sceneName) == true)
        {
            await SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}