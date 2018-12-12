using System.Collections;
using System.Collections.Generic;
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
        private CharacterData _characterData;

        private void Awake()
        {
            SingletonCheck();
        }

        internal void SetCharacterData(CharacterData characterData)
        {
            // CharacterData is a struct, so it is passed by value.
            _characterData = characterData;
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
