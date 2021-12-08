using System;
using UnityEngine;

public class EndHandler : MonoBehaviour
{
    private void Update()
    {
        if (EventHandler.Instance.GetHealthyTrees() == 0)
        {
            EventHandler.Instance.Stop();
            Time.timeScale = 0;
            HighScore.Instance.SetHighScore(EventHandler.Instance.score);
        }
    }
}
