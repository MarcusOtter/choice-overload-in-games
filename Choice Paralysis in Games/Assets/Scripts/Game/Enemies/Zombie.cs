using System.Collections;
using UnityEngine;

namespace Scripts.Game.Enemies
{
    public class Zombie : Enemy
    {
        [Header("Zombie settings")]
        [SerializeField] private uint _collisionDamage;
        [SerializeField] private float _repelRange = 3f;
        [SerializeField] private float _repelFactor = 0.2f;

        [Header("Zombie audio")]
        [SerializeField] private Audio.SoundEffect _zombieGruntAudio;
        [SerializeField] private float _gruntDelayMin = 5f;
        [SerializeField] private float _gruntDelayMax = 20f;

        private Vector2 _walkDirection;

        protected override void Awake()
        {
            base.Awake();
            StartCoroutine(PlayOccasionalGruntSound());
        }

        protected override void Move()
        {
            if (PlayerTransform == null) { return; }
            _walkDirection = (PlayerTransform.position - transform.position).normalized;
            Rigidbody.velocity = (_walkDirection + GetRepelForce()) * MovementSpeed;
        }

        /// <summary>Returns a force that is calculated depending on nearby enemies with colliders.</summary>
        private Vector2 GetRepelForce()
        {
            var collidersWithinRepelRange = Physics2D.OverlapCircleAll(transform.position, _repelRange);

            var repelForce = Vector2.zero;
            foreach (var col in collidersWithinRepelRange)
            {
                if (col.transform.root.GetComponentInChildren<Enemy>() == null) { continue; }

                // ReSharper disable once RedundantCast (it is not redundant)
                repelForce += (Vector2) (transform.position - col.transform.position).normalized;
            }

            return repelForce * _repelFactor;
        }

        private IEnumerator PlayOccasionalGruntSound()
        {
            while (!IsDead)
            {
                Audio.SoundEffectPlayer.PlaySoundEffect(_zombieGruntAudio, transform);
                yield return new WaitForSeconds(Random.Range(_gruntDelayMin, _gruntDelayMax));
            }
        }

        // Should maybe be sent to the weaponbehaviour of zombies
        private void OnCollisionEnter2D(Collision2D other)
        {
            var colRoot = other.collider.transform.root;

            if (colRoot.CompareTag(EnvironmentVariables.PlayerTag))
            {
                colRoot.GetComponentInChildren<IDamageable>()?.TakeDamage((int) _collisionDamage);
            }
        }
    }
}
