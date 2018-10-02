using UnityEngine;

namespace Scripts.Enemies
{
    [System.Serializable]
    public class EnemyWave
    {
        [SerializeField] internal float SpawnTime;
        [SerializeField] internal GameObject[] EnemyPrefabs;
    }
}
