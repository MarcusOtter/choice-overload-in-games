using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Scripts.Examination
{
    public class ExaminationHandler : MonoBehaviour
    {
        internal static ExaminationHandler Instance { get; private set; }

        private int _examinationNumber; // TODO: Modulo to figure out which mode it should be

        private void Awake()
        {
            SingletonCheck();
        }

        private void Start()
        {
            StartCoroutine(DownloadExaminationNumber());
        }

        private IEnumerator DownloadExaminationNumber()
        {
            Debug.Log($"[{DateTime.Now}]: Starting GET request to webtask.io");
            using (UnityWebRequest www = UnityWebRequest.Get(EnvironmentVariables.WebTaskUri))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.LogWarning($"[{DateTime.Now}]: An error occured when attempting to download the examination number from webtask.io.\nDefaulting to 0.");
                    _examinationNumber = 0;
                }
                else
                {
                    Debug.Log($"[{DateTime.Now}]: The download was successful from webtask.io");

                    try
                    {
                        _examinationNumber = int.Parse(www.downloadHandler.text);
                    }
                    catch
                    {
                        Debug.LogWarning($"[{DateTime.Now}]: An error occured when attempting parse the reveiced examination number.\nDefaulting to 0.");
                        _examinationNumber = 0;
                    }

                    Debug.Log($"[{DateTime.Now}]: The player is number {_examinationNumber}");
                }
            }
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
