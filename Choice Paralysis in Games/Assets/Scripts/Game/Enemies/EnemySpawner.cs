using UnityEngine;

namespace Scripts.Game.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Spawn position parent transform")]
        [SerializeField] private Transform _spawnPositionParent;

        [Header("Waves")]
        [SerializeField] private EnemyWave[] _defaultEnemyWaves;
        [SerializeField] private EnemyWave[] _loopEnemyWaves;

        private float _nextSpawnTime;
        private int _spawnedWavesCount;

        private void Update()
        {
            if (Time.time < _nextSpawnTime) { return; }

            if (_spawnedWavesCount < _defaultEnemyWaves.Length - 1)
            {
                SpawnDefaultWave();
            }
            else
            {
                SpawnLoopWave();
            }
        }

        private void SpawnLoopWave()
        {
            var waveToSpawn = _loopEnemyWaves[_spawnedWavesCount % _loopEnemyWaves.Length];

            SpawnWaveEnemies(waveToSpawn);
            _nextSpawnTime = Time.time + waveToSpawn.TimeUntilNextWave;

            waveToSpawn.MultiplyTimeUntilNextWave(1f - _spawnedWavesCount / 100f);
            _spawnedWavesCount++;
        }

        private void SpawnDefaultWave()
        {
            var waveToSpawn = _defaultEnemyWaves[_spawnedWavesCount];

            SpawnWaveEnemies(waveToSpawn);
            _nextSpawnTime = Time.time + waveToSpawn.TimeUntilNextWave;
            _spawnedWavesCount++;
        }

        private void SpawnWaveEnemies(EnemyWave waveToSpawn)
        {
            Logger.Instance.Log($"Spawning wave number {_spawnedWavesCount + 1}", gameObject);

            foreach (var enemy in waveToSpawn.EnemyPrefabs)
            {
                var spawnPoint = _spawnPositionParent.GetChild(Random.Range(0, _spawnPositionParent.childCount)).position;
                Instantiate(enemy, spawnPoint, Quaternion.identity);
                Logger.Instance.Log($"Spawned {enemy.name}", gameObject);
            }
        }
    }
}
