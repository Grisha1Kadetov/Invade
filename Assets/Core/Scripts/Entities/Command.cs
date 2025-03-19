using System;
using System.Collections.Generic;

namespace Core.Scripts.Entities
{
    public abstract class Command
    {
        public ExecuteType ExecuteType { get; set; } = ExecuteType.All;
        protected AbilityOverrideType AbilityOverride = AbilityOverrideType.NotOverride; 
        
        public IReadOnlyCollection<Type> HandledTypes => _handledTypes;
        private readonly HashSet<Type> _handledTypes = new();
        public abstract void Execute(Ability ability);

        protected void AddHandledType<T>() where T : Ability
        {
            _handledTypes.Add(typeof(T));
        }
        
        protected void RemoveHandledType<T>() where T : Ability
        {
            _handledTypes.Remove(typeof(T));
        }

        public Command WithExecuteType(ExecuteType executeType)
        {
            ExecuteType = executeType;
            return this;
        }
    }
    
    public enum ExecuteType
    {
        First,
        All,
        Last,
    }
    
    public enum AbilityOverrideType
    {
        NotOverride,
        OverrideSave,
        OverrideNotSave,
    }
}