using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CardAnimation))]
public class CardBehaviour : MonoBehaviour
{
    [SerializeField] CardAnimation cardAnimation;
    [SerializeField] CardProperties.Card cardProperty;
    [SerializeField] Image cardImage;

    void Start()
    {
        cardAnimation = GetComponent<CardAnimation>();
    }

    public void SelectCard()
    {
        cardAnimation.FlipCard();
        GameManager.Instance.SetSelected(transform.GetSiblingIndex());
    }

    public void HideCard()
    {

    }

    internal void ResetCard()
    {
        cardAnimation.FlipCard(false);
    }

    internal void SetCardProperty(CardProperties.Card p_card)
    {
        cardProperty = p_card;
        cardImage.sprite = cardProperty.image;
        cardAnimation.SetCardSprites(p_card.image);
    }
}
