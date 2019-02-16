using System;
using Scripts.Game.Weapons;
using UnityEngine;

namespace Scripts.Game
{
    public class ParticleEffectsSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _muzzleFlash;
        [SerializeField] private GameObject _shellCasing;

        private void OnEnable()
        {
            PlayerWeapon.OnWeaponFire += SpawnMuzzleFlash;
            PlayerWeapon.OnWeaponFire += SpawnShellCasing;
        }

        private void SpawnShellCasing(object sender, EventArgs e)
        {
            var spawnPoint = ((PlayerWeapon)sender).ShellCasingSpawnPoint;
            Instantiate(_shellCasing, spawnPoint.position, spawnPoint.rotation);
        }

        private void SpawnMuzzleFlash(object sender, EventArgs args)
        {
            Transform weaponTransform = ((PlayerWeapon) sender).transform;
            Instantiate(_muzzleFlash, weaponTransform.position, weaponTransform.rotation);
        }

        private void OnDisable()
        {
            PlayerWeapon.OnWeaponFire -= SpawnMuzzleFlash;
            PlayerWeapon.OnWeaponFire -= SpawnShellCasing;
        }
    }
}
