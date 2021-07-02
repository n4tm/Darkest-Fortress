using Cinemachine;
using UnityEngine;

namespace Camera
{
    public class CinemachineController : MonoBehaviour
    {
        public static CinemachineController Instance { get; private set; }
        private CinemachineVirtualCamera _cinemachineVirtualCamera;
        private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;

        private float _shakeTime;
        private float _shakeTimer;
        private float _shakeIntensity;

        private void Awake()
        {
            Instance = this;
            _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
            _cinemachineBasicMultiChannelPerlin = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        private void Update()
        {
            if (_shakeTimer <= 0) return;
            _shakeTimer -= Time.deltaTime;
            _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain =
                Mathf.Lerp(_shakeIntensity, 0f, 1 - (_shakeTimer / _shakeTime));
        }

        public void StartShake(float intensity, float time)
        {
            _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
            _shakeIntensity = intensity;
            _shakeTime = time;
            _shakeTimer = time;
        }
    }
}
