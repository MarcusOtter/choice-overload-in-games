using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Scripts.Examination;

namespace Scripts.Menu_and_UI
{
    public class FormHandler : MonoBehaviour
    {
        [SerializeField] private Button _submitButton;
        [SerializeField] private TMP_Dropdown[] _subjectQuestions;
        [SerializeField] private TMP_Dropdown[] _characterQuestions;
        [SerializeField] private TMP_Dropdown[] _reflectionQuestions;

        [SerializeField] private Color _incorrectColor = new Color(255, 105, 105);

        private void Start()
        {
            _submitButton.onClick.AddListener(SendForm);

            if (_subjectQuestions != null)    { SubscribeDropdowns(_subjectQuestions); }
            if (_characterQuestions != null)  { SubscribeDropdowns(_characterQuestions); }
            if (_reflectionQuestions != null) { SubscribeDropdowns(_reflectionQuestions); }
        }

        private void SubscribeDropdowns(TMP_Dropdown[] dropdowns)
        {
            foreach (var dropdown in dropdowns)
            {
                dropdown.onValueChanged.AddListener(delegate { DropdownSelectionIsValid(dropdown); });
            }
        }

        private void SendForm()
        {
            bool success = true;

            if (_subjectQuestions != null && _subjectQuestions.Length > 0)
            {
                success = SendAnswers(_subjectQuestions, typeof(SubjectQuestions)); 
            }

            if (_characterQuestions != null && _characterQuestions.Length > 0)
            {
                success = SendAnswers(_characterQuestions, typeof(CharacterQuestions)); 
            }

            if (_reflectionQuestions != null && _reflectionQuestions.Length > 0)
            {
                success = SendAnswers(_reflectionQuestions, typeof(ReflectionQuestions));

            }

            if (success)
            {
                SceneTransitioner.Instance.LoadNextScene();
            }
        }

        private bool SendAnswers(TMP_Dropdown[] answerDropdowns, Type questionType)
        {
            if (!DropdownsAreValid(answerDropdowns)) { return false; }

            object answers = null;

            if (questionType == typeof(SubjectQuestions))
            {
                answers = new SubjectQuestions
                (
                    gender:                  _subjectQuestions[0].captionText.text,
                    knowsExperimentPurpose:  _subjectQuestions[1].value == 1
                );
            }
            else if (questionType == typeof(CharacterQuestions))
            {
                answers = new CharacterQuestions
                (
                    initialCharacterSatisfaction:  _characterQuestions[0].captionText.text,
                    optionAmount:                  _characterQuestions[1].captionText.text,
                    enjoyedCustomization:          _characterQuestions[2].captionText.text
                );
            }
            else if (questionType == typeof(ReflectionQuestions))
            {
                answers = new ReflectionQuestions
                (
                    entertainmentValue:          int.Parse(_reflectionQuestions[0].captionText.text),
                    pleasedWithPerformance:      _reflectionQuestions[1].captionText.text,
                    finalCharacterSatisfaction:  _reflectionQuestions[2].captionText.text
                );
            }

            DataCollector.Instance.SetData(answers);
            return true;
        }

        private bool DropdownsAreValid(TMP_Dropdown[] dropdowns)
        {
            foreach (var dropdown in dropdowns)
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
