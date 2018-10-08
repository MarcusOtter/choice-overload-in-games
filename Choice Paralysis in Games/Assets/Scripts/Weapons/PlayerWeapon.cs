using System;
using UnityEngine;

namespace Scripts.Weapons
{
    public class PlayerWeapon : Weapon
    {
        private PlayerPoints _playerPoints;
        private Transform _parentTransform;

        private UserInputController _userInputController;

        private void OnEnable()
        {
            _userInputController = UserInputController.Instance;
            _parentTransform = transform.parent;

            _playerPoints = transform.root.GetComponent<PlayerPoints>();

            _userInputController.OnAttackButtonDown += SpawnBullet;
        }

        protected override void WeaponBehaviour()
        {
            // Rotate towards the mouse
            AimDirection = (_userInputController.MouseWorldPosition - _parentTransform.position).normalized;
            _parentTransform.up = AimDirection;
        }

        private void SpawnBullet(object sender, EventArgs e)
        {
            Instantiate(BulletPrefabToSpawn, transform.position, transform.rotation).Shoot(BulletDamage, BulletSpeed);
            _playerPoints.ModifyPoints(-1);
        }

        private void OnDisable()
        {
            _userInputController.OnAttackButtonDown -= SpawnBullet;
        }
    }
}

