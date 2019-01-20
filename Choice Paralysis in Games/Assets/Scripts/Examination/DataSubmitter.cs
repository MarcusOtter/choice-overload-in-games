using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Scripts.Examination
{
    public class DataSubmitter : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI _errorMessageText;
        [SerializeField] private GameObject _finalMessage;

        private ExaminationEntry _entry;

        private AllEntries _allEntries;

        private void Start()
        {
            if (DataCollector.Instance != null)
            {
                _entry = DataCollector.Instance.Entry;
            }
            
            StartCoroutine(EmailData(1f));
        }

        private IEnumerator EmailData(float delay = 0f)
        {
            yield return new WaitForSeconds(delay);

            var postData = JsonUtility.ToJson(_entry, false);

            using (UnityWebRequest www = UnityWebRequest.Post($"{EnvironmentVariables.DataWebTaskUri}?data={postData}", string.Empty))
            {
                Logger.Instance.Log($"Starting POST request to data submit webtask.io Post data: \n{postData}.", gameObject);
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Logger.Instance.LogError($"Data submisison failed: {www.error}", gameObject);
                    _errorMessageText.text = $"Something went while submitting the data.\nError message: {www.error}";

                    Logger.Instance.Log("Retrying data submission...");
                    StartCoroutine(EmailData(3f));
                    yield break;
                }
                
                Logger.Instance.Log("Data submitted.", gameObject);

                // Saves all the examination data into _allEntries.
                //_allEntries = JsonUtility.FromJson<AllEntries>(www.downloadHandler.text);
                //Logger.Instance.Log($"All data: {_allEntries}.", gameObject);

                _finalMessage.SetActive(true);
            }
        }
    }
}
