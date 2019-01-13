using System;
using Scripts.Game.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        private GameStats _gameStats;

        private PlayerDeathBehaviour _playerDeathBehaviour;

        private void Awake()
        {
            SingletonCheck();
            _gameStats = new GameStats();
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            _playerDeathBehaviour = FindObjectOfType<PlayerDeathBehaviour>();
            _playerDeathBehaviour?.OnDeath.AddListener(SetFinalTime);
        }

        private void SetFinalTime()
        {
            if (_playerDeathBehaviour == null)
            {
                Logger.Instance.LogWarning("Cannot set final time!");
                return;
            }

            int previousDeathsCount = PlayerDeathBehaviour.DeathCount;

            switch (previousDeathsCount)
            {
                case 0:
                    _gameStats.FirstAttemptTime = FindObjectOfType<Game.Timer>().FinalTime;
                    break;

                case 1:
                    _gameStats.SecondAttemptTime = FindObjectOfType<Game.Timer>().FinalTime;
                    break;
            }

            Logger.Instance.Log($"Set game stats. Data:\n{JsonUtility.ToJson(_gameStats, true)}", gameObject);
        }

        // Called by enemies when they die
        internal void IncrementKillCount()
        {
            _gameStats.KillCount++;
        }

        // Called from Bullet script if it is a player bullet
        internal void RegisterShot(bool hitEnemy)
        {
            _gameStats.ShotsFired++;

            if (hitEnemy)
            {
                _gameStats.ShotsHit++;
            }
            else
            {
                _gameStats.ShotsMissed++;
            }

            _gameStats.Accuracy = Math.Round((_gameStats.ShotsHit / (double) _gameStats.ShotsFired) * 100d, 2);
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

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
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
