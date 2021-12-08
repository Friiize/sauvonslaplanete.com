using System;
using System.Collections;
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
        public bool isGameEnded = false;


        private void Awake()
        {
            if (Instance) throw new NotImplementedException("Plus d'une fois le script dans la scène !");
            Instance = this;
        }

        private void Start()
        {
            StartCoroutine(Coroutine());
        }

        private IEnumerator Coroutine()
        {
            while (!isGameEnded)
            {
                yield return new WaitForSeconds(increase);
                NextEvent();
            }
        }

        private void NextEvent()
        {
            eventId = Random.Range(0, 2);
        }

        public void Stop()
        {
            isGameEnded = true;
        }
    }
}