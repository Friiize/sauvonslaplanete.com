using System;
using UnityEngine;

namespace DefaultNamespace
{
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
}