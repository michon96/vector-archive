using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CardAnimation))]
public class CardBehaviour : MonoBehaviour
{
    public enum CardType
    {
        Apple, 
        Banana
    }

    [SerializeField] CardAnimation cardAnimation;
    [SerializeField] Image cardImage;

    // Start is called before the first frame update
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
}
