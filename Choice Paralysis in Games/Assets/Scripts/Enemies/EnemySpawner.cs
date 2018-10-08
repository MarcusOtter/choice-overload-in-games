using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<EnemyWave> _waves;

        private List<EnemyWave> _remainingWaves;

        private float _elapsedtime;
        private int _spawnedWavesAmount;

        private Transform _player;

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag(EnvironmentVariables.PlayerTag)?.transform;

            // Order by spawn time (low --> high)
            _remainingWaves = _waves.OrderBy(x => x.SpawnTime).ToList();
        }

        private void Update()
        {
            _elapsedtime += Time.deltaTime;

            if (!_remainingWaves.Any()) { return; }

            if (_elapsedtime >= _remainingWaves[0].SpawnTime)
            {
                SpawnWave(_remainingWaves[0]);
                _remainingWaves.RemoveAt(0);
            }
        }

        private void SpawnWave(EnemyWave waveToSpawn)
        {
            Debug.Log($"Spawning wave {_spawnedWavesAmount + 1}");

            foreach (var enemy in waveToSpawn.EnemyPrefabs)
            {
                var testPos = (Vector2) _player.position + Random.insideUnitCircle.normalized * 25f;
                Instantiate(enemy, testPos, Quaternion.identity);
                print($"Spawned {enemy.name}");
            }

            _spawnedWavesAmount++;
        }
    }
}


