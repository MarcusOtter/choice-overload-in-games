using System.Collections;
using UnityEngine;

namespace Scripts.Enemies
{
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
    public class EnemyGraphics : MonoBehaviour
    {
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private Transform _lookDirection;

        private Color _startColor;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _startColor = _spriteRenderer.color;
            _lookDirection = transform.root.GetComponentInChildren<LookDirection>()?.transform;
        }

        private void Update()
        {
            _animator.SetFloat("Rotation", _lookDirection.localEulerAngles.z);
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

