using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreHandler : MonoBehaviour
{
    public SavedDataHandler savedDataHandler;
    public GameObject highScorePrefab;
    public Transform highScoreContainer;
    private void Start()
    {
        // Example usage
        ClearHighScores();
        InitTopScores(savedDataHandler.GetAllLevelScores(5));
        //InitTopScores(new List<GameData>()
        //{
        //        new GameData() { level = 1, score = 100, turns = 10 },
        //        new GameData() { level = 2, score = 150, turns = 15 },
        //        new GameData() { level = 3, score = 200, turns = 20 }
        //});
    }

    public void ShowHighScores()
    {
        ClearHighScores();
        InitTopScores(savedDataHandler.GetAllLevelScores(5));
    }
    public void ClearHighScores()
    {
        foreach (Transform child in highScoreContainer)
        {
            Destroy(child.gameObject);
        }
    }

    public void InitTopScores(List<GameData> p_topScores)
    {
        foreach (GameData data in p_topScores)
        {
            GameObject newHighScore = Instantiate(highScorePrefab, highScoreContainer);
            var entry = newHighScore.GetComponent<HighScoreItem>();
            entry.InitItem(data.level + 1, data.score, data.turns);
        }
    }
}
