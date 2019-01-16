using System;
using Scripts.Game.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Examination
{
    public class DataCollector : MonoBehaviour
    {
        internal static DataCollector Instance { get; private set; }

        internal ExaminationEntry Entry
            => new ExaminationEntry(_subjectQuestions, CharacterData, _characterQuestions, _reflectionQuestions, _gameStats);

        [SerializeField] private int _webtaskSceneBuildIndex = 2;

        internal CharacterData CharacterData { get; private set; }
        private SubjectQuestions _subjectQuestions;
        private CharacterQuestions _characterQuestions;
        private ReflectionQuestions _reflectionQuestions;
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

            if (scene.buildIndex == _webtaskSceneBuildIndex)
            {
                ExaminationHandler.Instance?.Initialize();
            }
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

        internal void SetData(object data)
        {
            if (data is SubjectQuestions)
            {
                if (!_subjectQuestions.Equals(default(SubjectQuestions))) { Logger.Instance.LogWarning("SubjectQuestions overwritten!", gameObject); }
                _subjectQuestions = (SubjectQuestions) data;
            }
            else if (data is CharacterData)
            {
                if (!CharacterData.Equals(default(CharacterData))) { Logger.Instance.LogWarning("CharacterData overwritten!", gameObject); }
                CharacterData = (CharacterData) data;
            }
            else if (data is CharacterQuestions)
            {
                if (!_characterQuestions.Equals(default(CharacterQuestions))) { Logger.Instance.LogWarning("CharacterQuestions overwritten!", gameObject); }
                _characterQuestions = (CharacterQuestions) data;
            }
            else if (data is ReflectionQuestions)
            {
                if (!_reflectionQuestions.Equals(default(ReflectionQuestions))) { Logger.Instance.LogWarning("ReflectionQuestions overwritten!", gameObject); }
                _reflectionQuestions = (ReflectionQuestions) data;
            }
            else
            {
                Logger.Instance.LogError($"Cannot set data of type '{data.GetType()}'", gameObject);
                return;
            }

            Logger.Instance.Log($"DataCollector reveiced data:\n{JsonUtility.ToJson(data, true)}", gameObject);
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
