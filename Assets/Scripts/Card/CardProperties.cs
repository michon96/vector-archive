using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardProperties : MonoBehaviour
{

    [Serializable]
    public class Card
    {
        public string name;
        public Sprite image;
    }

    public CardFaceLoader cardFaceLoader;
    public Card[] _cards;
    string[] fruitNames = {
    "Red Apple", "Green Grapes", "Watermelon", "Pineapple", "Strawberry", "Green Apple", "Orange",
    "Bananas", "Tangerine", "Cherries", "Lemon", "Pear", "Coconut", "Red Plum",
    "Kiwi", "Blueberries", "Peach", "Purple Plum", "Mango", "Papaya", "Dragon Fruit",
    "Lime", "Avocado", "Raspberry", "Fig", "Pomegranate", "Passion Fruit", "Blackberry"
    };

    public Card[] Cards => _cards;

    [ContextMenu("Get Cards")]
    public void InitCards()
    {
        var cardSprites = cardFaceLoader.GetSprites();
        _cards = new Card[cardSprites.Length];
        int index = 0;
        foreach (var item in cardSprites)
        {
            var newCard = new Card();
            newCard.name = fruitNames[index];
            newCard.image = item;
            _cards[index] = newCard;
            index++;
        }
    }

    public Card[] GetCards(int p_totalCards)
    {
        int uniqueNeeded = p_totalCards / 2;
        Card[] selectedCards = new Card[uniqueNeeded];

        List<int> pool = new List<int>();

        for (int i = 0; i < uniqueNeeded; i++)
        {
            if (pool.Count == 0)
            {
                for (int j = 0; j < _cards.Length; j++)
                {
                    pool.Add(j);
                }
                ShuffleList(pool);
            }

            int indexToUse = pool[0];
            selectedCards[i] = _cards[indexToUse];

            pool.RemoveAt(0);
        }

        return selectedCards;
    }

    private void ShuffleList(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int temp = list[i];
            int randomIndex = UnityEngine.Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

}
