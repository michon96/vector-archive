using System;
using System.Collections;
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
    [SerializeField] bool alreadyCorrect = false;

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
        alreadyCorrect = false;
        cardUIHandler = GetComponent<CardUIBehaviour>();
        cardUIHandler.EnableButton(true);
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
        if (GameManager.Instance.isCardCorrect(cardIndex) && !alreadyCorrect)
        {
            if (isResetable)
            {
                cardUIHandler.EnableButton(false);
                isResetable = false;
            }
            cardUIHandler.FlipCard(true);
            Debug.Log($"Card {cardProperty.name} is correct");
            alreadyCorrect = true;
        }
    }

    public void SelectCard()
    {
        //if (GameManager.Instance.GetLastSelected == transform.GetSiblingIndex() || !isSelectable)
        //{
        //    return;
        //}
        if (alreadyCorrect || cardUIHandler.isAnimating)
        {
            return;
        }
        Debug.Log($"Selected 1");
        GameManager.Instance.SetSelected(cardIndex, transform.GetSiblingIndex());
        cardUIHandler.FlipCard(true);
    }

    internal void ResetCard()
    {
        if (isResetable)
        {
            //StartCoroutine(DelayedReset());
            cardUIHandler.FlipCard(false);
        }
    }

    public IEnumerator DelayedReset()
    {
        isSelectable = false;
        yield return new WaitForSeconds(1f);
        cardUIHandler.FlipCard(false);
        isSelectable = true;

    }

}
