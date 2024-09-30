using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class SaveSelectModel : MonoBehaviour
{
    public async UniTask OnPressClose()
    {
        await SceneController.UnloadAsync(nameof(SaveLoadUI));
    }

    public void OnPressSaveSelect(int slot)
    {
        GameManager.instance.ChangeScene("Title", "GameMain");
    }
}
