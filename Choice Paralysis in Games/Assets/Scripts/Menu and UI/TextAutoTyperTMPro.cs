using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Menu_and_UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextAutoTyperTMPro : MonoBehaviour
    {
        [SerializeField] private float _initialDelay = 2f;
        [SerializeField] private float _letterDelay = 0.05f;

        [SerializeField] private UnityEvent _onTextCompleted;

        private TextMeshProUGUI _text;

        private void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _text.enableAutoSizing = false;

            StartCoroutine(TypeText());
        }

        private IEnumerator TypeText()
        {
            var textToWrite = _text.text;
            _text.text = string.Empty;

            yield return new WaitForSeconds(_initialDelay);
            foreach (var letter in textToWrite)
            {
                _text.text += letter;
                yield return new WaitForSeconds(_letterDelay);
            }

            _onTextCompleted?.Invoke();
        }
    }
}
