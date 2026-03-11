using System;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    [Header("Dependencies")]
    [SerializeField] LevelSelectHandler levelSelectHandler;
    [SerializeField] GameUIHandler gameUIHandler;
    [SerializeField] CardFaceLoader cardFaceLoader;
    [Space]
    [Header("Selected")]
    [SerializeField] int previouslySelected = -1;
    [SerializeField] int currentSelected = -1;

    public int selectedCards = 0;
    [Header("Score")]
    [Space]
    [SerializeField] int score;
    [SerializeField] int turns;
    [SerializeField] float scoreComboMultiplier = 1.5f;
    [Space]
    [Header("Debug")]
    [SerializeField] bool startGameOnStart = true;

    private Queue<int> selectQueue;
    public Action<int> selectedIndex;
    public Action<int> OnAnswerCorrect;
    public Action<int> Reset;
    public Action OnGameEnd;

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
            if (score >= levelSelectHandler.GetTotalScore())
                EndGame();
            OnAnswerCorrect?.Invoke(currentSelected);
            return;
        }

        selectedCards++;
        //if (selectedCards == 1)
        //{
        //    Debug.Log($"select cards triggered the reset");
        //    ResetSelection();
        //}

        if (turns % 2 == 0)
        {
            Debug.Log($"turns  triggered the reset");
            ResetSelection();
        }
    }

    private void UpdateScore()
    {
        score++;
        gameUIHandler.UpdateScore(score);
    }

    private void ResetSelection()
    {
        gameUIHandler.ResetCards();
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

        previouslySelected = -1;
        currentSelected = -1;

        cardFaceLoader.PopulateGrid(levelSelectHandler.GetSelectedLevel());
        gameUIHandler.UpdateScore(score);
        gameUIHandler.UpdateTurn(turns);
    }

    public void EndGame()
    {
        gameUIHandler.UpdateFinalScore(score);
        UIManager.Instance.ChangeStatus(2);
        OnGameEnd?.Invoke();
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
