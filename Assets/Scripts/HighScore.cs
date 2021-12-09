using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public GameObject[] highScores = new GameObject[5];

    private static int[] scoreTab = new int[5];
    private int score;

    public void SetHighScore(int currentScore)
    {
        score = currentScore;
        int i = 0;
        bool updated = false;
        while (i < 5 && !updated)
        {
            if (score > scoreTab[i])
            {
                updated = true;
                for (int j = 4; j > i; j--)
                    scoreTab[j] = scoreTab[j - 1];
                scoreTab[i] = score;
            }
            i++;
        }

        ShowHighScores();
    }

    public void ShowHighScores()
    {
        for (int i = 0; i < 5; i++)
            highScores[i].GetComponent<Text>().text = scoreTab[i].ToString();
    }
}
