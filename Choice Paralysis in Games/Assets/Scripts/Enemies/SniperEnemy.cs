﻿using Scripts.Weapons;
using UnityEngine;

namespace Scripts.Enemies
{
    public class SniperEnemy : Enemy
    {
        [Header("Roam movement settings")]
        [SerializeField] private float _minimumWaitTime;
        [SerializeField] private float _maximumWaitTime;

        private float _nextDirectionChangeTime;
        private Vector2 _moveDir;

        private Sniper _sniper;

        private void Awake()
        {
            _sniper = GetComponentInChildren<Sniper>();
        }

        protected override void FixedUpdate()
        {
            if (_sniper.IsShooting)
            {
                Rigidbody.velocity = Vector2.zero;
                return;
            }

            Move();
        }

        protected override void Move()
        {
            if (Time.time > _nextDirectionChangeTime)
            {
                _moveDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                _nextDirectionChangeTime = Time.time + Random.Range(_minimumWaitTime, _maximumWaitTime);
            }

            Rigidbody.velocity = _moveDir * MovementSpeed;
        }
    }
}

