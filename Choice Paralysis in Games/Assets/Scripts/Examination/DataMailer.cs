using System.Collections;
using System.ComponentModel;
using UnityEngine;
using System.Net;
using System.Net.Mail;

namespace Scripts.Examination
{
    public class DataMailer : MonoBehaviour
    {
        [SerializeField] private ExaminationEntry _entry;

        private void Start()
        {
            if (DataCollector.Instance != null)
            {
                _entry = DataCollector.Instance.Entry;
            }

            Logger.Instance.Log("Starting email coroutine.");
            StartCoroutine(EmailData());
        }

        private IEnumerator EmailData()
        {
            MailMessage mail = new MailMessage(EnvironmentVariables.EmailAddress, EnvironmentVariables.EmailAddress)
            {
                Subject = $"Gymarb data | {_entry.CharacterData.CharacterName}",
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

            smtp.SendAsync(mail, _entry.CharacterData.CharacterName);
            yield break;
        }

        private void MailCallback(object sender, AsyncCompletedEventArgs e)
        {
            var token = (string) e.UserState;

            if (e.Cancelled)
            {
                Logger.Instance.LogError($"[{token}] Cancelled.", gameObject);
            }

            if (e.Error != null)
            {
                Logger.Instance.LogError($"[{token}] {e.Error}", gameObject);
            }
            else
            {
                Logger.Instance.Log($"[{token}] Email sent.", gameObject);
            }

            // TODO: Go to next scene? Or retry on fail
        }
    }
}
