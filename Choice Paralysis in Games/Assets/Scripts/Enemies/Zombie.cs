using UnityEngine;

namespace Scripts.Enemies
{
    public class Zombie : Enemy
    {
        [Header("Zombie settings")]
        [SerializeField] private uint _collisionDamage;
        [SerializeField] private float _repelRange = 3f;
        [SerializeField] private float _repelFactor = 0.2f;

        private Vector2 _walkDirection;

        protected override void Move()
        {
            if (PlayerTransform == null) { return; }
            _walkDirection = (PlayerTransform.position - transform.position).normalized;
            Rigidbody.velocity = (_walkDirection + GetRepelForce()) * MovementSpeed;
        }

        /// <summary>Returns a force that is calculated depending on nearby enemies.</summary>
        private Vector2 GetRepelForce()
        {
            Collider2D[] collidersWithinRepelRange = Physics2D.OverlapCircleAll(transform.position, _repelRange);

            var repelForce = Vector2.zero;
            foreach (var col in collidersWithinRepelRange)
            {
                if (col.transform.root.GetComponentInChildren<Enemy>() == null) { continue; }

                repelForce += (Vector2)(transform.position - col.transform.position).normalized;
            }

            return repelForce * _repelFactor;
        }

        // Should maybe be sent to the weaponbehaviour of zombies
        private void OnCollisionEnter2D(Collision2D other)
        {
            var colRoot = other.collider.transform.root;

            if (colRoot.CompareTag("Player"))
            {
                colRoot.GetComponentInChildren<IDamageable>()?.TakeDamage((int)_collisionDamage);
            }
        }
    }
}
