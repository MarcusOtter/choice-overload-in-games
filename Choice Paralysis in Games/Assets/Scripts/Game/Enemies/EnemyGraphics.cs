using UnityEngine;

namespace Scripts.Game.Enemies
{
    [RequireComponent(typeof(Animator))]
    public class EnemyGraphics : MonoBehaviour
    {
        [Header("Gun sprite settings")]
        [SerializeField] private SpriteRenderer _gunSpriteRenderer;
        [SerializeField] private int _sortInFrontNumber = 6;
        [SerializeField] private int _sortBehindNumber = 4;

        [Header("Animator variables")]
        [SerializeField] private string _rotationAnimationName = "Rotation";
        [SerializeField] private string _damagedTriggerName = "TriggerDamaged";
        [SerializeField] private string _deadBoolName = "IsDead";

        [Header("Sorting group settings on death")]
        [SerializeField] private string _deathSortingLayerName = "Ground";
        [SerializeField] private int _deathSortingOrder = 1;

        private Animator _animator;
        private Transform _lookDirection;

        private float _rotationZ;
        private int _damagedTriggerHash;
        private int _deadBoolHash;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _lookDirection = transform.root.GetComponentInChildren<LookDirection>()?.transform;
            _damagedTriggerHash = Animator.StringToHash(_damagedTriggerName);
            _deadBoolHash = Animator.StringToHash(_deadBoolName);
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

        // Called by UnityEvent OnDeath in Enemy.cs
        public void PlayDeathAnimation()
        {
            _animator.SetBool(_deadBoolHash, true);
            ChangeToDeathSortingLayer();
        }


        private void ChangeToDeathSortingLayer()
        {
            var sortingGroup = transform.root.GetComponentInChildren<UnityEngine.Rendering.SortingGroup>();

            if (sortingGroup != null)
            {
                sortingGroup.sortingLayerName = _deathSortingLayerName;
                sortingGroup.sortingOrder = _deathSortingOrder;
            }
        }

        // Called by UnityEvent OnDamaged in Enemy.cs
        public void PlayDamagedAnimation()
        {
            _animator.SetTrigger(_damagedTriggerHash);
        }

    }
}
