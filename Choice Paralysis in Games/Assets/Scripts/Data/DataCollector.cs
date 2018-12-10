using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Data
{
    public class DataCollector : MonoBehaviour
    {
        internal static DataCollector Instance { get; private set; }

        private void Awake()
        {
            SingletonCheck();
        }

        private void SingletonCheck()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
