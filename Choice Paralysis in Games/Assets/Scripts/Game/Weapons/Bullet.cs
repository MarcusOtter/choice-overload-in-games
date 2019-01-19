using UnityEngine;

namespace Scripts.Game.Weapons
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        [Header("Bullet settings")]
        [SerializeField] private bool _isPlayerBullet;

        [Header("Impact settings")]
        [SerializeField] private Audio.SoundEffect _impactSound;
        [SerializeField] private ParticleSystem _impactParicleSystem;
        [SerializeField] private ParticleSystem.MinMaxGradient _wallImpactColor;
        [SerializeField] private ParticleSystem.MinMaxGradient _enemyImpactColor;

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
            // Bullet script should really be an abstract class
            // and then have a PlayerBullet.cs and an EnemyBullet.cs
            // inheriting it. For now, this works.
            if (_isPlayerBullet)
            {
                HandlePlayerBulletCollision(collision.collider);
            }
            else
            {
                HandleEnemyBulletCollision(collision.collider);
            }
        }

        private void HandlePlayerBulletCollision(Collider2D collider)
        {
            // If a player bullet collides with the player
            if (collider.CompareTag(EnvironmentVariables.PlayerTag))
            {
                Destroy(gameObject);
                return;
            }

            var dataCollector = Examination.DataCollector.Instance;

            var hitDamageable = collider.GetComponentInChildren<IDamageable>();
            bool hitEnemy = collider.GetComponent<Enemies.Enemy>() != null;

            if (hitDamageable != null)
            {
                hitDamageable.TakeDamage(_damage);

                dataCollector.RegisterShot(hitEnemy);
            }
            else
            {
                // Play default impact sound if no IDamageable is hit
                Audio.SoundEffectPlayer.PlaySoundEffect(_impactSound, transform);
                dataCollector.RegisterShot(hitEnemy);
            }

            SpawnImpactParticles(hitEnemy);

            Destroy(gameObject);
        }

        private void HandleEnemyBulletCollision(Collider2D collider)
        {
            var hitDamageable = collider.GetComponentInChildren<IDamageable>();
            bool hitEnemy = collider.GetComponent<Enemies.Enemy>() != null;

            if (hitDamageable != null)
            {
                hitDamageable.TakeDamage(_damage);
            }
            else
            {
                // Play default impact sound if no IDamageable is hit
                Audio.SoundEffectPlayer.PlaySoundEffect(_impactSound, transform);
            }

            SpawnImpactParticles(hitEnemy);

            Destroy(gameObject);
        }

        private void SpawnImpactParticles(bool hitEnemy)
        {
            var particleSystem = Instantiate(_impactParicleSystem, transform.position, Quaternion.identity);
            var mainModule = particleSystem.main;

            particleSystem.transform.up = -transform.up;

            mainModule.startColor = hitEnemy
                ? _enemyImpactColor
                : _wallImpactColor;
        }
    }
}
