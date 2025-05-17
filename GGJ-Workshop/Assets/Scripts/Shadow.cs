using System;
using UnityEngine;

namespace Hilam
{
    public class Shadow : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            _spriteRenderer.enabled = GameFeelBooleans.Instance.Shadows;
        }
    }
}
