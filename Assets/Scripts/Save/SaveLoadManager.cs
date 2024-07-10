using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    private static string savePath = Application.persistentDataPath + "/gamestate.json";

    public static void SaveGame(GameState gameState)
    {
        string json = JsonUtility.ToJson(gameState, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Game Saved: " + savePath);
    }

    public static GameState LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            GameState gameState = JsonUtility.FromJson<GameState>(json);
            Debug.Log("Game Loaded: " + savePath);
            return gameState;
        }
        else
        {
            Debug.LogWarning("Save file not found: " + savePath);
            return null;
        }
    }
}
