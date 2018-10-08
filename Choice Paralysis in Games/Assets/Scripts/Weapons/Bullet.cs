using UnityEngine;

namespace Scripts.Weapons
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _selfDestructDelay = 5;
        private int _damage;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            Destroy(gameObject, _selfDestructDelay);
        }

        internal void Shoot(int damage, float speed)
        {
            _damage = damage;
            _rigidbody.AddForce(transform.up * speed, ForceMode2D.Impulse);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            HandleCollision(other);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            HandleCollision(collision.collider);
        }

        private void HandleCollision(Collider2D collider)
        {
            collider.GetComponentInChildren<IDamageable>()?.TakeDamage(_damage);

            // Spawn bullet effect
            Destroy(gameObject);
        }
    }
}
