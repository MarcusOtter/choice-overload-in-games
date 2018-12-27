using UnityEngine;

namespace Scripts.Game.Weapons
{
    /// <summary>Base class for all weapons</summary>
    public abstract class Weapon : MonoBehaviour
    {
        public Vector2 AimDirection { get; protected set; }

        [Header("General Weapon Settings")]
        [SerializeField] internal float KnockbackForce = 10;
        [SerializeField] protected Bullet BulletPrefabToSpawn;
        [SerializeField] protected int BulletDamage = 10;
        [SerializeField] protected float BulletSpeed = 10;
        [SerializeField] protected float ShootDelay = 0.2f;
        [SerializeField] [Range(0f, 1f)] protected float Accuracy = 0.9f;

        protected virtual void Update()
        {
            WeaponBehaviour();
        }

        protected virtual void WeaponBehaviour()
        {
            // Overwritten in weapons that inherit from this class.
        }
    }
}
