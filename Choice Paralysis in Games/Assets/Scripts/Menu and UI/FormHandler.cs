using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Scripts.Examination;

namespace Scripts.Menu_and_UI
{
    public class FormHandler : MonoBehaviour
    {
        [SerializeField] private Button _submitButton;
        [SerializeField] private TMP_Dropdown[] _characterQuestions;
        [SerializeField] private TMP_Dropdown[] _reflectionQuestions;

        [SerializeField] private Color _incorrectColor = new Color(255, 105, 105);

        private void Start()
        {
            _submitButton.onClick.AddListener(SendForm);

            foreach (var dropdown in _characterQuestions)
            {
                dropdown.onValueChanged.AddListener(delegate { DropdownSelectionIsValid(dropdown); });
            }

            foreach (var dropdown in _reflectionQuestions)
            {
                dropdown.onValueChanged.AddListener(delegate { DropdownSelectionIsValid(dropdown); });
            }
        }

        private void SendForm()
        {
            if (_characterQuestions.Length > 0)
            {
                SendCharacterQuestions();
            }

            if (_reflectionQuestions.Length > 0)
            {
                SendReflectionQuestions();
            }
            
            SceneTransitioner.Instance.LoadNextScene();
        }

        private void SendCharacterQuestions()
        {
            if (!DropdownsAreValid(_characterQuestions)) { return; }

            var characterQuestions = new CharacterQuestions
            (
                initialCharacterSatisfaction: _characterQuestions[0].captionText.text,
                optionAmount:                 _characterQuestions[1].captionText.text,
                enjoyedCustomization:         _characterQuestions[2].captionText.text
            );

            DataCollector.Instance.SetCharacterQuestions(characterQuestions);
        }

        private void SendReflectionQuestions()
        {
            if (!DropdownsAreValid(_reflectionQuestions)) { return; }

            var reflectionQuestions = new ReflectionQuestions
            (
                entertainmentValue:         _reflectionQuestions[0].captionText.text,
                pleasedWithPerformance:     _reflectionQuestions[1].captionText.text,
                finalCharacterSatisfaction: _reflectionQuestions[2].captionText.text
            );

            DataCollector.Instance.SetReflectionQuestions(reflectionQuestions);
        }

        private bool DropdownsAreValid(TMP_Dropdown[] dropdowns)
        {
            foreach (var dropdown in _characterQuestions)
            {
                if (!DropdownSelectionIsValid(dropdown))
                {
                    return false;
                }
            }

            return true;
        }

        private bool DropdownSelectionIsValid(TMP_Dropdown dropdown)
        {
            var isValid = dropdown.value != 0;

            dropdown.image.color = isValid
                ? Color.white
                : _incorrectColor;

            return isValid;
        }
    }
}
