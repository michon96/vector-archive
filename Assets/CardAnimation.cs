using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardAnimation : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] float duration = 0.5f;
    [SerializeField] float waitTime = 1.0f; // How long it stays visible
    [Header("References")]
    [SerializeField] Image cardImage;
    [SerializeField] Sprite frontSprite;
    [SerializeField] Sprite backSprite;

    public bool isFaceUp = false;
    public bool isAnimating = false;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FlipCard();
        }
    }
    public void FlipCard()
    {
        if (!isAnimating) StartCoroutine(RotateSequence(1));
    }
    // Call this to flip up, wait, then flip back (Memory style)
    public void ShowAndHide()
    {
        if (!isAnimating) StartCoroutine(RotateSequence(2));
    }

    IEnumerator RotateSequence(int loopCount)
    {
        isAnimating = true;

        for (int i = 0; i < loopCount; i++)
        {
            yield return StartCoroutine(PerformRotation());

            // If we are doing a double flip, wait before flipping back
            if (loopCount > 1 && i == 0)
            {
                yield return new WaitForSeconds(waitTime);
            }
        }

        isAnimating = false;
    }

    IEnumerator PerformRotation()
    {
        float time = 0;
        Quaternion startRotation = cardImage.rectTransform.transform.localRotation;
        Quaternion endRotation = cardImage.rectTransform.localRotation * Quaternion.Euler(0, 180, 0);

        bool spriteSwapped = false;

        while (time < duration)
        {
            time += Time.deltaTime;
            float progress = time / duration;

            cardImage.rectTransform.localRotation = Quaternion.Slerp(startRotation, endRotation, progress);

            // Swap sprite at the "edge-on" moment (90 degrees)
            if (!spriteSwapped && progress >= 0.5f)
            {
                spriteSwapped = true;
                isFaceUp = !isFaceUp;
                cardImage.sprite = isFaceUp ? frontSprite : backSprite;
            }
            yield return null;
        }

        cardImage.rectTransform.localRotation = endRotation;
    }
}