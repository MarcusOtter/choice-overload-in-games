using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Scripts.Examination;

namespace Scripts.Menu_and_UI
{
    public class FormHandler : MonoBehaviour
    {
        [SerializeField] private Button _submitButton;
        [SerializeField] private TMP_Dropdown[] _dropdowns;

        [SerializeField] private Color _incorrectColor = Color.red;

        private void Start()
        {
            _submitButton.onClick.AddListener(ValidateForm);

            foreach (var dropdown in _dropdowns)
            {
                dropdown.onValueChanged.AddListener(delegate { DropdownSelectionIsValid(dropdown); });
            }
        }

        private void ValidateForm()
        {
            foreach (var dropdown in _dropdowns)
            {
                if (!DropdownSelectionIsValid(dropdown))
                {
                    return;
                }
            }

            var characterQuestions = new CharacterQuestions
            (
                satisfaction:          _dropdowns[0].captionText.text,
                options:               _dropdowns[1].captionText.text,
                enjoyedCustomization:  _dropdowns[2].captionText.text
            );

            DataCollector.Instance.SetCharacterQuestions(characterQuestions);
            SceneTransitioner.Instance.LoadNextScene();
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
