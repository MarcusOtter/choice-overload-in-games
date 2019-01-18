using UnityEngine;

namespace Scripts.Game.Enemies
{
    [System.Serializable]
    public class EnemyWave
    {
        [SerializeField] internal float TimeUntilNextWave;
        [SerializeField] internal GameObject[] EnemyPrefabs;
    }
}
