using UnityEngine;

namespace Scripts.Examination
{
    public class DataCollector : MonoBehaviour
    {
        internal static DataCollector Instance { get; private set; }

        // Did they change their character's name? To what?
        // How many sets did they have?
        // How many skins did they look at?
        // Did they choose a matching set? (head & body index matches)
        // How long time did they spend creating a character?
        // How happy are they with their character?

        // What are their game results?
        // How happy are they with their game results?

        // TODO: Make an ExaminationEntry that holds this and game data

        private CharacterData? _characterData;
        private CharacterQuestions? _characterQuestions;

        private void Awake()
        {
            SingletonCheck();
        }

        internal void SetCharacterData(CharacterData characterData)
        {
            if (_characterData != null) { Logger.Instance.LogWarning("CharacterData overriden!"); }
            
            _characterData = characterData;

            Logger.Instance.Log($"Set character data. Data:\n{JsonUtility.ToJson(_characterData, true)}");
        }

        internal void SetCharacterQuestions(CharacterQuestions characterQuestions)
        {
            if (_characterQuestions != null) { Logger.Instance.LogWarning("CharacterQuestions overriden!"); }

            _characterQuestions = characterQuestions;

            Logger.Instance.Log($"Set character questions. Data:\n{JsonUtility.ToJson(_characterQuestions, true)}");
        }

        private void SingletonCheck()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
