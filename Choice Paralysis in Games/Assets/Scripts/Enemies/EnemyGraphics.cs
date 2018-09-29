using System.Collections;
using UnityEngine;

namespace Scripts.Enemies
{
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
    public class EnemyGraphics : MonoBehaviour
    {
        private Animator _animator;
        private Weapons.Weapon _weapon;
        private SpriteRenderer _spriteRenderer;

        private Color _startColor;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _startColor = _spriteRenderer.color;
            _weapon = transform.root.GetComponentInChildren<Weapons.Weapon>();
            // Instantiate an object that displays the direction of the enemy to the player
            //_weapon = Instantiate(new GameObject("Direction Pointer"), transform).transform;
        }

        private void Update()
        {
            if (_weapon == null) { return; }

            _animator.SetFloat("Rotation", _weapon.Rotation);
        }

        internal void PlayDamagedAnimation()
        {
            StartCoroutine(DamageAnimation());
        }

        private IEnumerator DamageAnimation()
        {
            _spriteRenderer.color = Color.red;

            yield return new WaitForSeconds(0.1f);
            _spriteRenderer.color = _startColor;
        }
    }
}

