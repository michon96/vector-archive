using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CardUIBehaviour))]
public class CardBehaviour : MonoBehaviour
{
    [SerializeField] CardUIBehaviour cardUIHandler;
    [SerializeField] CardProperties.Card cardProperty;
    [SerializeField] Image cardImage;
    [SerializeField] int cardIndex;
    [SerializeField] bool isResetable = true;
    [SerializeField] bool isSelectable = true;

    public int GetCardID()
    {
        return cardIndex;
    }
    
    internal void SetCardProperty(CardProperties.Card p_card, int p_index)
    {
        cardProperty = p_card;
        cardImage.sprite = cardProperty.image;
        cardUIHandler.SetCardSprites(p_card.image);
        cardIndex = p_index;
    }

    void Start()
    {
        isSelectable = true;
        isResetable = true;

        cardUIHandler = GetComponent<CardUIBehaviour>();
        cardUIHandler.EnableButton(isSelectable);

        GameManager.Instance.OnAnswerCorrect += OnAnswerCorrect;
        GameManager.Instance.OnGameEnd += OnEndGame;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameEnd -= OnEndGame;
        GameManager.Instance.OnAnswerCorrect -= OnAnswerCorrect;
    }

    private void OnEndGame()
    {
        cardUIHandler.StopAllCoroutines();
    }

    private void OnAnswerCorrect(int p_correctIndex)
    {
        if (p_correctIndex == cardIndex && isResetable)
        {
            cardUIHandler.HideCard();
            //isResetable = false;
            //isSelectable = false;
            //cardAnimation.EnableButton(isSelectable);
        }
    }

    public void SelectCard()
    {
        cardUIHandler.FlipCard();
        GameManager.Instance.SetSelected(cardIndex);
    }

    internal void ResetCard()
    {
        if (isResetable)
        {
            cardUIHandler.FlipCard(false);
        }
    }
  
}
