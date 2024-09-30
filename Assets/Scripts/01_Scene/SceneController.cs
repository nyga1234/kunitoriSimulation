using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;

public partial class SceneController : SingletonMonoBehaviour<SceneController>
{
    private string _mainScene = string.Empty;

    /// <summary>
    /// [非同期] 指定シーンを加算で展開
    /// </summary>
    /// <param name="sceneName">シーン名</param>
    public void Open(string sceneName, Action onComplate = null)
    {
        StartCoroutine(ILoad(sceneName, onComplate));
    }

    /// <summary>
    /// シーン読み込みの非同期処理
    /// </summary>
    /// <param name="sceneName">シーン名</param>
    /// <returns></returns>
    private IEnumerator ILoad(string sceneName, Action onComplate)
    {
        var load = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        yield return load;
        onComplate?.Invoke();
    }

    /// <summary>
    /// [非同期] 指定シーンを開放
    /// </summary>
    /// <param name="sceneName">シーン名</param>
    public void Close(string sceneName, Action onComplate = null)
    {
        StartCoroutine(IUnLoad(sceneName, onComplate));
    }

    /// <summary>
    /// シーン開放の非同期処理
    /// </summary>
    /// <param name="sceneName">シーン名</param>
    /// <returns></returns>
    private IEnumerator IUnLoad(string sceneName, Action onComplate)
    {
        var unload = SceneManager.UnloadSceneAsync(sceneName);
        yield return unload;
        yield return Resources.UnloadUnusedAssets();
        onComplate?.Invoke();
    }

    public static async UniTask LoadAsync(string sceneName)
    {
        await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    public static async UniTask UnloadAsync(string sceneName)
    {
        await SceneManager.UnloadSceneAsync(sceneName);
        await Resources.UnloadUnusedAssets();
    }

    /// <summary>
    /// 既に読み込まれているシーンかどうかを調べる
    /// </summary>
    /// <param name="sceneName">探したいシーン名</param>
    /// <returns>読み込まれていれば true</returns>
    public static bool AlreadyLoadScene(string sceneName)
    {
        for (var i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == sceneName)
            {
                return true;
            }
        }
        return false;
    }

    public static int LoadedSceneCount => SceneManager.sceneCount;

    #region Active Scene Stack
    [SerializeField]
    private List<string> _stack = new List<string>();
    public List<string> Stack => _stack;
    public string Active => _stack.Last();
    #endregion
}