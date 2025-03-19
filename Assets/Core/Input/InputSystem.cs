using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace Core.Input
{
    public class InputSystem : ITickable
    {
        public event Action<Vector2> OnMove;
        public event Action OnJumpHold;
        public event Action OnJumpPerformed;
        public event Action OnJumpCanceled;

        private readonly GameInput _gameInput = new();
        private bool _isJumpHold = false;
        
        public InputSystem()
        {
            _gameInput.Enable();
            _gameInput.Player.Jump.performed += JumpPerformed; 
            _gameInput.Player.Jump.canceled += JumpCanceled;
        }

        private void JumpPerformed(InputAction.CallbackContext input)
        {
            _isJumpHold = true;
            OnJumpPerformed?.Invoke();
        }

        private void JumpCanceled(InputAction.CallbackContext input)
        {
            _isJumpHold = false;
            OnJumpCanceled?.Invoke();
        }


        public void Tick()
        {
            OnMove?.Invoke(_gameInput.Player.Move.ReadValue<Vector2>());
            if(_isJumpHold)
                OnJumpHold?.Invoke();
        }
    }
}
