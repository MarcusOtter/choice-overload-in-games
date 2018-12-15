using System;
using UnityEngine;

namespace Scripts
{
    // Can be made to log to a file later on.
    public class Logger : MonoBehaviour
    {
        internal static Logger Instance { get; private set; }

        private void Awake()
        {
            SingletonCheck();
        }

        internal void Log(string messageToLog)
        {
            Debug.Log($"[{DateTime.Now:HH:mm:ss}]: {messageToLog}");
        }

        internal void LogWarning(string messageToLog)
        {
            Debug.LogWarning($"[{DateTime.Now:HH:mm:ss}]: {messageToLog}");
        }

        internal void LogError(string messageToLog)
        {
            Debug.LogError($"[{DateTime.Now:HH:mm:ss}]: {messageToLog}");
        }

        private void SingletonCheck()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(transform.root.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
