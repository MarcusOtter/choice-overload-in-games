using UnityEngine;

namespace Scripts.Game.Weapons
{
    /// <summary>Base class for all weapons</summary>
    public abstract class Weapon : MonoBehaviour
    {
        public Vector2 AimDirection { get; protected set; }

        [Header("Bullet settings")]
        [SerializeField] protected Bullet BulletPrefabToSpawn;
        [SerializeField] protected int BulletDamage = 2;
        [SerializeField] protected float BulletSpeed = 40;

        [Header("Weapon settings")]
        [SerializeField] protected float ShootDelay = 0.15f;
        [SerializeField] [Range(0f, 1f)] protected float Accuracy = 0.925f;

        [Header("Audio settings")]
        [SerializeField] protected Audio.SoundEffectPlayer SoundPlayer;
        [SerializeField] protected Audio.SoundEffect ShootSound;

        protected virtual void Update()
        {
            WeaponBehaviour();
        }

        protected abstract void WeaponBehaviour();
    }
}
