using System;
using UnityEngine;

namespace Scripts.Game.Weapons
{
    public class PlayerWeapon : Weapon
    {
        internal static event EventHandler OnWeaponFire;

        private Transform _parentTransform;

        private UserInputController _userInputController;

        private int _bulletsFiredThisHold; // How many bullets have been fired during this mouse button hold?

        private float _holdStartTime;
        private bool _attackBeingHeld;

        private float _lastBulletSpawnTime;

        private bool _fireWhenPossible;

        private void OnEnable()
        {
            _userInputController = UserInputController.Instance;

            _parentTransform = transform.parent;
            _userInputController.OnAttackKeyDown += RegisterAttackKeyDown;
            _userInputController.OnAttackKeyUp += RegisterAttackKeyUp;
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
            AimDirection = (_userInputController.MouseWorldPosition - _parentTransform.position).normalized;
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

            Instantiate(BulletPrefabToSpawn, transform.position, transform.rotation).Shoot(BulletDamage, BulletSpeed);
            _lastBulletSpawnTime = Time.time;
        }

        private void OnDisable()
        {
            _userInputController.OnAttackKeyDown -= RegisterAttackKeyDown;
            _userInputController.OnAttackKeyUp -= RegisterAttackKeyUp;
        }
    }
}

