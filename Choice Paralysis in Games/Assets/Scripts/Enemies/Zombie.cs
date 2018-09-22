using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Enemies
{
    public class Zombie : Enemy
    {
        [Header("Zombie settings")]
        [SerializeField] private uint _collisionDamage;

        protected override void Move()
        {
            if (PlayerTransform == null) { return; }
            LookDirection = (PlayerTransform.position - transform.position).normalized;
            Rigidbody.velocity = LookDirection * MovementSpeed;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var colRoot = other.collider.transform.root;

            if (colRoot.CompareTag("Player"))
            {
                colRoot.GetComponent<IDamageable>()?.TakeDamage((int)_collisionDamage);
                Debug.Log("Colliding with player");
            }
        }
    }
}
