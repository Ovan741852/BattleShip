using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    public interface IUnitData
    {

    }

    public interface IUnitDataHandle<T> where T : struct, IUnitData
    {
        T value { get; set; }
    }


    public class UnitBase
    {
        public UnitBase()
        {
            _states = new Dictionary<Type, IState>();
        }

        private Dictionary<Type, IState> _states;

        public void AddUnitData<T>(T data) where T : struct, IUnitData
        {
            var type = typeof(T);
            if(!_states.TryGetValue(type, out var state))
            {
                var instance = new State<T>();
                instance.value = data;
                _states.Add(type, instance);
                state = instance;
            }

            if (state.activated)
            {
                Debug.LogError("duplicated component");
                return;
            }
            state.activated = true;
        }

        public IUnitDataHandle<T> GetUnitData<T>() where T : struct, IUnitData
        {
            var type = typeof(T);
            if (_states.TryGetValue(type, out var state) && state.activated)
                return state as State<T>;
            else
                return null;
        }

        public bool HasUnitData(Type type)
        {
            if (_states.TryGetValue(type, out var state) && state.activated)
                return true;
            return false;
        }

        public bool HasUnitData<T>() where T : struct, IUnitData
        {
            var type = typeof(T);
            return HasUnitData(type);
        }

        public void RemoveUnitData<T>() where T : IUnitData
        {
            var type = typeof(T);
            if (!_states.TryGetValue(type, out var state))
                return;
            state.activated = false;
        }

        interface IState
        {
            public bool activated { get; set; }
        }

        class State<T> : IState, IUnitDataHandle<T> where T : struct, IUnitData
        {
            public State() { }

            public bool activated { get; set; }
            public T value { get; set; }
        }
    }
}