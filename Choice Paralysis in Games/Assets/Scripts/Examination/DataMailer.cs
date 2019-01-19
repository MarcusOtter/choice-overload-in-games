using System.Collections;
using System.ComponentModel;
using UnityEngine;
using System.Net;
using System.Net.Mail;

namespace Scripts.Examination
{
    public class DataMailer : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI _errorMessageText;
        [SerializeField] private GameObject _finalMessage;

        private ExaminationEntry _entry;

        private void Start()
        {
            if (DataCollector.Instance != null)
            {
                _entry = DataCollector.Instance.Entry;
            }

            Logger.Instance.Log("Starting email coroutine.");
            StartCoroutine(EmailData());
        }

        private IEnumerator EmailData(float delay = 0f)
        {
            yield return new WaitForSeconds(delay);

            var characterName = string.IsNullOrWhiteSpace(_entry.CharacterData.CharacterName)
                ? EnvironmentVariables.DefaultCharacterName
                : _entry.CharacterData.CharacterName;

            MailMessage mail = new MailMessage(EnvironmentVariables.EmailAddress, EnvironmentVariables.EmailAddress)
            {
                Subject = $"Gymarb data | {characterName}",
                Body = JsonUtility.ToJson(_entry, true)
            };

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(EnvironmentVariables.EmailAddress, EnvironmentVariables.EmailPassword) as ICredentialsByHost
            };

            ServicePointManager.ServerCertificateValidationCallback =
                (s, certificate, chain, sslPolicyErrors) => true;

            

            smtp.SendCompleted += MailCallback;

            smtp.SendAsync(mail, characterName);
        }

        private void MailCallback(object sender, AsyncCompletedEventArgs e)
        {
            var token = (string) e.UserState;

            if (e.Cancelled)
            {
                Logger.Instance.LogError($"[{token}] Cancelled.", gameObject);
                _errorMessageText.text = "Something went wrong while submitting the data. Check your connection and try again.";

                Logger.Instance.Log("Retrying data submission...");
                StartCoroutine(EmailData(3f));
                return;
            }

            if (e.Error != null)
            {
                Logger.Instance.LogError($"[{token}] {e.Error}", gameObject);
                _errorMessageText.text = $"Something went while submitting the data.\nError message: {e.Error}";

                Logger.Instance.Log("Retrying data submission...");
                StartCoroutine(EmailData(3f));
                return;
            }

            Logger.Instance.Log($"[{token}] Email sent.", gameObject);
            _finalMessage.SetActive(true);
        }
    }
}
