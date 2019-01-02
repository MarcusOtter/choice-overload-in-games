﻿using System;
using Scripts.Game.Weapons;
using UnityEngine;

namespace Scripts.Game.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _movementSpeed;

        private UserInput _input;
        private Rigidbody2D _rigidbody;

        private Vector2 _recoilKnockbackVector;

        private void OnEnable()
        {
            PlayerWeapon.OnWeaponFire += AddRecoilKnockback;
        }

        private void Start()
        {
            _input = UserInput.Instance;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = new Vector2(_input.HorizontalAxis * _movementSpeed,
                _input.VerticalAxis * _movementSpeed) + _recoilKnockbackVector;

            // Reset recoil knockback vector after applying it
            _recoilKnockbackVector = Vector2.zero;
        }

        private void AddRecoilKnockback(object sender, EventArgs args)
        {
            var weapon = (PlayerWeapon) sender;
            var knockbackDirection = weapon.AimDirection * -1;

            _recoilKnockbackVector = knockbackDirection * weapon.RecoilKnockbackForce;
        }

        // TODO: Move outside of PlayerMovement
        public void TakeDamage(int incomingDamage)
        {
            Logger.Instance.Log($"Player took {incomingDamage} damage.");
        }

        private void OnDisable()
        {
            PlayerWeapon.OnWeaponFire -= AddRecoilKnockback;
        }
    }
}

