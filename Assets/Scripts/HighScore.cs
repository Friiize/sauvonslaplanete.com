using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HighScore : MonoBehaviour
{
    public GameObject HighScore1, HighScore2, HighScore3, HighScore4, HighScore5;

    public static int bestscore;
    float[] scoreTab = new float[5];
    float score;

    [HideInInspector] 
    
    public static HighScore Instance;
    
    private void Awake()
    {
        if (Instance) throw new NotImplementedException("Plus d'une fois le script dans la scène !");
        Instance = this;
    }

    public void SetHighScore(float currentScore)
    {

        score = currentScore;
        if (score >float.Parse(HighScore1.GetComponent<Text>().text))
        {
            scoreTab[4] = scoreTab[3];
            scoreTab[3] = scoreTab[2];
            scoreTab[2] = scoreTab[1];
            scoreTab[1] = scoreTab[0];
            scoreTab[0] = score;
        }
        else if (score > float.Parse(HighScore2.GetComponent<Text>().text))
        {
            scoreTab[4] = scoreTab[3];
            scoreTab[3] = scoreTab[2];
            scoreTab[2] = scoreTab[1];
            scoreTab[1] = score;
        }
        else if (score > float.Parse(HighScore3.GetComponent<Text>().text))
        {
            scoreTab[4] = scoreTab[3];
            scoreTab[3] = scoreTab[2];
            scoreTab[2] = score;
        }
        else if (score > float.Parse(HighScore4.GetComponent<Text>().text))
        {
            scoreTab[4] = scoreTab[3];
            scoreTab[3] = score;
        }else if (score > float.Parse(HighScore5.GetComponent<Text>().text))
        {
            scoreTab[4] = score;
        }
        
        HighScore1.GetComponent<Text>().text = scoreTab[0].ToString();
        HighScore2.GetComponent<Text>().text = scoreTab[1].ToString();
        HighScore3.GetComponent<Text>().text = scoreTab[2].ToString();
        HighScore4.GetComponent<Text>().text = scoreTab[3].ToString();
        HighScore5.GetComponent<Text>().text = scoreTab[4].ToString();
    }
}
