using System;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    [Header("Dependencies")]
    [SerializeField] HighScoreHandler highScoreHandler;
    [SerializeField] SavedDataHandler saveDataHandler;
    [SerializeField] LevelSelectHandler levelSelectHandler;
    [SerializeField] GameUIHandler gameUIHandler;
    [SerializeField] CardFaceLoader cardFaceLoader;
    [Space]
    [Header("Selected")]
    [SerializeField] int currentSelected = -1;
    [SerializeField] int lastSelected = -1;
    [SerializeField] int lastSelectedObject = -1;

    [Header("Score")]
    [Space]
    [SerializeField] int score;
    [SerializeField] int turns;
    [SerializeField] float scoreComboMultiplier = 1.5f;
    [Space]
    [Header("Debug")]
    [SerializeField] bool startGameOnStart = true;
    public List<int> correctAnswers;

    public Action<int> selectedIndex;
    public Action<int> OnAnswerCorrect;
    public Action<int> Reset;
    public Action OnGameEnd;

    private bool _isGameOver = false;
    public bool IsGameOver => _isGameOver;

    public int GetLastSelected => lastSelectedObject;

    public bool isCardCorrect(int p_index)
    {
        return correctAnswers.Contains(p_index);
    }

    public void SetSelected(int p_index, int p_siblingIndex)
    {
        UpdateTurn();
        //Debug.Log($"Selected 2");
        lastSelectedObject = p_siblingIndex;
        lastSelected = currentSelected;
        currentSelected = p_index;

        if (currentSelected == lastSelected && !isCardCorrect(p_index))
        {
            UpdateScore();
            //Debug.Log($"Correct");
            correctAnswers.Add(p_index);
            if (correctAnswers.Count >= levelSelectHandler.GetTotalScore())
                EndGame();
            OnAnswerCorrect?.Invoke(p_index);

            currentSelected = -1;
            lastSelected = -1;
            lastSelectedObject = -1;
            gameUIHandler.ResetCards();
            return;
        }
        else
        {
            //Debug.Log($"Incorrect");
            gameUIHandler.ResetCards();
        }
    }
    public void SetSelected(CardBehaviour p_cardBehaviour, int p_siblingIndex)
    {
        UpdateTurn();
        Debug.Log($"Selected 2");
        lastSelectedObject = p_siblingIndex;
        lastSelected = currentSelected;
        currentSelected = p_cardBehaviour.GetCardID();

        if (currentSelected == lastSelected)
        {
            UpdateScore();
            Debug.Log($"Correct");
            //p_cardBehaviour.Fl
            if (correctAnswers.Count >= levelSelectHandler.GetTotalScore())
                EndGame();
            return;
        }
        else
        {
            Debug.Log($"Incorrect");
            gameUIHandler.ResetCards();
        }
    }

    private void UpdateScore()
    {
        score++;
        gameUIHandler.UpdateScore(score);
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
        correctAnswers = new List<int>();
        _isGameOver = false;
        score = 0;
        turns = 0;

        currentSelected = -1;
        lastSelected = -1;
        lastSelectedObject = -1;

        cardFaceLoader.PopulateGrid(levelSelectHandler.GetSelectedLevelDimensions());
        gameUIHandler.UpdateScore(score);
        gameUIHandler.UpdateTurn(turns);
    }

    public void EndGame()
    {
        _isGameOver = true;
        gameUIHandler.UpdateFinalScore(score);
        gameUIHandler.UpdateFinalTurns(turns);
        
        GameData data = new GameData();
        data.turns = turns;
        data.score = score;
        data.level = (int)levelSelectHandler.GetSelectedLevel;
        
        saveDataHandler.SaveGame(data);
        highScoreHandler.ShowHighScores();
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
