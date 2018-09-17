using System.Collections;
using UnityEngine;

namespace Scripts.Enemies
{
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
    public class EnemyGraphics : MonoBehaviour
    {
        private Enemy _enemy;
        private Animator _animator;
        private Transform _direction;
        private SpriteRenderer _spriteRenderer;

        private Color _startColor;

        private void Start()
        {
            _enemy = GetComponentInParent<Enemy>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _startColor = _spriteRenderer.color;

            _direction = Instantiate(new GameObject("Direction Pointer"), transform).transform;
        }

        private void Update()
        {
            _direction.up = _enemy.Direction;
            _animator.SetFloat("Rotation", _direction.localEulerAngles.z);
            //Debug.Log(_direction.localEulerAngles.z);
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

