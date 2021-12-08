using System;
using UnityEngine;

public class EndHandler : MonoBehaviour
{
    private void Update()
    {
        if (MeteoHandler.Instance.increment == 50)
        {
            EventHandler.Instance.Stop();
        }
    }
}
