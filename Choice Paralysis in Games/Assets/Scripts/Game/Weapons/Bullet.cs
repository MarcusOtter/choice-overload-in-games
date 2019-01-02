﻿using UnityEngine;

namespace Scripts.Game.Weapons
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        [Header("Audio settings")]
        [SerializeField] private Audio.SoundEffectPlayer _soundPlayer;
        [SerializeField] private Audio.SoundEffect _impactSound;

        private int _damage;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        internal void Shoot(int damage, float speed)
        {
            _damage = damage;
            _rigidbody.AddForce(transform.up * speed, ForceMode2D.Impulse);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            HandleCollision(collider);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            HandleCollision(collision.collider);
        }

        private void HandleCollision(Collider2D collider)
        {
            collider.GetComponentInChildren<IDamageable>()?.TakeDamage(_damage);

            _soundPlayer.PlaySoundEffect(_impactSound);

            // Spawn bullet effect

            Destroy(gameObject);
        }
    }
}
