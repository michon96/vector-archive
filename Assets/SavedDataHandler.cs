using System.Collections.Generic;
using UnityEngine;

using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class GameData
{
    public int level = 1;
    public int score = 0;
    public int turns = 0;
}

public class SavedDataHandler : MonoBehaviour
{
    private const string SAVE_KEY = "PlayerGameData";
    public GameData activeData = new GameData();

    private void Awake()
    {
        LoadGame();
    }

    private void Start()
    {
        var loadedData = GetAllLevelScores(5);
    }

    public void SaveGame(GameData p_gameData)
    {
        string jsonData = JsonUtility.ToJson(p_gameData);
        PlayerPrefs.SetString(SAVE_KEY + p_gameData.level, jsonData);
        PlayerPrefs.Save();
        Debug.Log($"Saved data for level {p_gameData.level}: {jsonData}");
    }

    [ContextMenu("Save Active Data")]
    public void SaveGame()
    {
        string jsonData = JsonUtility.ToJson(activeData);
        PlayerPrefs.SetString(SAVE_KEY, jsonData);
        PlayerPrefs.Save();
    }

    public GameData LoadLevelData(int level)
    {
        string key = SAVE_KEY + level;
        //Debug.Log($"Loading Saved Data: {level}");
        if (PlayerPrefs.HasKey(key))
        {
            string jsonData = PlayerPrefs.GetString(key);
            //Debug.Log($"Success Loaded Saved Data: {jsonData}");
            return JsonUtility.FromJson<GameData>(jsonData);
        }
        Debug.Log($"Failed Loading Saved Data: ");
        return null;
    }

    [ContextMenu("Load General Save")]
    public void LoadGame()
    {
        if (PlayerPrefs.HasKey(SAVE_KEY))
        {
            string jsonData = PlayerPrefs.GetString(SAVE_KEY);
            activeData = JsonUtility.FromJson<GameData>(jsonData);
        }
        else
        {
            activeData = new GameData();
        }
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteKey(SAVE_KEY);
        activeData = new GameData();
    }

    public List<GameData> GetAllLevelScores(int maxLevels = 50)
    {
        List<GameData> allScores = new List<GameData>();

        for (int i = 0; i <= maxLevels; i++)
        {
            GameData data = LoadLevelData(i);
            if (data != null)
            {
                allScores.Add(data);
            }
        }
        allScores = allScores.OrderByDescending(s => s.score).ToList();
        allScores.Reverse();
        return allScores;
    }
}