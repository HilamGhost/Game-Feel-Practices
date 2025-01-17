using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hilam
{
    public static class Extensions
    {
        public static IEnumerator KnockbackEntityOverTime(this Rigidbody2D rigidbody2D,Vector3 direction, float knockbackPower, float knockBackTime)
        {
            float elapsedTime = 0f;
            
            Vector3 initialPosition = rigidbody2D.transform.position;
            float entityVelocityMagnitude = Mathf.Clamp(rigidbody2D.velocity.magnitude, 1, 1.5f);
            Vector3 finalDestination = rigidbody2D.transform.position + (direction * (-knockbackPower * entityVelocityMagnitude * knockBackTime));
            
            
            while (elapsedTime < knockBackTime)
            {
                float t = elapsedTime / knockBackTime;
                float easedT = t * t * (3f - 2f * t);

                Vector3 newPosition = Vector3.Lerp(initialPosition, finalDestination, easedT);
               
                float currentDistance = Vector3.Distance(rigidbody2D.transform.position, finalDestination);
                float newDistance = Vector3.Distance(newPosition, finalDestination);
                
                if (newDistance > currentDistance)
                {
                    break;
                }

                rigidbody2D.MovePosition(newPosition);
                
                elapsedTime += Time.deltaTime;

                yield return new WaitForFixedUpdate();
            }
        }
        
        public static void PlayOneShotWithRandomPitch(this AudioSource audioSource, AudioClip audioClip, float minPitch = 0.8f, float maxPitch = 1.2f)
        {
            float oldPitch = audioSource.pitch;
            float newPitch = 1;
            while (Mathf.Abs(oldPitch-newPitch) <= 0.1f )
            {
                newPitch = Random.Range(minPitch, maxPitch);
            }
            
            audioSource.pitch = newPitch;
            audioSource.PlayOneShot(audioClip);
        }
    }
}
