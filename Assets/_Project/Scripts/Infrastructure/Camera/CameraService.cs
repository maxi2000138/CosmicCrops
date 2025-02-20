using System;
using _Project.Scripts.UI.Screens;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

namespace _Project.Scripts.Infrastructure.Camera
{
    public sealed class CameraService : MonoBehaviour, ICameraService
    {
        [SerializeField] private UnityEngine.Camera _camera;
        [SerializeField] private CinemachineCamera _cameraZoomIn;
        [SerializeField] private CinemachineCamera _cameraZoomOut;
        [SerializeField] private ShakeSettings _shakeSettings;
        
        private CinemachineBasicMultiChannelPerlin _basicMultiChannelPerlin;
        private Tween _shakeTween;

        UnityEngine.Camera ICameraService.Camera => _camera;

        void ICameraService.Init()
        {
            _basicMultiChannelPerlin = _cameraZoomIn.GetComponent<CinemachineBasicMultiChannelPerlin>();
        }

        void ICameraService.SetTarget(Transform target)
        {
            _cameraZoomIn.Follow = target;
            _cameraZoomIn.LookAt = target;
            _cameraZoomOut.Follow = target;
            _cameraZoomOut.LookAt = target;
        }

        void ICameraService.ActivateCamera(ScreenType type)
        {
            _cameraZoomIn.Priority = type == ScreenType.Game ? 100 : 0;
            _cameraZoomOut.Priority = type == ScreenType.Game ? 0 : 100;
        }

        void ICameraService.Shake()
        {
            _shakeTween?.Kill();
            _shakeTween = DOVirtual.Float(_shakeSettings.Amplitude, 0f, _shakeSettings.Duration, SetAmplitude);
        }

        bool ICameraService.IsOnScreen(Vector3 viewportPoint) => viewportPoint is { x: > 0f and < 1f, y: > 0f and < 1f };

        void ICameraService.Cleanup()
        {
            _cameraZoomIn.Follow = null;
            _cameraZoomIn.LookAt = null;
            _cameraZoomOut.Follow = null;
            _cameraZoomOut.LookAt = null;
            _shakeTween?.Kill();
        }

        private void SetAmplitude(float value) => _basicMultiChannelPerlin.AmplitudeGain = value;
    }

    [Serializable]
    public struct ShakeSettings
    {
        public float Amplitude;
        public float Duration;
    }
}