using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CardUIBehaviour : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] float duration = 0.5f;
    [SerializeField] float waitTime = 1.0f; // How long it stays visible
    [Header("References")]
    [SerializeField] Image cardImage;
    [SerializeField] Button cardButton;
    [SerializeField] Sprite frontSprite;
    [SerializeField] Sprite backSprite;

    public bool isFaceUp = false;
    public bool isAnimating = false;

    #region LifeCycles

    private void Start()
    {
        cardImage.sprite = backSprite;
        isFaceUp = false;
        isAnimating = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FlipCard();
        }
    }

    #endregion

    #region Card Flipping

    public void FlipCard()
    {
        if (!isAnimating) StartCoroutine(RotateSequence(1));
    }

    public void FlipCard(bool p_front)
    {
        if (isFaceUp == p_front)
        {
            return;
        }
        if (!isAnimating) StartCoroutine(FlipTo(p_front));
    }

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
        isAnimating = true;
        float time = 0;
        Quaternion startRotation = cardImage.rectTransform.transform.localRotation;
        Quaternion endRotation = cardImage.rectTransform.localRotation * Quaternion.Euler(0, 180, 0);

        bool spriteSwapped = false;

        while (time < duration)
        {
            time += Time.deltaTime;
            float progress = time / duration;

            cardImage.rectTransform.localRotation = Quaternion.Slerp(startRotation, endRotation, progress);

            if (!spriteSwapped && progress >= 0.5f)
            {
                spriteSwapped = true;
                isFaceUp = !isFaceUp;
                cardImage.sprite = isFaceUp ? frontSprite : backSprite;
            }
            yield return null;
        }

        cardImage.rectTransform.localRotation = endRotation;
        isAnimating = false;
    }

    IEnumerator FlipTo(bool p_front)
    {
        isAnimating = true;
        float time = 0;
        Quaternion startRotation = cardImage.rectTransform.transform.localRotation;
        Quaternion endRotation = cardImage.rectTransform.localRotation * Quaternion.Euler(0, 180, 0);

        bool spriteSwapped = false;

        while (time < duration)
        {
            time += Time.deltaTime;
            float progress = time / duration;

            cardImage.rectTransform.localRotation = Quaternion.Slerp(startRotation, endRotation, progress);

            if (!spriteSwapped && progress >= 0.5f)
            {
                spriteSwapped = !p_front;
                isFaceUp = p_front;
                cardImage.sprite = p_front ? frontSprite : backSprite;
            }
            yield return null;
        }

        cardImage.rectTransform.localRotation = endRotation;
        isAnimating = false;
    }
    #endregion

    #region UI
    public void SetCardSprites(Sprite p_front)
    {
        frontSprite = p_front;
    }

    public void EnableButton(bool p_enable)
    {
        cardButton.enabled = p_enable;
    }

    #endregion

}