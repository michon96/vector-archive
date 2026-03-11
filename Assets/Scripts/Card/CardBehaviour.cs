using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CardUIBehaviour))]
public class CardBehaviour : MonoBehaviour
{
    [SerializeField] CardUIBehaviour cardAnimation;
    [SerializeField] CardProperties.Card cardProperty;
    [SerializeField] Image cardImage;
    [SerializeField] int cardIndex;
    [SerializeField] bool isResetable = true;
    [SerializeField] bool isSelectable = true;


    void Start()
    {
        isSelectable = true;
        isResetable = true;

        cardAnimation = GetComponent<CardUIBehaviour>();
        cardAnimation.EnableButton(isSelectable);

        GameManager.Instance.OnAnswerCorrect += OnAnswerCorrect;
    }

    private void OnAnswerCorrect(int p_correctIndex)
    {
        if (p_correctIndex == cardIndex && isResetable)
        {
            isResetable = false;
            isSelectable = false;
            cardAnimation.EnableButton(isSelectable);
        }
    }

    public void SelectCard()
    {
        cardAnimation.FlipCard();
        GameManager.Instance.SetSelected(cardIndex);
    }

    internal void ResetCard()
    {
        if (isResetable)
        {
            cardAnimation.FlipCard(false);
        }
    }

    internal void SetCardProperty(CardProperties.Card p_card, int p_index)
    {
        cardProperty = p_card;
        cardImage.sprite = cardProperty.image;
        cardAnimation.SetCardSprites(p_card.image);
        cardIndex = p_index;
    }

    public int GetCardID()
    {
        return cardIndex;
    }
}
