using System;
using DG.Tweening;
using Sigtrap.Relays;
using UnityEngine;

namespace Hilam
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Health Settings")] 
        [SerializeField] private int _health = 5;
        private int _maxHealth;
        
        [Header("Movement Settings")]
        [SerializeField] private float _moveSpeed = 5f;

        [Header("Mouse Settings")] 
        [SerializeField] private GameObject _cursor;
        [SerializeField] private GameObject _staff;
        [SerializeField] private GameObject _staffImage;

        [Header("Shoot Settings")]
        [SerializeField] private Transform _shotPoint;
        [SerializeField] private Bullet _basicAttackBullet;
        [SerializeField] private float _shootRate = 0.5f;
        
        [Header("VFX")]
        [SerializeField] private ParticleSystem _shootVFX;
        [SerializeField] private ParticleSystem _playerHit;

        [Header("SFX")] 
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _walkSFX;
        [SerializeField] private AudioClip _shootSFX;
        [SerializeField] private AudioClip _hitSFX;

        private Rigidbody2D _rb;
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        
        private Vector2 _moveInput;
        private Vector2 _lookInput;

        private float _shootRateReset;

        #region Properties

        public int MaxHealth
        {
            get => _maxHealth;
        }
        public int Health
        {
            get => _health;
        }
        #endregion

        #region Events

        public Relay<int> PlayerHealthChanged { get; private set; } = new Relay<int>();

        #endregion
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();

            _shootRateReset = _shootRate;

            _maxHealth = _health;
        }
        

        private void Update()
        {
           CheckMoveInput();
           CheckLookInput();
           ResetShootRate();
           Shoot();
        }

        private void FixedUpdate()
        {
           MovePlayer();
        }

        #region Movement

        private void CheckMoveInput()
        {
            _moveInput.x = Input.GetAxisRaw("Horizontal");
            _moveInput.y = Input.GetAxisRaw("Vertical");;
            
            if (_moveInput.sqrMagnitude > 1)
                _moveInput = _moveInput.normalized;
            
            CheckMoveAnimaton();
        }

        private void MovePlayer()
        {
            Vector2 targetVelocity = _moveInput * _moveSpeed;
            _rb.velocity = targetVelocity;
        }

        private void CheckMoveAnimaton()
        {
            _animator.SetBool("IsMoving", _moveInput.magnitude > 0);
        }
        
        #endregion

        #region Shoot & Aim

        private void CheckLookInput()
        {
            _lookInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _cursor.transform.position = _lookInput;
            
            RotatePlayer();
            RotateStaff();
        }
        private void RotatePlayer()
        {
            transform.localScale = new Vector3(_lookInput.x < transform.position.x ? -1: 1 , 1, 1);
        }

        private void RotateStaff()
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0f;
            
            Vector2 aimDirection = mouseWorldPosition - _staff.transform.position;
            
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            _staff.transform.rotation = Quaternion.Euler(0,0,angle - 90);
        }

        private void ResetShootRate()
        {
            if (_shootRate < _shootRateReset)
            {
                _shootRate += Time.deltaTime;
            }
            else
            {
                _shootRate = _shootRateReset;
            }
        }
        private void Shoot()
        {
            if (Input.GetMouseButton(0) && Math.Abs(_shootRate - _shootRateReset) < 0.1f)
            {
                Bullet bullet = Instantiate(_basicAttackBullet, _shotPoint.transform.position, Quaternion.identity);
                bullet.InitializeBullet(_shotPoint.transform.up);

                _shootRate = 0;

                _staffImage.transform.DOLocalMoveY(0, _shootRateReset / 8).SetEase(Ease.OutCirc).OnComplete(()=> _staffImage.transform.DOLocalMoveY(0.121f, _shootRateReset / 4).SetEase(Ease.OutCirc));
                CameraShaker.Instance.ShakeCamera(2,_shootRateReset / 8);
                
                _shootVFX.Play();
                _audioSource.PlayOneShotWithRandomPitch(_shootSFX);
            }
        }
        #endregion

        #region Health

        public void TakeDamage()
        {
            _health--;
            PlayerHealthChanged.Dispatch(_health);
            
            _playerHit.Play();
            _audioSource.PlayOneShotWithRandomPitch(_hitSFX);
            StartCoroutine(MaterialManager.Instance.ChangeMaterialToHit(_spriteRenderer));
        }

        #endregion

        #region FX

        public void PlayMoveSFX()
        {
            _audioSource.PlayOneShotWithRandomPitch(_walkSFX);
        }
        #endregion
    }
}
