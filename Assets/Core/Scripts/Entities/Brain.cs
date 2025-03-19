using System;
using UnityEngine;

namespace Core.Scripts.Entities
{
    public abstract class Brain : MonoBehaviour
    {
        public event Action<Command> OnCommand;
        protected Entity OwnedEntity;
        
        public bool Enable { get; set; } = true; 

        protected virtual void Awake()
        {
            OwnedEntity = GetComponent<Entity>();
            OwnedEntity?.RegisterBrain(this);
        }

        protected void InvokeCommand(Command command)
        {
            if(!Enable) return;
            OnCommand?.Invoke(command);
        }
        
        public abstract void HandleInput();

        private void OnDestroy()
        {
            OwnedEntity?.UnregisterBrain(this);
        }
    }
}