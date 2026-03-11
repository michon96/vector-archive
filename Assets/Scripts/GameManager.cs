using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Dependencies")]
    public GameUIHandler gameUIHandler;
    public CardFaceLoader cardFaceLoader;
    [Space]
    public int score;
    public int turns;

    public Queue selectQueue;

    #region LifeCycles

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    #endregion

    #region Game Phases

    public void GameStart()
    {

    }
    #endregion

    public void CheckCorrect()
    {

    }
}
