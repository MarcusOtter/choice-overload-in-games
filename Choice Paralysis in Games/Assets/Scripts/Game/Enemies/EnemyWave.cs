using UnityEngine;

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
