using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreItem : MonoBehaviour
{
    public Text level, score, turns;

    public void InitItem(int p_lvl, int p_score, int p_turns)
    {
        level.text = "Level: " + p_lvl;
        score.text = "Score: " + p_score;
        turns.text = "Turns: " + p_turns;
    }

}
