using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    [Header("Dependencies")]
    public GameUIHandler gameUIHandler;
    public CardFaceLoader cardFaceLoader;
    [Space]
    [Header("Selected")]
    public int previouslySelected = -1;
    public int currentSelected = -1;
    public int selectedCards = 0;
    public Vector2 selected = Vector2.zero;
    [Header("Score")]
    [Space]
    public int score;
    public int turns;
    public float scoreComboMultiplier = 1.5f;
    [Space]
    [Header("Debug")]
    public bool startGameOnStart = true;

    private Queue<int> selectQueue;

    public Action<int> selectedIndex;
    public Action<int> Reset;

    public void SetSelected(int p_index)
    {
        UpdateTurn();
        selectedCards++;
        if (selectedCards > 2)
        {
            gameUIHandler.ResetCards();
            selectedCards = 1;
        }

        currentSelected = p_index;
        if (previouslySelected < 0)
        {
            previouslySelected = p_index;
        }
    }

    public void UpdateTurn()
    {
        turns++;
        gameUIHandler.UpdateTurn(turns);
    }

    #region LifeCycles

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start()
    {
        if (startGameOnStart)
        {
            GameStart();
        }
    }

    void Update()
    {

    }
    #endregion

    #region Game Phases

    public void GameStart()
    {
        score = 0;
        turns = 0;

        selectedCards = 0;

        previouslySelected = -1;
        currentSelected = -1;

        cardFaceLoader.PopulateGrid();
        gameUIHandler.UpdateScore(score);
        gameUIHandler.UpdateTurn(turns);
    }

    public void EndGame()
    {

    }
    #endregion

    public void CheckCorrect()
    {

    }

    public void Combo()
    {

    }
}
