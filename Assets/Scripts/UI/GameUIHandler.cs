using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIHandler : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] Text turnText;
    [Space]
    [SerializeField] Text finalScoreText;
    [SerializeField] Text finalTurnsText;
    [SerializeField] RectTransform cardParent;

    public void UpdateTurn(int p_turn)
    {
        turnText.text = $"Turn: {p_turn}";
    }

    public void UpdateScore(int p_score)
    {
        scoreText.text = $"Matches: {p_score}";
    }
    public void UpdateFinalScore(int p_score)
    {
        finalScoreText.text = $"Final Score: {p_score}";
    }
    public void UpdateFinalTurns(int p_turn)
    {
        finalTurnsText.text = $"Turns: {p_turn}";
    }

    [ContextMenu("Reset Cards")]
    public void ResetCards()
    {
        int totalChildren = cardParent.childCount;
        for (int childIndex = 0; childIndex < totalChildren; childIndex++)
        {
            var child = cardParent.GetChild(childIndex).gameObject;
            child.GetComponent<CardBehaviour>().ResetCard();
        }
    }
}
