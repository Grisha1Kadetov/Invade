using Core.Scripts.Entities.Abilities;

namespace Core.Scripts.Entities.Commands
{
    public class JumpCommand : Command
    {
        private readonly float _velocity;
        
        public JumpCommand()
        {
            AbilityOverride = AbilityOverrideType.NotOverride;
            AddHandledType<JumpAbility>();
        }
        
        public JumpCommand(float velocity, AbilityOverrideType abilityOverride = AbilityOverrideType.OverrideNotSave)
        {
            AbilityOverride = abilityOverride;
            _velocity = velocity;
            AddHandledType<JumpAbility>();
        }
        
        public override void Execute(Ability ability)
        {
            if (ability is not JumpAbility jumpAbility) return;
            var jumpOldVelocity = jumpAbility.JumpVelocity;
            if(AbilityOverride != AbilityOverrideType.NotOverride)
                jumpAbility.JumpVelocity = _velocity;
            jumpAbility.Execute();
            if(AbilityOverride == AbilityOverrideType.OverrideNotSave)
                jumpAbility.JumpVelocity = jumpOldVelocity;
        }
    }
}