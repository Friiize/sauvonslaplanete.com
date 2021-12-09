using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoHandler : MonoBehaviour
{
    public GameObject text;
    public float temperature = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(incrementation());
    }

    private IEnumerator incrementation()
    {
        while (!EventHandler.Instance.isGameEnded)
        {
            yield return new WaitForSeconds(Mathf.Round(5 * EventHandler.Instance.GetHealthyTrees() * 10 / 84) / 10);
            /*if (temperature < 5)
            {*/
                temperature = Mathf.Round(temperature * 10 + 1) / 10;
                text.GetComponent<UnityEngine.UI.Text>().text = "+" + temperature + "°C";
            //}
            /*else if (temperature > 5)
                temperature = 5;*/
        }
    }
}
