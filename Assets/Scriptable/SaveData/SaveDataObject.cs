using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Save Data", fileName = "SaveDataObject")]
public class SaveDataObject : ScriptableObject
{
    [SerializeField]
    private float _volume_BGM;
    [SerializeField]
    private float _volume_SFX;
    [SerializeField]
    private uint _progress;

    public float VolumeBGM { get { return _volume_BGM; } set { _volume_BGM = value; } }
    public float VolumeSFX { get { return _volume_SFX; } set { _volume_SFX = value; } }
    public uint Progress { get { return _progress; } set { _progress = value; } }

    public string ToJson() => JsonUtility.ToJson(this);
    public bool FromJson(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);

        var isDefault = this._progress == default(int);

        if (isDefault == true)
        {
            Init();
        }

        return isDefault;
    }
    public void Init()
    {
        _volume_BGM = 0f;
        _volume_SFX = 0f;
        _progress = 1;
    }
}