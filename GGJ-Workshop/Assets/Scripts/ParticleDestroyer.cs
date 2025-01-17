using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hilam
{
    public class ParticleDestroyer : MonoBehaviour
    {
        [SerializeField] private float _destroyTime = 2;
        void Start()
        {
            Destroy(gameObject,_destroyTime);
        } 
    }
}
