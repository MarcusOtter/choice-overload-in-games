using System;
using System.Collections;
using Scripts.Webtask.io;
using UnityEngine;
using UnityEngine.Networking;

namespace Scripts.Examination
{
    public class ExaminationHandler : MonoBehaviour
    {
        internal static ExaminationHandler Instance { get; private set; }

        [SerializeField] private AvailableSprites[] _modes; // 0: 0 sprites  | 1: 5 sprites  | 2: 15 sprites
                                                            // 3: 30 sprites | 4: 45 sprites | 5: 60 sprites

        private int _playerNumber;
        private int _examinationModeIndex;

        private void Awake()
        {
            SingletonCheck();
        }

        private void Start()
        {
            StartCoroutine(WebtaskIoRequest(WebtaskRequestType.IncrementCounter));
        }

        private IEnumerator WebtaskIoRequest(WebtaskRequestType requestType)
        {
            Debug.Log($"[{DateTime.Now}]: Starting GET request to webtask.io");
            using (UnityWebRequest www = UnityWebRequest.Post(EnvironmentVariables.WebTaskUri, ((int) requestType).ToString()))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.LogWarning($"[{DateTime.Now}]: An error occured when attempting to download the examination number from webtask.io.\nDefaulting to 1.");
                    _playerNumber = 1;
                }
                else
                {
                    Debug.Log($"[{DateTime.Now}]: The download was successful from webtask.io");

                    try
                    {
                        _playerNumber = int.Parse(www.downloadHandler.text);
                    }
                    catch
                    {
                        Debug.LogWarning($"[{DateTime.Now}]: An error occured when attempting parse the reveiced examination number.\nDefaulting to 1.");
                        _playerNumber = 1;
                    }

                    Debug.Log($"[{DateTime.Now}]: The player is number {_playerNumber}.");
                }
            }

            _examinationModeIndex = GetExaminationMode();
            Debug.Log($"[{DateTime.Now}]: Loaded execution mode with an index of {_examinationModeIndex}.");
        }

        private int GetExaminationMode()
        {
            if (_playerNumber % 5 == 0) { return 4; }

            return _playerNumber % 5 - 1;
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
