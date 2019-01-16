using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Game.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPositionParent;
        [SerializeField] private EnemyWave[] _waves;

        private List<EnemyWave> _remainingWaves;

        private float _elapsedTime;
        private int _spawnedWavesAmount;

        private void Start()
        {
            // Order by spawn time (low --> high)
            _remainingWaves = _waves.OrderBy(x => x.SpawnTime).ToList();

            // Disable spawning when player dies
            FindObjectOfType<Player.PlayerDeathBehaviour>().OnDeath.AddListener(() => _elapsedTime = float.MinValue);
        }

        private void Update()
        {
            _elapsedTime += Time.deltaTime;

            if (!_remainingWaves.Any()) { return; }

            if (_elapsedTime >= _remainingWaves[0].SpawnTime)
            {
                SpawnWave(_remainingWaves[0]);
                _remainingWaves.RemoveAt(0);
            }
        }

        //private void Disable

        private void SpawnWave(EnemyWave waveToSpawn)
        {
            Logger.Instance.Log($"Spawning wave {_spawnedWavesAmount + 1}", gameObject);

            foreach (var enemy in waveToSpawn.EnemyPrefabs)
            {
                var spawnPoint = _spawnPositionParent.GetChild(Random.Range(0, _spawnPositionParent.childCount)).position;
                Instantiate(enemy, spawnPoint, Quaternion.identity);
                Logger.Instance.Log($"Spawned {enemy.name}", gameObject);
            }

            _spawnedWavesAmount++;
        }
    }
}


