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
            _requiredDataTypes = GetRequiredDataTypes();
        }

        private UnitDict _unitDict;
        private List<UnitBase> _selections;
        private Type[] _requiredDataTypes;

        protected IUnitDict unitDict => _unitDict;

        public void Init(UnitDict unitDict)
        {
            _unitDict = unitDict;
        }

        protected virtual void OnCreated() { }

        protected abstract void OnUpdateUnit(float deltaTime, UnitBase unit);

        protected abstract Type[] GetRequiredDataTypes();

        public void Update(float deltaTime)
        {
            _unitDict.GetUnits(_requiredDataTypes, ref _selections);
            foreach(var selection in _selections)
            {
                OnUpdateUnit(deltaTime, selection);
            }
        }

        protected void RemoveUnit(UnitBase unit)
        {
            unitDict.RemoveUnit(unit);
        }

        public virtual void OnUnitRemoved(UnitBase unit) { }
    }
}