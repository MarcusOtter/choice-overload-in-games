using System.Collections;
using Scripts.Menu_and_UI;
using TMPro;
using UnityEngine;

namespace Scripts.Game
{
    public class MessageUI : MonoBehaviour
    {
        [Header("Delays")]
        [SerializeField] private float _deathTextDelay = 1f;

        [Header("Messages")]
        [SerializeField] private string _firstAttemptMessage;
        [SerializeField] private string _lastAttemptMessage;
        [SerializeField] private string _deathMessage;

        private TextMeshProUGUI _messageText;
        private ObjectFader _textFader;

        private Player.PlayerDeathBehaviour _playerDeath;

        private void Awake()
        {
            _messageText = GetComponentInChildren<TextMeshProUGUI>();
            _textFader = _messageText.GetComponent<ObjectFader>();

            DisplayInitialMessage();
        }

        private void Start()
        {
            _playerDeath = FindObjectOfType<Player.PlayerDeathBehaviour>();
            _playerDeath?.OnDeath.AddListener(() => StartCoroutine(DisplayDeathText(_deathTextDelay)));
        }

        private void DisplayInitialMessage()
        {
            var message = string.Empty;

            switch (Player.PlayerDeathBehaviour.DeathCount)
            {
                case 0:
                    message = _firstAttemptMessage;
                    break;

                case 1:
                    message = _lastAttemptMessage;
                    break;
            }

            StartCoroutine(DisplayMessage(message));
        }

        private IEnumerator DisplayMessage(string message, float duration = 3f)
        {
            _messageText.text = message;
            _textFader.FadeInComponentsOnFader();

            yield return new WaitForSeconds(duration);

            _textFader.FadeOutComponentsOnFader();
        }

        private IEnumerator DisplayDeathText(float delay)
        {
            yield return new WaitForSeconds(delay);

            _messageText.text = _deathMessage;
            _textFader.FadeInComponentsOnFader();
        }
    }
}
