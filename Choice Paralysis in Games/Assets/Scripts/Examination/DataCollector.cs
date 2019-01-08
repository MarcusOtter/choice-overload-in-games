using UnityEngine;

namespace Scripts.Examination
{
    public class DataCollector : MonoBehaviour
    {
        internal static DataCollector Instance { get; private set; }

        // TODO
        // What are their game results?
        // How happy are they with their game results?

        // TODO: Make an ExaminationEntry that holds this and game data

        internal CharacterData? CharacterData { get; private set; }
        private CharacterQuestions? _characterQuestions;

        private void Awake()
        {
            SingletonCheck();
        }

        internal void SetCharacterData(CharacterData characterData)
        {
            if (CharacterData != null) { Logger.Instance.LogWarning("CharacterData overriden!", gameObject); }
            
            CharacterData = characterData;

            Logger.Instance.Log($"Set character data. Data:\n{JsonUtility.ToJson(CharacterData, true)}", gameObject);
        }

        internal void SetCharacterQuestions(CharacterQuestions characterQuestions)
        {
            if (_characterQuestions != null) { Logger.Instance.LogWarning("CharacterQuestions overriden!", gameObject); }

            _characterQuestions = characterQuestions;

            Logger.Instance.Log($"Set character questions. Data:\n{JsonUtility.ToJson(_characterQuestions, true)}", gameObject);
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
