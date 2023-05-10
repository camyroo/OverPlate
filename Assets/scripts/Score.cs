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
        // Define the file path
        string filePath = Application.dataPath + "/score.txt";

        // Check if the file exists
        if (File.Exists(filePath))
        {
            // Read the first line of the file
            StreamReader reader = new StreamReader(filePath);
            string fileContents = reader.ReadLine();

            // Parse the score from the file contents
            int.TryParse(fileContents, out score);
            UpdateScore(score);

            // Close the file
            reader.Close();
        }
        else
        {
            // If the file doesn't exist, set the score to 0
            score = 0;
        }
    }

    public void SaveScore()
    {
        // Define the file path
        string filePath = Application.dataPath + "/score.txt";

        // Check if the file exists
        if (File.Exists(filePath))
        {
            // Clear the file contents
            File.WriteAllText(filePath, string.Empty);
        }

        // Create or open the file
        StreamWriter writer = new StreamWriter(filePath, true);

        // Write the score to the file
        writer.WriteLine(score.ToString());

        // Close the file
        writer.Close();
    }

    public void UpdateScore(int score)
    {
        ScoreText.text = score.ToString();
    }


}
