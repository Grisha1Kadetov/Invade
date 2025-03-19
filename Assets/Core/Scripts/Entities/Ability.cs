using UnityEngine;

namespace Core.Scripts.Entities
{
    [RequireComponent(typeof(Entity))]
    public abstract class Ability : MonoBehaviour
    {
        protected Entity OwnedEntity;
        protected virtual void Awake()
        {
            OwnedEntity = GetComponent<Entity>();
        }
        
        public abstract void Execute(); 
    }
}