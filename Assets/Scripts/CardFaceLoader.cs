using UnityEngine;
using UnityEngine.UI;

public class CardFaceLoader : MonoBehaviour
{
    [Header("Dependencies")]
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
   

    void Start()
    {
        PopulateGrid();
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

    public void PopulateGrid()
    {
        ClearGrid();
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
        for (int i = 0; i < totalCards; i++)
        {
            GameObject newCard = Instantiate(cardPrefab, rectTransform);
            newCard.name = $"Card_{i}";
        }
    }

}
