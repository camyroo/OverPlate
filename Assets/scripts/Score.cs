using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Score : MonoBehaviour
{
    public TMP_Text ScoreText;
    PlayerController player;

    public int score = 0;

    //public event System.Action<int> OnScoreChanged;

    public void IncreaseScore(int value) {
        score += value;
        //if(OnScoreChanged != null) {
            //OnScoreChanged(score);
        //}
        UpdateScore(score);
        Debug.Log("Player Score: " + score);
    }
/*     void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
         IncreaseScore(5);
        }
    } */

    public void UpdateScore(int score) {
        ScoreText.text = score.ToString();
    }
    

}
