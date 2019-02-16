using System.Collections;
using UnityEngine;

namespace Scripts.Game.Weapons
{
    [RequireComponent(typeof(Rigidbody2D))]

    public class FireworkRocket : MonoBehaviour
    {
        [Header("Audio")]
        [SerializeField] private AudioClip _shotClip;
        [SerializeField] private AudioClip _explosionClip;

        [Header("Explosion")]
        [SerializeField] private Bullet _fireworkProjectilePrefab;
        [SerializeField] private int _minProjectileAmount = 10;
        [SerializeField] private int _maxProjectileAmount = 60;
        [SerializeField] private Color[] _availableProjectileColors;

        [Header("Other")]
        [SerializeField] private float _rotationSpeed;

        private int _damage;
        private Rigidbody2D _rigidbody;

        private float _speed;

        private Quaternion _targetRotation;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        internal void Shoot(int damage, float speed)
        {
            _damage = damage;
            _speed = speed;
            StartCoroutine(TravelInRandomDirection());
            //Audio.AudioPlayer.Instance.PlaySoundEffect(_shotClip, 1f);
        }

        private IEnumerator TravelInRandomDirection()
        {
            while (true)
            {
                var travelTime = Random.Range(0.1f, 0.5f);
                var randomOffsetDegrees = Random.Range(-90, 90);

                _targetRotation = Quaternion.AngleAxis(randomOffsetDegrees, Vector3.forward) * transform.rotation;

                //_rigidbody.MoveRotation(randomOffsetDegrees); //= direction;
                yield return new WaitForSeconds(travelTime);
            }
        }

        private void Update()
        {
            transform.rotation =
                Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * _rotationSpeed);

            _rigidbody.velocity = _speed * transform.up;

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

            //Audio.AudioPlayer.Instance.PlaySoundEffect(_explosionClip, 1f);
            //Audio.AudioPlayer.Instance.PlaySoundEffect3D(Audio.SoundEffect.BulletImpact, transform.position);

            int projectileAmount = Random.Range(_minProjectileAmount, _maxProjectileAmount + 1);

            for (int i = 0; i < projectileAmount; i++)
            {
                var randomRotation = Random.Range(-360f, 360f);
                var projectile = Instantiate(_fireworkProjectilePrefab, transform.position,
                    Quaternion.AngleAxis(randomRotation, Vector3.forward) * transform.rotation);
                
                var randomColor = _availableProjectileColors[Random.Range(0, _availableProjectileColors.Length)];

                projectile.GetComponent<SpriteRenderer>().color = randomColor;

                var trailRenderer = projectile.GetComponent<TrailRenderer>();
                trailRenderer.startColor = randomColor;
                trailRenderer.endColor = randomColor;

                projectile.Shoot(1, 15);
            }

            Destroy(gameObject);
        }
    }
}
