using System;
using Scripts.Game.Weapons;
using UnityEngine;

namespace Scripts.Game.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerGraphics : MonoBehaviour
    {
        [Header("Necessary references")]
        [SerializeField] private Transform _gunRotation;

        [Header("Weapon graphics")]
        [SerializeField] private SpriteRenderer _weaponSpriteRenderer;
        [SerializeField] private int _sortInFrontNumber = 11;
        [SerializeField] private int _sortBehindNumber = 9;
        [SerializeField] private Transform _bulletSpawnPoint;

        [Header("Weapon animation")]
        [SerializeField] private Animator _weaponAnimator;
        [SerializeField] private string _recoilTriggerName = "TriggerRecoil";

        [Header("Movement animation")]
        [SerializeField] private string _speedParameterName = "Speed";
        [SerializeField] private string _skipTriggerName = "TriggerSkip";

        [Header("Death animation")]
        [SerializeField] private string _deathTriggerName = "TriggerDeath";

        [Header("Audio settings")]
        [SerializeField] private Audio.SoundEffect _skipSound;

        private int _recoilTriggerHash;
        private int _speedParameterHash;
        private int _skipTriggerHash;
        private int _deathTriggerHash;

        private Animator _bodyAnimator;
        private Rigidbody2D _rigidbody;

        private float _rotationZ;

        private float _bulletSpawnPointXOffset;

        private void OnEnable()
        {
            PlayerWeapon.OnWeaponFire += TriggerSkip;
            PlayerWeapon.OnWeaponFire += TriggerWeaponRecoil;
        }

        private void Start()
        {
            _rigidbody = transform.root.GetComponent<Rigidbody2D>();
            _bulletSpawnPointXOffset = _bulletSpawnPoint.localPosition.x;

            _bodyAnimator = GetComponent<Animator>();
            _speedParameterHash = Animator.StringToHash(_speedParameterName);
            _skipTriggerHash = Animator.StringToHash(_skipTriggerName);
            _recoilTriggerHash = Animator.StringToHash(_recoilTriggerName);
            _deathTriggerHash = Animator.StringToHash(_deathTriggerName);
        }

        private void Update()
        {
            _rotationZ = _gunRotation.localEulerAngles.z;

            // Set sorting order of gun
            _weaponSpriteRenderer.sortingOrder = _rotationZ > 90 && _rotationZ < 270
                ? _sortInFrontNumber
                : _sortBehindNumber;

            // Set the flipY of gun sprite
            _weaponSpriteRenderer.flipY = _rotationZ < 180;

            // Flip the weapon spawner offset (for weapons that don't spawn from the middle of the sprite)
            _bulletSpawnPoint.localPosition =
                new Vector3(_rotationZ < 180 ? -_bulletSpawnPointXOffset : _bulletSpawnPointXOffset,
                    _bulletSpawnPoint.localPosition.y, 0);

            // Update the float in the animator
            _bodyAnimator.SetFloat(_speedParameterHash, Mathf.Abs(_rigidbody.velocity.x) + Mathf.Abs(_rigidbody.velocity.y));
        }

        // Makes the player jump whenever a bullet is fired from the player weapon
        private void TriggerSkip(object sender, EventArgs args)
        {
            _bodyAnimator.SetTrigger(_skipTriggerHash);
        }

        private void TriggerWeaponRecoil(object sender, EventArgs args)
        {
            _weaponAnimator.SetTrigger(_recoilTriggerHash);
        }

        // Called by PlayerDeathBehaviour
        internal void PlayDeathAnimation()
        {
            _bodyAnimator.SetTrigger(_deathTriggerHash);
        }

        // Called by animation event
        public void PlaySkipSound()
        {
            Audio.SoundEffectPlayer.PlaySoundEffect(_skipSound, transform, false);
        }

        private void OnDisable()
        {
            PlayerWeapon.OnWeaponFire -= TriggerSkip;
            PlayerWeapon.OnWeaponFire -= TriggerWeaponRecoil;
        }
    }
}
