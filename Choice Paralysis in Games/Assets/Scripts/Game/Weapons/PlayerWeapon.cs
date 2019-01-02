using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Game.Weapons
{
    public class PlayerWeapon : Weapon
    {
        internal static event EventHandler OnWeaponFire;

        [Header("Player weapon settings")]
        [SerializeField] internal Transform ShellCasingSpawnPoint;
        [SerializeField] internal float RecoilKnockbackForce = 10;

        private Transform _parentTransform;

        private UserInput _userInput;

        private int _bulletsFiredThisHold; // How many bullets that have been fired during this mouse press

        private float _holdStartTime;
        private bool _attackBeingHeld;

        private float _lastBulletSpawnTime;

        private bool _fireWhenPossible;

        private void OnEnable()
        {
            _userInput = UserInput.Instance;

            _parentTransform = transform.parent;
            _userInput.OnAttackKeyDown += RegisterAttackKeyDown;
            _userInput.OnAttackKeyUp += RegisterAttackKeyUp;
        }

        protected override void WeaponBehaviour()
        {
            RotateTowardsMouse();

            // Spawn a bullet if there is one that is queued to be spawned
            if (_fireWhenPossible && _lastBulletSpawnTime + ShootDelay < Time.time)
            {
                SpawnBullet();
                _fireWhenPossible = false;
            }

            if (!_attackBeingHeld) { return; }

            SprayBullets();
        }
        
        /// <summary>Spawns a bullet when the attack key is being held </summary>
        private void SprayBullets()
        {
            if (Time.time - (_holdStartTime + _bulletsFiredThisHold * ShootDelay) > ShootDelay)
            {
                SpawnBullet();
                _bulletsFiredThisHold++;
            }
        }

        private void RotateTowardsMouse()
        {
            AimDirection = (_userInput.MouseWorldPosition - _parentTransform.position).normalized;
            _parentTransform.up = AimDirection;
        }

        private void RegisterAttackKeyDown(object sender, EventArgs args)
        {
            _holdStartTime = Time.time;
            _attackBeingHeld = true;

            if (_lastBulletSpawnTime + ShootDelay > Time.time)
            {
                // The player has pressed the attack key prematurely. 
                // This bullet will automatically be spawned when it can be.
                _fireWhenPossible = true;
            }
            else
            {
                SpawnBullet();
            }
        }

        private void RegisterAttackKeyUp(object sender, EventArgs args)
        {
            _attackBeingHeld = false;
            _bulletsFiredThisHold = 0;
        }

        private void SpawnBullet()
        {
            OnWeaponFire?.Invoke(this, EventArgs.Empty);

            SoundPlayer?.PlaySoundEffect(ShootSound);

            Instantiate(BulletPrefabToSpawn, transform.position, GetRandomOffsetBulletRotation()).Shoot(BulletDamage, BulletSpeed);
            _lastBulletSpawnTime = Time.time;
        }

        /// <summary>
        /// Calculates a rotation with a random offset that depends on the <see cref="Weapon.Accuracy"/>.
        /// </summary>
        private Quaternion GetRandomOffsetBulletRotation()
        {
            // If the accuracy is 1, the bullet will always go in a straight line.
            if (Mathf.Approximately(Accuracy, 1f))
            {
                return transform.rotation;
            }

            // With 0 accuracy, the offset can be between -45 degrees and + 45 degrees.
            // With 0.5 accuracy, the offset can be between -22.5 degrees and + 22.5 degrees.
            // (etc)
            var randomOffsetDegrees = Random.Range((1 - Accuracy) * -45, (1 - Accuracy) * 45);
            return Quaternion.AngleAxis(randomOffsetDegrees, Vector3.forward) * transform.rotation;
        }

        private void OnDisable()
        {
            _userInput.OnAttackKeyDown -= RegisterAttackKeyDown;
            _userInput.OnAttackKeyUp -= RegisterAttackKeyUp;
        }
    }
}

