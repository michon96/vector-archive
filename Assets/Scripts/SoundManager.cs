using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource bgm;
    [SerializeField] AudioSource sfx;
    // Start is called before the first frame update
    void Start()
    {
        bgm.Play();
        bgm.loop = true;
        GameManager.Instance.OnAnswerCorrect += OnAnswerCorrect;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnAnswerCorrect -= OnAnswerCorrect;
    }

    private void OnAnswerCorrect(int obj)
    {
        sfx.Play();
    }

    
}
