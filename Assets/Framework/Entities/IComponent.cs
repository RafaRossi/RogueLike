using System;
using System.Collections.Generic;
using Framework.Abilities;
using Framework.Behaviours.Animations;
using Framework.Player;
using Framework.Stats;
using UnityEngine;

namespace Framework.Entities
{
    public interface IComponent
    {
        ComponentController ComponentController { get; set; }
    }

    public abstract class ComponentController : MonoBehaviour
    {
        [field:SerializeField] public CharacterData CharacterData { get; private set; }

        private readonly Dictionary<Type, IComponent> _entities = new Dictionary<Type, IComponent>();

        public T GetEntityOfType<T>() where T : IComponent
        {
            return TryGetEntityOfType(out T component) ? component : default;
        }

        public bool TryGetEntityOfType<T>(out T component) where T : IComponent
        {
            if(_entities.TryGetValue(typeof(T), out var _component) && _component is T typeComponent)
            {
                component = typeComponent;
                return true;
            }

            component = default;
            return false;
        }

        public void AddEntityOfType(Type type, IComponent component)
        {
            _entities.Add(type, component);
        }

        public bool TryRemoveEntityOfType(Type type)
        {
            return _entities.Remove(type);
        }
    }
}