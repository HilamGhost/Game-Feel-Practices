using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hilam
{
    public class GameFeelBooleans : Singleton<GameFeelBooleans>
    {
        public bool Shadows;
        public bool Animations;
        public bool ShootAnimation;
        public bool HitShader;
        public bool CameraShake;
        public bool CameraDelay;
        public bool Knockback;
        public bool Particles;
        public bool SFXs;


        public void ChangeShadow(bool toggle)
        {
            Shadows = toggle;
        }
        public void ChangeAnimations(bool toggle)
        {
            Animations = toggle;
        }
        public void ChangeShootAnimations(bool toggle)
        {
            ShootAnimation = toggle;
        }
        public void ChangeHitShader(bool toggle)
        {
            HitShader = toggle;
        }
        public void ChangeCameraShake(bool toggle)
        {
            CameraShake = toggle;
        }
        public void ChangeCameraDelay(bool toggle)
        {
            CameraDelay = toggle;
        }
        public void ChangeKnockback(bool toggle)
        {
            Knockback = toggle;
        }
        public void ChangeParticles(bool toggle)
        {
            Particles = toggle;
        }
        public void ChangeSFX(bool toggle)
        {
            SFXs = toggle;
        }
    }
}
