using UnityEngine;

namespace Core.Scripts.Entities.Abilities
{
    public class JumpAbility : Ability
    {
        [SerializeField]
        private float _jumpVelocity = 10f;
        public float JumpVelocity {get => _jumpVelocity; set => _jumpVelocity = value;}

        protected override void Awake()
        {
            base.Awake();
            OwnedEntity.RegisterAbility<JumpAbility>(this);
        }

        public override void Execute()
        {
            OwnedEntity.Rigidbody.linearVelocity += Vector3.up * JumpVelocity;
        }

        private void OnDestroy()
        {
            OwnedEntity.UnregisterAbility<JumpAbility>();
        }
    }
}