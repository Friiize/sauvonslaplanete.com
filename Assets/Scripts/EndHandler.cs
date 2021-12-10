using System;
using UnityEngine;

public class EndHandler : MonoBehaviour
{
    public GameObject scoreBoard;
    private HighScore highScore;

    private void Start()
    {
        highScore = scoreBoard.GetComponent<HighScore>();
    }

    private void Update()
    {
        if (EventHandler.Instance.GetHealthyTrees() == 0 && !EventHandler.Instance.isGameEnded)
        {
            EventHandler.Instance.Stop();
            highScore.SetHighScore((int)EventHandler.Instance.score);
            scoreBoard.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
