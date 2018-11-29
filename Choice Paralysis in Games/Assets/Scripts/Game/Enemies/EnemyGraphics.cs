using System.Collections;
using UnityEngine;

namespace Scripts.Game.Enemies
{
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
    public class EnemyGraphics : MonoBehaviour
    {
        [Header("Gun sprite settings")]
        [SerializeField] private SpriteRenderer _gunSpriteRenderer;
        [SerializeField] private int _sortInFrontNumber = 6;
        [SerializeField] private int _sortBehindNumber = 4;

        [Header("Body sprite settings")]
        [SerializeField] private string _rotationAnimationName = "Rotation";

        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private Transform _lookDirection;

        private Color _startColor;

        private float _rotationZ;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _startColor = _spriteRenderer.color;
            _lookDirection = transform.root.GetComponentInChildren<LookDirection>()?.transform;
        }

        private void Update()
        {
            _rotationZ = _lookDirection.localEulerAngles.z;
            _animator.SetFloat(_rotationAnimationName, _rotationZ);

            if (_gunSpriteRenderer == null) { return; }

            // Set sorting order of gun
            _gunSpriteRenderer.sortingOrder = _rotationZ > 90 && _rotationZ < 270
                ? _sortInFrontNumber
                : _sortBehindNumber;

            // Set the flipY of gun sprite
            _gunSpriteRenderer.flipY = _rotationZ < 180;
        }

        internal void PlayDamagedAnimation()
        {
            StartCoroutine(DamageAnimation());
        }

        // This could probably be replaced with actual animation clips
        // which would allow for more custom animation & reuseability
        private IEnumerator DamageAnimation()
        {
            _spriteRenderer.color = Color.red;

            yield return new WaitForSeconds(0.1f);
            _spriteRenderer.color = _startColor;
        }
    }
}

