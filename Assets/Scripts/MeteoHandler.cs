using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoHandler : MonoBehaviour
{

    public GameObject temperature;
    public int increment = 0;
    [HideInInspector] 
    public static MeteoHandler Instance;
    
    private void Awake()
    {
        if (Instance) throw new NotImplementedException("Plus d'une fois le script dans la scène !");
        Instance = this;
    }
    
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
            temperature.GetComponent<UnityEngine.UI.Text>().text = increment + "�C";
        }
        else if (increment > 5)
            increment = 5;
    }
}
