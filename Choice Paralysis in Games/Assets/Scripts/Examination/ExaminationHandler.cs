using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Scripts.Examination
{
    public class ExaminationHandler : MonoBehaviour
    {
        internal static ExaminationHandler Instance { get; private set; }

        internal int ExaminationModeNumber { get; private set; }

        [SerializeField] private AvailableSprites[] _modes; // 0: 2 sprites  | 1: 5 sprites  | 2: 15 sprites
                                                            // 3: 30 sprites | 4: 45 sprites | 5: 60 sprites

        [SerializeField] private WebtaskRequestType _requestType;

        private int _playerNumber;
        private int _examinationModeIndex;

        private void Awake()
        {
            SingletonCheck();
        }

        // Should be called by something else so ExaminationHandler can be used in other scenes without this
        private void Start()
        {
            StartCoroutine(WebtaskIoRequest(_requestType));
        }

        private IEnumerator WebtaskIoRequest(WebtaskRequestType requestType)
        {
            var postData = ((int)requestType).ToString();

            using (UnityWebRequest www = UnityWebRequest.Post(EnvironmentVariables.WebTaskUri, postData))
            {
                Logger.Instance.Log($"Starting POST request to webtask.io (Post data: {postData}).", gameObject);
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Logger.Instance.LogWarning("An error occured when attempting to download the examination number from webtask.io.\nDefaulting to 1.", gameObject);
                    _playerNumber = 1;
                }
                else
                {
                    Logger.Instance.Log("The download was successful from webtask.io.", gameObject);

                    try
                    {
                        _playerNumber = int.Parse(www.downloadHandler.text);
                    }
                    catch
                    {
                        Logger.Instance.LogWarning("An error occured when attempting parse the reveiced examination number.\nDefaulting to 1.", gameObject);
                        _playerNumber = 1;
                    }

                    Logger.Instance.Log($"The player is number {_playerNumber}.", gameObject);
                }
            }

            _examinationModeIndex = GetExaminationMode();
            ExaminationModeNumber = _examinationModeIndex + 1;
            Logger.Instance.Log($"Loaded Mode {ExaminationModeNumber}.", gameObject);
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
