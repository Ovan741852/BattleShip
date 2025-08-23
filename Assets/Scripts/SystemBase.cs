using System;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    public abstract class SystemBase
    {
        protected SystemBase()
        {
            OnCreated();
            _selections = new List<UnitBase>();
        }

        private UnitDict _unitDict;
        private List<UnitBase> _selections;

        public void Init(UnitDict unitDict)
        {
            _unitDict = unitDict;
        }

        protected virtual void OnCreated() { }

        protected abstract void OnUpdate(float deltaTime, IReadOnlyList<UnitBase> selections);

        protected abstract bool Picker(UnitBase unit);

        public void Update(float deltaTime)
        {
            _unitDict.GetUnits(Picker, ref _selections);
            OnUpdate(deltaTime, _selections);
        }
    }
}