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

        [SerializeField] private AvailableSprites[] _modes; // 0: 2 sprites  | 1: 5 sprites  | 2: 15 sprites
                                                            // 3: 30 sprites | 4: 45 sprites | 5: 60 sprites

        [SerializeField] private WebtaskRequestType _requestType;

        private int _playerNumber;
        private int _examinationModeIndex;

        private void Awake()
        {
            SingletonCheck();
        }

        private void Start()
        {
            StartCoroutine(WebtaskIoRequest(_requestType));
        }

        private IEnumerator WebtaskIoRequest(WebtaskRequestType requestType)
        {
            var postData = ((int)requestType).ToString();

            using (UnityWebRequest www = UnityWebRequest.Post(EnvironmentVariables.WebTaskUri, postData))
            {
                Debug.Log($"[{DateTime.Now}]: Starting POST request to webtask.io (Post data: {postData}).");
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.LogWarning($"[{DateTime.Now}]: An error occured when attempting to download the examination number from webtask.io.\nDefaulting to 1.");
                    _playerNumber = 1;
                }
                else
                {
                    Debug.Log($"[{DateTime.Now}]: The download was successful from webtask.io.");

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
            Debug.Log($"[{DateTime.Now}]: Loaded Mode {_examinationModeIndex + 1}.");
        }

        private int GetExaminationMode()
        {
            if (_playerNumber % 6 == 0) { return 5; }

            return _playerNumber % 6 - 1;
        }

        internal AvailableSprites GetAvailableSprites()
        {
            return _modes[_examinationModeIndex];
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
