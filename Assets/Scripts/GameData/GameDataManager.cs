using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public static class GameDataManager
{
    private static readonly string FilePath = Path.Combine(Application.persistentDataPath, "gamedata.json");

    // Save the game data to a file
    public static void SaveGame(GameData data)
    {
        try
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(FilePath, json);
            Debug.Log("Game data saved successfully.");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to save game data: {ex.Message}");
        }
    }

    // Load the game data from a file
    public static GameData LoadGame()
    {
        try
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                GameData data = JsonUtility.FromJson<GameData>(json);
                Debug.Log("Game data loaded successfully.");
                return data;
            }
            else
            {
                Debug.LogWarning("No game data file found. Returning new data.");
                return new GameData(1, 6, 0, 0,  false, DateTime.Now, true, true, new List<int> {1}); // Default data if no file found
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to load game data: {ex.Message}");
            return null; // Return null if loading failed
        }
    }

    // Delete the saved game data file
    public static void DeleteGameData()
    {
        try
        {
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
                Debug.Log("Game data deleted successfully.");
            }
            else
            {
                Debug.LogWarning("No game data file to delete.");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to delete game data: {ex.Message}");
        }
    }
}
