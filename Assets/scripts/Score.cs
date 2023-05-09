using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Score : MonoBehaviour
{
    public TMP_Text ScoreText;
    public int score = 0;

    public void IncreaseScore(int value)
    {
        score += value;
        UpdateScore(score);
        Debug.Log("Player Score: " + score);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateScore(5);
        }
    }

    public void UpdateScore(int score)
    {
        ScoreText.text = score.ToString();
    }


}
