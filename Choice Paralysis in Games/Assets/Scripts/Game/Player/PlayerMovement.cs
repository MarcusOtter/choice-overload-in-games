using System;
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

        private Vector2 _knockbackVector;

        private void OnEnable()
        {
            PlayerWeapon.OnWeaponFire += AddKnockback;
        }

        private void Start()
        {
            _input = UserInput.Instance;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = new Vector2(_input.HorizontalAxis * _movementSpeed,
                _input.VerticalAxis * _movementSpeed) + _knockbackVector;

            // Reset knockback vector after applying it
            _knockbackVector = Vector2.zero;
        }

        private void AddKnockback(object sender, EventArgs args)
        {
            var weapon = (PlayerWeapon) sender;
            var knockbackDirection = weapon.AimDirection * -1;

            _knockbackVector = knockbackDirection * weapon.KnockbackForce;
        }

        // TODO: Move outside of PlayerMovement
        public void TakeDamage(int incomingDamage)
        {
            Logger.Instance.Log($"Player took {incomingDamage} damage.");
        }

        private void OnDisable()
        {
            PlayerWeapon.OnWeaponFire -= AddKnockback;
        }
    }
}

