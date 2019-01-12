using UnityEngine;

namespace Scripts.Game.Weapons
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        [Header("Bullet settings")]
        [SerializeField] private bool _isPlayerBullet;

        [Header("Audio settings")]
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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            HandleCollision(collision.collider);
        }

        private void HandleCollision(Collider2D collider)
        {
            // If a player bullet collides with the player
            if (_isPlayerBullet && collider.CompareTag(EnvironmentVariables.PlayerTag))
            {
                Destroy(gameObject);
                return;
            }

            var damageable = collider.GetComponentInChildren<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(_damage);
            }
            else
            {
                // Play default impact sound if no IDamageable is hit
                Audio.SoundEffectPlayer.PlaySoundEffect(_impactSound, transform);
            }

            // Spawn bullet effect?

            Destroy(gameObject);
        }
    }
}
