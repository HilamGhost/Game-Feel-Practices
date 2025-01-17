using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hilam
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _bulletSpeed;

        private Rigidbody2D _rigidbody2D;
        public virtual void InitializeBullet(Vector2 direction)
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.AddForce(direction * _bulletSpeed, ForceMode2D.Impulse);
            Destroy(gameObject,5);
        }

        public virtual void DestroyBullet()
        {
            Destroy(gameObject);
        }
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.transform.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(enemy.transform.position - transform.position);
                DestroyBullet();
            }
        }
    }
}
