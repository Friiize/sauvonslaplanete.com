using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class EventHandler : MonoBehaviour
    {
        [HideInInspector]
        public static EventHandler Instance;        
        public float increase;
        public int eventId = 0;


        private void Awake()
        {
            if (Instance) throw new NotImplementedException("PLus d'une fois le script dans la scène !");
            Instance = this;
        }

        private void Start()
        {
            InvokeRepeating("NextEvent", 2f, increase);
        }

        private void NextEvent()
        {
            eventId = Random.Range(0, 2);
        }
    }
}