using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Hilam
{
    public class Enemy : MonoBehaviour
    {
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody2D;
        
        [Header("Damage Settings")]
        public float damageRadius = 2f;
        public float damageInterval = 2f;
        public LayerMask damageableLayer;
        
        [Header("VFX")]
        [SerializeField] private ParticleSystem _shotVFX;

        [Header("Sword VFX")] 
        [SerializeField] private GameObject _swordObject;

        [Header("SFX")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _hitSFX;
        [SerializeField] private AudioClip _attackSFX;
        private Coroutine damageCoroutine;
        
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
        }
        
        private void OnEnable()
        {
            damageCoroutine = StartCoroutine(DamageOverTime());
        }

        private void OnDisable()
        {
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
            }
        }
        
        public void TakeDamage(Vector2 hitPosition)
        {
            Debug.Log("Damage Taken");

            StartCoroutine(_rigidbody2D.KnockbackEntityOverTime(-hitPosition.normalized,1,0.1f));
            StartCoroutine(MaterialManager.Instance.ChangeMaterialToHit(_spriteRenderer));
            
            _shotVFX.Play();
            _audioSource.PlayOneShotWithRandomPitch(_hitSFX);
        }
        
        private IEnumerator DamageOverTime()
        {
            while (true)
            {
                Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, damageRadius, damageableLayer);
                
                foreach (Collider2D hit in hitObjects)
                {
                    PlayerController targetPlayer = hit.GetComponent<PlayerController>();
                    if (targetPlayer != null)
                    {
                        PlayAttackVFX(targetPlayer.transform.position);
                        _animator.SetTrigger("Attack");
                        targetPlayer.TakeDamage();
                        
                        CameraShaker.Instance.ShakeCamera(4,0.2f);
                        StartCoroutine(_rigidbody2D.KnockbackEntityOverTime( -(transform.position - targetPlayer.transform.position).normalized,4,0.1f));
                        
                        _audioSource.PlayOneShotWithRandomPitch(_attackSFX);
                    }
                }
                
                yield return new WaitForSeconds(damageInterval);
            }
        }

        private void PlayAttackVFX(Vector2 playerPosition)
        {
            _swordObject.SetActive(true);
            
            Vector3 enemyPosition = transform.position;
            enemyPosition.z = 0f;
            
            Vector2 aimDirection = enemyPosition - (Vector3) playerPosition;
            
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            _swordObject.transform.rotation = Quaternion.Euler(0,0,angle + 30);

            _swordObject.transform.DORotate(new Vector3(0, 0, angle - 30), 0.2f).SetEase(Ease.InCubic).OnComplete(() =>
            {
                _swordObject.SetActive(false);
            });
        }
    }
}
