using Scripts.Examination;
using TMPro;
using UnityEngine;

namespace Scripts.Menu_and_UI
{
    public class LoadPlayerName : MonoBehaviour
    {
        [SerializeField] private string _defaultName = "John Doe the Great";
        [SerializeField] private TextMeshProUGUI _playerNameTmp;

        private CharacterData? _characterData;

        private void Start ()
        {
            _characterData = DataCollector.Instance?.CharacterData;
            SetNameText();
        }

        private void SetNameText()
        {
            if (!_characterData.HasValue)
            {
                Logger.Instance.LogWarning("Character Data doesn't have any data.");
                _playerNameTmp.text = _defaultName;
                return; 
            }

            var characterName = _characterData.Value.CharacterName;

            _playerNameTmp.text = string.IsNullOrWhiteSpace(characterName) ? _defaultName : characterName;
        }
    }
}
