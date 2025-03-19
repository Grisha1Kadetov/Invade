using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Scripts.Entities
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public abstract class Entity : MonoBehaviour
    {
        public Collider Collider
        {
            get
            {
                if (_collider == null)
                    _collider = GetComponent<Collider>();
                return _collider;
            }
        }
        private Collider _collider;
        public Rigidbody Rigidbody
        {
            get
            {
                if (_rigidbody == null)
                    _rigidbody = GetComponent<Rigidbody>();
                return _rigidbody;
            }
        }
        private Rigidbody _rigidbody;
        public Animator Animator { get; private set; }
        
        private readonly Dictionary<Type, Ability> _abilities = new();
        private List<Brain> _brains = new();

        protected virtual void Awake()
        {
            var (a, b) = (Rigidbody, Collider);
            Animator = GetComponent<Animator>();
        }

        public void RegisterBrain(Brain brain)
        {
            _brains.Add(brain);
            brain.OnCommand += ExecuteCommand;
        }
        public void UnregisterBrain(Brain brain)
        {
            _brains.Remove(brain);
            brain.OnCommand -= ExecuteCommand;
        }

        public void RegisterAbility<T>(Ability ability) where T : Ability
        {
            if (_abilities.ContainsKey(typeof(T))) return;
            Debug.Log($"Registering ability {typeof(T).Name} On {gameObject.name}");
            _abilities.Add(typeof(T), ability);
        }
        
        public void UnregisterAbility<T>() where T : Ability
        {
            if (!_abilities.ContainsKey(typeof(T))) return;
            Debug.Log($"Unregistering ability {typeof(T).Name}");
            _abilities.Remove(typeof(T));
        }

        public void ExecuteCommand(Command command)
        {
            var abilities = 
                _abilities.Values
                    .Where(ability => command.HandledTypes.Contains(ability.GetType()))
                    .ToList();
            if (command.ExecuteType == ExecuteType.First)
                command.Execute(abilities[0]);
            if (command.ExecuteType == ExecuteType.Last)
                command.Execute(abilities[^1]);
            if (command.ExecuteType == ExecuteType.All)
                abilities.ForEach(command.Execute);
        }
    }
}