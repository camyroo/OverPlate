using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.IO;

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

    public void LoadScore()
    {
        string filePath = Application.dataPath + "/score.txt";

        if (File.Exists(filePath))
        {
            StreamReader reader = new StreamReader(filePath);
            string fileContents = reader.ReadLine();

            int.TryParse(fileContents, out score);
            UpdateScore(score);

            reader.Close();
        }
        else
            score = 0;
    }

    public void SaveScore()
    {
        string filePath = Application.dataPath + "/score.txt";
        
        if (File.Exists(filePath))
        {
            File.WriteAllText(filePath, string.Empty);
        }

        StreamWriter writer = new StreamWriter(filePath, true);
        writer.WriteLine(score.ToString());
        writer.Close();
    }

    public void UpdateScore(int score)
    {
        ScoreText.text = score.ToString();
    }


}
