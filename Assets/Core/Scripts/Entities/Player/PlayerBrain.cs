using Core.Scripts.Entities.Commands;
using VContainer;
using InputSystem = Core.Input.InputSystem;

namespace Core.Scripts.Entities.Player
{
    public class PlayerBrain : Brain
    {
        private InputSystem _inputSystem;
        
        [Inject]
        public void Init(InputSystem input)
        {
            _inputSystem = input;
            _inputSystem.OnJumpPerformed += JumpPerformed;
        }

        private void OnEnable()
        {
            if (_inputSystem == null) return;
            _inputSystem.OnJumpPerformed -= JumpPerformed;
            _inputSystem.OnJumpPerformed += JumpPerformed;
        }

        private void OnDisable()
        {
            if (_inputSystem == null) return;
            _inputSystem.OnJumpPerformed -= JumpPerformed;
        }

        private void JumpPerformed()
        {
            InvokeCommand(new JumpCommand(10f));
        }

        public override void HandleInput()
        {
        }
    }
}