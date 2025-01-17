using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hilam
{
    public class MaterialManager : Singleton<MaterialManager>
    {
        [SerializeField] private Material _hitMaterial;

        public IEnumerator ChangeMaterialToHit(SpriteRenderer spriteRenderer, float hitChangeTime = 0.1f)
        {
            var originalMaterial = spriteRenderer.material;
            spriteRenderer.material = _hitMaterial;
            
            yield return new WaitForSeconds(hitChangeTime);

            spriteRenderer.material = originalMaterial;
        }
    }
}
