using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [Header("Initialize Scene Setting")]
    [SerializeField]
    private string _initializeLoadScene;

    [Header("Loading Canvas")]
    [SerializeField]
    private LoadingOverlay _loading;

    public LoadingOverlay Loading => _loading;

    private SceneController _sceneController;

    public void Awake()
    {
        _sceneController = SceneController.instance;
    }

    public void Start()
    {
        // Load First Scene
        if (_initializeLoadScene != string.Empty && SceneController.LoadedSceneCount <= 1)
        {
            _sceneController.Open(_initializeLoadScene);
        }
    }

    public async void ChangeScene(string before, string next)
    {
        await _loading.Display();
        await _sceneController.SwitchPrimaryScene(before, next);
    }
}
