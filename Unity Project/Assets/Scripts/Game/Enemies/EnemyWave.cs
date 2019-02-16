using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Game.Enemies
{
    [CreateAssetMenu(menuName = "Enemy wave")]
    public class EnemyWave : ScriptableObject
    {
        internal float TimeUntilNextWave { get; private set; }

        [SerializeField] private float _timeUntilNextWave;
        [SerializeField] private float _minimumTimeUntilNextWave = 1f;
        [SerializeField] internal GameObject[] EnemyPrefabs;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += ResetTimeUntilNextWave;
        }

        private void ResetTimeUntilNextWave(Scene loadedScene, LoadSceneMode sceneMode)
        {
            TimeUntilNextWave = _timeUntilNextWave;
        }

        internal void MultiplyTimeUntilNextWave(float factor)
        {
            TimeUntilNextWave *= factor;
            
            if (TimeUntilNextWave < _minimumTimeUntilNextWave)
            {
                TimeUntilNextWave = _minimumTimeUntilNextWave;
            }
        }
    }
}
