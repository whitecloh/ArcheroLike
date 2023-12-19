using System;
using UnityEngine;

    public  class MovementInput
    {
        private Joystick _joystick;
        public event Action OnInputBegin;
        public event Action OnInputEnded;

        private Vector3 _movementDirection;

        private bool _isMoving = false;

    public void SetInput(Joystick joystick)
    {
        _joystick = joystick;
    }
    public void ClearEvents()
        {
            OnInputBegin = null;
            OnInputEnded = null;
        }
    private Vector3 DetectMovementDirection()
    {
        _movementDirection.x = _joystick.Horizontal;
        _movementDirection.z = _joystick.Vertical;
        return _movementDirection;
    }

    public Vector3 GetMovement()
        {
            _movementDirection = DetectMovementDirection();
            
            if(_movementDirection == Vector3.zero)
            {
                if(_isMoving)
                {
                    OnInputEnded?.Invoke();
                    _isMoving = false;
                }
            }
            else
            {
                if(!_isMoving)
                {
                    OnInputBegin?.Invoke();
                    _isMoving = true;
                }
            }
            return _movementDirection;
        }
    }
