using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor;
using UnityEngine;

namespace Hilam
{
    public class CameraShaker : Singleton<CameraShaker>
    {
        [SerializeField] Camera gameCamera;
        [SerializeField] CinemachineVirtualCamera virtualCamera;
        float shakeTimer;
        float shakeTimerTotal;

        float startingIntensity;

        public Camera GameCamera => gameCamera;
        public CinemachineVirtualCamera VirtualCamera => virtualCamera;
     
        
        protected override void Awake()
        {
            base.Awake();
            if (gameCamera == null)
            {
                gameCamera = Camera.main;
            }
        }
        
        public void ShakeCamera(float shakeIntensity, float shakeTime)
        {
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = shakeIntensity;
            shakeTimer = shakeTime;
            shakeTimerTotal = shakeTimer;
            startingIntensity = shakeIntensity;
        }
        
        void StopCameraShake()
        {
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain =
                Mathf.Lerp(startingIntensity, 0f, 1 - (shakeTimer / shakeTimerTotal));
        }
        
        void Update()
        {
            if (shakeTimer > 0)
            {
                shakeTimer -= Time.deltaTime;
                StopCameraShake();
            }
        }
    }
}
