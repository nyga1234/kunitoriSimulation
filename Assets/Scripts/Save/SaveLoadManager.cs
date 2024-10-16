using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    private static bool isSaving = true; // �Z�[�u�����[�h���𔻒f����t���O
    // �v���p�e�B�����J
    public  static bool IsSaving { get { return isSaving; } set { isSaving = value; }}

    private static string GetSavePath(int slot) => Application.persistentDataPath + $"/gamestate_slot{slot}.json";

    public static void SaveGame(GameState gameState, int slot)
    {
        string savePath = GetSavePath(slot);
        string json = JsonUtility.ToJson(gameState, true);
        File.WriteAllText(savePath, json);

        Debug.Log("Game Saved: " + savePath);
    }

    public static GameState LoadGame(int slot)
    {
        string savePath = GetSavePath(slot);
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            GameState gameState = JsonUtility.FromJson<GameState>(json);
            //Debug.Log("Game Loaded: " + savePath);
            return gameState;
        }
        else
        {
            Debug.LogWarning("Save file not found: " + savePath);
            return null;
        }
    }

    public static bool HasSaveData(int slot)
    {
        return File.Exists(GetSavePath(slot));
    }

    //// �Z�[�u�����[�h���̃��[�h���Z�b�g
    //public void SetMode(bool saving)
    //{
    //    isSaving = saving;
    //}
}
