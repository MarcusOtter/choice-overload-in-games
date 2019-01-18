using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Game.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Spawn position parent transform")]
        [SerializeField] private Transform _spawnPositionParent;

        [Header("Waves")]
        [SerializeField] private EnemyWave[] _defaultEnemyWaves;
        [SerializeField] private EnemyWave[] _loopEnemyWaves;

        private List<EnemyWave> _remainingDefaultWaves;

        private float _latestSpawnTime;
        private float _lifespanTime;
        private int _spawnedWavesAmount;

        private void Start()
        {
            // Order by spawn time (low --> high)
            _remainingDefaultWaves = _defaultEnemyWaves.OrderBy(x => x.TimeUntilNextWave).ToList();

            // Disable spawning when player dies
            FindObjectOfType<Player.PlayerDeathBehaviour>().OnDeath.AddListener(() => _lifespanTime = float.MinValue);
        }

        private void Update()
        {
            _lifespanTime += Time.deltaTime;

            var waveToSpawn = GetNextWave();

            if (_lifespanTime >= _latestSpawnTime + waveToSpawn.TimeUntilNextWave)
            {
                SpawnWave(waveToSpawn);
            }
        }

        private EnemyWave GetNextWave()
        {
            if (!_remainingDefaultWaves.Any())
            {
                return GetLoopWave(_spawnedWavesAmount);
            }

            var wave = _remainingDefaultWaves[0];
            _remainingDefaultWaves.RemoveAt(0);
            return wave;
        }

        // return some time instead of the wave.. idk gotta modify it somewhere
        private EnemyWave GetLoopWave(int waveCount)
        {
            var index = waveCount % _loopEnemyWaves.Length;
            return _loopEnemyWaves[index];
        }

        private void SpawnWave(EnemyWave waveToSpawn)
        {
            Logger.Instance.Log($"Spawning wave {_spawnedWavesAmount + 1}", gameObject);

            foreach (var enemy in waveToSpawn.EnemyPrefabs)
            {
                var spawnPoint = _spawnPositionParent.GetChild(Random.Range(0, _spawnPositionParent.childCount)).position;
                Instantiate(enemy, spawnPoint, Quaternion.identity);
                Logger.Instance.Log($"Spawned {enemy.name}", gameObject);
            }

            _latestSpawnTime = Time.time;
            _spawnedWavesAmount++;
        }
    }
}


