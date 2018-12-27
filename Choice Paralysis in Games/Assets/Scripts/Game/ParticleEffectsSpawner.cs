using System;
using Scripts.Game.Weapons;
using UnityEngine;

namespace Scripts.Game
{
    public class ParticleEffectsSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _muzzleFlash;

        private void OnEnable()
        {
            PlayerWeapon.OnWeaponFire += SpawnMuzzleFlash;
        }

        private void SpawnMuzzleFlash(object sender, EventArgs args)
        {
            Transform weaponTransform = ((PlayerWeapon) sender).transform;
            Instantiate(_muzzleFlash, weaponTransform.position, weaponTransform.rotation);
        }

        private void OnDisable()
        {
            PlayerWeapon.OnWeaponFire -= SpawnMuzzleFlash;
        }
    }
}
