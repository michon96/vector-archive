using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    [Header("Dependencies")]
    public LevelSelectHandler levelSelectHandler;
    public GameUIHandler gameUIHandler;
    public CardFaceLoader cardFaceLoader;
    [Space]
    [Header("Selected")]
    public int previouslySelected = -1;
    public int currentSelected = -1;

    public int selectedCards = 0;
    public int[] selected = new int[2];
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
    public Action<int> OnAnswerCorrect;
    public Action<int> Reset;

    public void SetSelected(int p_index)
    {
        UpdateTurn();
        currentSelected = p_index;
        if (currentSelected != previouslySelected)
        {
            previouslySelected = currentSelected;
            //is two open;
        }
        else
        {
            //is correct
            Debug.Log($"Correct");
            UpdateScore();
            OnAnswerCorrect?.Invoke(currentSelected);
            //return;
        }
      
        //selected[selectedCards >= 2 ? 0 : selectedCards] = currentSelected;
        selectedCards++;
        if (selectedCards == 1)
        {
            ResetSelection();
        }

        //if (turns %3 ==0)
        //{
        //    ResetSelection();
        //}

        //if (selectedCards >= 2)
        //{
        //    if (selected[0]==selected[1])
        //    {
        //        Debug.Log($"Correct");
        //    }
        //}
    }

    private void UpdateScore()
    {
        score++;
        gameUIHandler.UpdateScore(score);   
    }

    private void ResetSelection()
    {
        gameUIHandler.ResetCards();
        selected = new int[2];
        previouslySelected = -1;
        selectedCards = -1;
    }

    public void UpdateTurn()
    {
        turns++;
        gameUIHandler.UpdateTurn(turns);
    }

    #region LifeCycles
    private void OnEnable()
    {
        //GameStart();
    }
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

    #endregion

    #region Game Phases

    public void GameStart()
    {
        score = 0;
        turns = 0;

        selectedCards = 0;
        selected = new int[2];

        previouslySelected = -1;
        currentSelected = -1;

        cardFaceLoader.PopulateGrid(levelSelectHandler.GetSelectedLevel());
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
    public void ResetGame()
    {
        UIManager.Instance.ChangeStatus(1);
        GameStart();
    }
}
