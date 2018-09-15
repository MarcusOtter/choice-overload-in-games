using System;
using UnityEngine;

namespace Scripts.Weapons
{
    public class BulletSpawner : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefabToSpawn;
        [SerializeField] private int _bulletDamage = 10;
        [SerializeField] private float _bulletSpeed = 10;

        private UserInputController _userInputController;

        private void OnEnable()
        {
            _userInputController = UserInputController.Instance;
            _userInputController.OnAttackButtonDown += SpawnBullet;
        }

        private void SpawnBullet(object sender, EventArgs e)
        {
            Instantiate(_bulletPrefabToSpawn, transform.position, transform.rotation).Shoot(_bulletDamage, _bulletSpeed);
        }
    }
}

