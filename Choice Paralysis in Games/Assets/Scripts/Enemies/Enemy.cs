﻿using UnityEngine;

namespace Scripts.Enemies
{
    /// <summary>Base class for enemies</summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class Enemy : MonoBehaviour, IDamageable
    {
        [Header("General Enemy Settings")]
        [SerializeField] protected float MaxHealth;
        [SerializeField] protected float MovementSpeed;

        protected float Health;

        protected Rigidbody2D Rigidbody;
        protected Transform PlayerTransform;
        protected EnemyGraphics EnemyGraphics;

        protected virtual void Start()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            PlayerTransform = GameObject.FindGameObjectWithTag("Player")?.transform.root;
            EnemyGraphics = GetComponentInChildren<EnemyGraphics>();

            Health = MaxHealth;
        }

        protected virtual void FixedUpdate()
        {
            Move();
        }

        protected virtual void Move()
        {
            // Overwritten
        }

        public virtual void TakeDamage(int incomingDamage)
        {
            Health -= incomingDamage;
            EnemyGraphics?.PlayDamagedAnimation();

            if (Health <= 0) { Die(); }
        }

        protected virtual void Die()
        {
            Destroy(gameObject);
        }
    }
}

