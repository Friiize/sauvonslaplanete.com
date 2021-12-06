using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoHandler : MonoBehaviour
{

    public GameObject temperature;
    private int increment = 0;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("incrementation", 2f, 2f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void incrementation()
    {
        if (increment < 5)
        {
            increment++;
            temperature.GetComponent<UnityEngine.UI.Text>().text = increment + "°C";
        }
        else if (increment > 5)
            increment = 5;
    }
}
