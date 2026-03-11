using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardAnimation))]
public class CardBehaviour : MonoBehaviour
{
    public enum CardType
    {
        Apple, 
        Banana
    }
    [SerializeField] CardAnimation cardAnimation;
    // Start is called before the first frame update
    void Start()
    {
        cardAnimation = GetComponent<CardAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        
    }

    public void SelectCard()
    {
        cardAnimation.FlipCard();
    }
}
