using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Menu_and_UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextAutoTyperTMPro : MonoBehaviour
    {
        [Header("Special mode")]
        [SerializeField] private bool _infinitelyRepeat;

        [Header("Multiple messages")]
        [SerializeField] private bool _useMultipleMessages; // Use the strings defined below instead of the start value
        [SerializeField] private KeyCode _nextMessageKey = KeyCode.Space;
        [SerializeField] private TextMeshProUGUI _progressInstructions;
        [SerializeField] private ObjectFader _instructionTextFader;
        [SerializeField] [TextArea(1, 3)] private string[] _messages;

        [Header("Delays")]
        [SerializeField] private float _initialDelay = 2f;
        [SerializeField] private float _letterDelay = 0.025f;

        [Header("Events")]
        [SerializeField] private UnityEvent _onTextLoaded;
        [SerializeField] private UnityEvent _onTextCompleted;

        private TextMeshProUGUI _text;
        private int _currentMessageIndex;
        private bool _allowMessageProgression;

        private void OnEnable()
        {
            _onTextLoaded?.Invoke();
        }

        private void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _text.enableAutoSizing = false;

            StartCoroutine(TypeText());
        }

        private void Update()
        {
            if (!_allowMessageProgression) { return; }

            if (!Input.GetKeyDown(_nextMessageKey)) { return; }

            if (++_currentMessageIndex > _messages.Length - 1)
            {
                _onTextCompleted?.Invoke();
                _text.text = string.Empty;
                SetInstructionText(string.Empty);
                enabled = false;
                return;
            }

            StartCoroutine(TypeText());
        }

        private void SetInstructionText(string newText)
        {
            if (_progressInstructions != null)
            {
                _progressInstructions.text = newText;
            }

            // If there is text in the progressInstructions, fade it in.
            if (!string.IsNullOrWhiteSpace(newText))
            {
                _instructionTextFader?.FadeInTMPText(_progressInstructions);
            }
        }

        private IEnumerator TypeText()
        {
            _allowMessageProgression = false;

            do
            {
                var textToWrite = _useMultipleMessages
                    ? _messages[_currentMessageIndex]
                    : _text.text;

                _text.text = string.Empty;
                SetInstructionText(string.Empty);

                if (_currentMessageIndex == 0)
                {
                    yield return new WaitForSeconds(_initialDelay);
                }

                foreach (var letter in textToWrite)
                {
                    _text.text += letter;
                    yield return new WaitForSeconds(_letterDelay);
                }

            } while (_infinitelyRepeat);

            if (!_useMultipleMessages)
            {
                _onTextCompleted?.Invoke();
                yield break;
            }

            SetInstructionText($"Press [{_nextMessageKey.ToString()}] to continue...");
            _allowMessageProgression = true;
        }
    }
}
