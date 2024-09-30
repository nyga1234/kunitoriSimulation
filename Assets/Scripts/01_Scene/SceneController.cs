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
    /// [�񓯊�] �w��V�[�������Z�œW�J
    /// </summary>
    /// <param name="sceneName">�V�[����</param>
    public void Open(string sceneName, Action onComplate = null)
    {
        StartCoroutine(ILoad(sceneName, onComplate));
    }

    /// <summary>
    /// �V�[���ǂݍ��݂̔񓯊�����
    /// </summary>
    /// <param name="sceneName">�V�[����</param>
    /// <returns></returns>
    private IEnumerator ILoad(string sceneName, Action onComplate)
    {
        var load = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        yield return load;
        onComplate?.Invoke();
    }

    /// <summary>
    /// [�񓯊�] �w��V�[�����J��
    /// </summary>
    /// <param name="sceneName">�V�[����</param>
    public void Close(string sceneName, Action onComplate = null)
    {
        StartCoroutine(IUnLoad(sceneName, onComplate));
    }

    /// <summary>
    /// �V�[���J���̔񓯊�����
    /// </summary>
    /// <param name="sceneName">�V�[����</param>
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
    /// ���ɓǂݍ��܂�Ă���V�[�����ǂ����𒲂ׂ�
    /// </summary>
    /// <param name="sceneName">�T�������V�[����</param>
    /// <returns>�ǂݍ��܂�Ă���� true</returns>
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