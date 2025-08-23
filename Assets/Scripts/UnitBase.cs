using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    public interface IUnitComponent
    {

    }

    public class UnitComponent<T>: IUnitComponent where T : struct
    {
        public UnitComponent(T initValue)
        {
            value = initValue;
        }

        public T value { get; set; }
    }

    public class UnitBase
    {
        public UnitBase()
        {
            _components = new Dictionary<Type, IUnitComponent>();
        }

        private Dictionary<Type, IUnitComponent> _components;

        public void AddComponent<T>(T component) where T : IUnitComponent
        {
            var type = typeof(T);
            if (_components.ContainsKey(type))
            {
                Debug.LogWarning("Duplicated Component");
                return;
            }
            _components.Add(type, component);
        }

        public T GetComponent<T>() where T : class, IUnitComponent
        {
            var type = typeof(T);
            _components.TryGetValue(type, out var result);
            return result as T;
        }

        public bool HasComponent<T>() where T : class, IUnitComponent
        {
            var type = typeof(T);
            return _components.ContainsKey(type);
        }

        public void RemoveComponent<T>() where T : IUnitComponent
        {
            var type = typeof(T);
            if (!_components.ContainsKey(type))
                return;
            _components.Remove(type);
        }
    }
}