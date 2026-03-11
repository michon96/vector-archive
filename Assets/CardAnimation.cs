using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardAnimation : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] float duration = 0.5f;

    [Header("References")]
    [SerializeField] Image cardImage;
    [SerializeField] Sprite frontSprite;
    [SerializeField] Sprite backSprite;

    private bool isFaceUp = false;
    private bool isAnimating = false;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FlipCard();
        }
    }

    public void FlipCard()
    {
        if (!isAnimating)
        {
            StartCoroutine(RotateCard());
        }
    }

    IEnumerator RotateCard()
    {
        isAnimating = true;

        float time = 0;
        Quaternion startRotation = transform.localRotation;
        // We want to rotate 180 degrees from wherever we currently are
        Quaternion endRotation = transform.localRotation * Quaternion.Euler(0, 180, 0);

        while (time < duration)
        {
            time += Time.deltaTime;
            float progress = time / duration;

            // Rotate the card
            transform.localRotation = Quaternion.Slerp(startRotation, endRotation, progress);

            // Mid-way point: Swap the sprite so it looks like the other side
            if (progress >= 0.5f)
            {
                cardImage.sprite = isFaceUp ? backSprite : frontSprite;
            }

            yield return null;
        }

        // Ensure we land exactly at the target rotation
        transform.localRotation = endRotation;
        isFaceUp = !isFaceUp;
        isAnimating = false;
    }
}