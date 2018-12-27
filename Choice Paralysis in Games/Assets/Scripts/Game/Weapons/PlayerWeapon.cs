using System;
using UnityEngine;

namespace Scripts.Game.Weapons
{
    public class PlayerWeapon : Weapon
    {
        [Header("Player Weapon Settings")]
        [SerializeField] private GameObject _muzzleFlashPrefab;

        private PlayerPoints _playerPoints;
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
            _userInputController.OnAttackKeyDown += RegisterAttackHoldStart;
            _userInputController.OnAttackKeyUp += RegisterAttackHoldEnd;
        }

        private void Start()
        {
            _playerPoints = transform.root.GetComponent<PlayerPoints>();
        }

        protected override void WeaponBehaviour()
        {
            // Rotate towards the mouse
            AimDirection = (_userInputController.MouseWorldPosition - _parentTransform.position).normalized;
            _parentTransform.up = AimDirection;

            if (_fireWhenPossible && _lastBulletSpawnTime + ShootDelay < Time.time)
            {
                SpawnBullet();
            }

            if (!_attackBeingHeld) { return; }

            if (Time.time - (_holdStartTime + _bulletsFiredThisHold * ShootDelay) > ShootDelay)
            {
                SpawnBullet();
                _bulletsFiredThisHold++;
            }
        }

        private void RegisterAttackHoldStart(object sender, EventArgs args)
        {
            _holdStartTime = Time.time;
            _attackBeingHeld = true;

            if (_lastBulletSpawnTime + ShootDelay > Time.time)
            {
                _fireWhenPossible = true;
                return; 
            }

            SpawnBullet();
        }

        private void RegisterAttackHoldEnd(object sender, EventArgs args)
        {
            _attackBeingHeld = false;
            _bulletsFiredThisHold = 0;
        }

        private void SpawnBullet()
        {
            Instantiate(BulletPrefabToSpawn, transform.position, transform.rotation).Shoot(BulletDamage, BulletSpeed);
            Instantiate(_muzzleFlashPrefab, transform.position, transform.rotation);
            _playerPoints.ModifyPoints(-1);
            _lastBulletSpawnTime = Time.time;
            _fireWhenPossible = false;
        }

        private void OnDisable()
        {
            _userInputController.OnAttackKeyDown -= RegisterAttackHoldStart;
            _userInputController.OnAttackKeyUp -= RegisterAttackHoldEnd;
        }
    }
}

