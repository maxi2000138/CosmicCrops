﻿using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts.Infrastructure.Input
{
    public sealed class JoystickService : MonoBehaviour, IJoystickService, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform _movementArea;
        [SerializeField] private RectTransform _handle;
        [SerializeField] private RectTransform _thumb;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _valueMultiplier = 1f;
        [SerializeField] private float _movementAreaRadius = 50f;
        [SerializeField] private float _deadZoneRadius = 0.1f;
        [SerializeField] private float _fadeTime = 4f;
        [SerializeField] private bool _isStatick = false;

        private Vector3 _startPosition;
        private float _loverMovementAreaRadius;
        private float _movementAreaRadiusSqr;
        private float _deadZoneAreaRadiusSqr;
        private float _opacity;
        private bool _joystickHeld;
        private bool _isEnable;

        private Vector2 _axis;

        void IJoystickService.Init()
        {
            _axis = Vector2.zero;
            _opacity = 0f;
            _canvasGroup.alpha = _opacity;
            _startPosition = _handle.position;
            _loverMovementAreaRadius = 1f / _movementAreaRadius;
            _movementAreaRadiusSqr = Mathf.Pow(_movementAreaRadius, 2f);
            _deadZoneAreaRadiusSqr = Mathf.Pow(_deadZoneRadius, 2f);
        }

        Vector2 IJoystickService.GetAxis() => _axis;

        float IJoystickService.GetDeadZone() => _deadZoneRadius;

        void IJoystickService.Enable(bool isEnable)
        {
            _isEnable = isEnable;

            if (isEnable == false)
            {
                _joystickHeld = false;
            
                if (_isStatick)
                {
                    _handle.position = _startPosition;
                }
            
                _thumb.localPosition = Vector3.zero;
            
                _axis = Vector2.zero;
            }
        }

        void IJoystickService.Execute()
        {
            _opacity = _joystickHeld ? 
                Mathf.Min(1f, _opacity + Time.deltaTime * _fadeTime) : 
                Mathf.Max(0f, _opacity - Time.deltaTime * _fadeTime);

            _canvasGroup.alpha = _opacity;
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (_isEnable == false) return;
            
            _joystickHeld = true;

            if (_isStatick)
            {
                _handle.position = _startPosition;
            }
            else
            {
                RectTransformUtility.ScreenPointToWorldPointInRectangle
                    (_movementArea, eventData.position, eventData.pressEventCamera, out Vector3 position);

                _handle.position = position;
            }
        }
        
        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            if (_isEnable == false) return;

            RectTransformUtility.ScreenPointToLocalPointInRectangle
                (_handle, eventData.position, eventData.pressEventCamera, out Vector2 direction);

            if (direction.sqrMagnitude < _deadZoneAreaRadiusSqr)
            {
                _axis = Vector2.zero;
            }
            else
            {
                if (direction.sqrMagnitude > _movementAreaRadiusSqr)
                {
                    direction = direction.normalized * _movementAreaRadius;
                }

                _axis = direction * _loverMovementAreaRadius * _valueMultiplier;
            }
            
            _thumb.localPosition = direction;
        }
        
        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            if (_isEnable == false) return;
            
            _joystickHeld = false;
            
            if (_isStatick)
            {
                _handle.position = _startPosition;
            }
            
            _thumb.localPosition = Vector3.zero;

            _axis = Vector2.zero;
        }
    }
}