using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CardFaceLoader : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] CardProperties globalCardValues;
    [SerializeField] RectTransform rectTransform;
    [SerializeField] GridLayoutGroup gridGroup;
    [Space]
    [Header("Card Settings")]
    [SerializeField] GameObject cardPrefab;
    [SerializeField] Sprite[] cardFaces;
    [Space]
    [Header("Grid Settings")]
    [SerializeField] int rows = 4;
    [SerializeField] int columns = 4;
    [SerializeField] Vector2 spacing = new Vector2(10, 10);
    //[SerializeField] Vector4 padding = new Vector4(10,10,10,10);

    public bool IsCardTotalEven()
    {
        int totalCards = rows * columns;
        if (totalCards % 2 > 0)
        {
            Debug.LogError($"Error: Total Cards must be divisible by 2");
            return false;
        }
        return true;
    }

    [ContextMenu("Clear Children")]
    public void ClearGrid()
    {
        int totalChildren = rectTransform.childCount;
        for (int childIndex = 0; childIndex < totalChildren; childIndex++)
        {
            var child = rectTransform.GetChild(childIndex).gameObject;
            Destroy(child);
        }
    }

    public void PopulateGrid(int[] p_dimension)
    {

        if (!p_dimension.Equals(new int[2]))
        {
            rows = (int)p_dimension[0];
            columns = (int)p_dimension[1];
            Debug.Log($"Using Selected Values {rows}x{columns}");
        }
        else
        {
            Debug.Log($"Using Default Values {rows}x{columns}");
        }

        ClearGrid();
        if (!IsCardTotalEven())
            return;

        //get parent area
        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        //calculate dimensions
        float totalSpacingWidth = spacing.x * (columns - 1);
        float totalSpacingHeight = spacing.y * (rows - 1);

        //gets the values from the padding
        float paddingWidth = gridGroup.padding.left + gridGroup.padding.right;
        float paddingHeight = gridGroup.padding.top + gridGroup.padding.bottom;
        //float paddingWidth = padding.w + padding.y;
        //float paddingHeight = padding.y + padding.z;

        //final dimensions
        float cellWidth = (parentWidth - paddingWidth - totalSpacingWidth) / columns;
        float cellHeight = (parentHeight - paddingHeight - totalSpacingHeight) / rows;

        gridGroup.cellSize = new Vector2(cellWidth, cellHeight);
        gridGroup.spacing = spacing;

        //set based on column
        gridGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridGroup.constraintCount = columns;

        int totalCards = rows * columns;

        //first we get from cardProperties. 
        //basically get total/2 => number of Cards. then we receive the list. then spawn them below.
        //then we can shuffle the list and assign them to the cards.

        var shuffledCards = globalCardValues.GetCards(totalCards);
        //if 12 => 6 unique cards. we get 6 cards from the cardProperties. then we shuffle them and assign them to the cards.
        //we can shuffle the list and assign them to the cards.

        for (int i = 0; i < shuffledCards.Length; i++)
        {
            //spawn twice
            GameObject newCard = Instantiate(cardPrefab, rectTransform);
            GameObject newCardPair = Instantiate(cardPrefab, rectTransform);

            newCard.GetComponent<CardBehaviour>().SetCardProperty(shuffledCards[i], i);
            newCardPair.GetComponent<CardBehaviour>().SetCardProperty(shuffledCards[i], i);

            //newCard.GetComponent<CardBehaviour>().cardImage.sprite = cardFaces[i % cardFaces.Length];
            newCard.name = $"Card_{shuffledCards[i].name}";
            newCardPair.name = $"Card_{shuffledCards[i].name}";

        }

        ShuffleGrid();
    }

    public void ShuffleGrid()
    {
        int childCount = rectTransform.childCount;

        for (int childIndex = 0; childIndex < childCount; childIndex++)
        {
            int randomIndex = UnityEngine.Random.Range(childIndex, childCount);
            rectTransform.GetChild(childIndex).SetSiblingIndex(randomIndex);
        }
    }

    internal Sprite[] GetSprites()
    {
        return cardFaces;
    }
}
