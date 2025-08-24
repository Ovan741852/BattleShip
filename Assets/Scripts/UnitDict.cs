using System;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    public class UnitDict 
    {
        public UnitDict()
        {
            _allUnits = new List<UnitBase>();
        }

        private List<UnitBase> _allUnits;

        public void AddUnit(UnitBase unit)
        {
            _allUnits.Add(unit);
        }

        public void RemoveUnit(UnitBase unit)
        {
            _allUnits.Remove(unit);
        }

        public void GetUnits(Type[] types, ref List<UnitBase> selections)
        {
            bool HasRequireDatas(UnitBase unit)
            {
                foreach(var type in types)
                {
                    if (!unit.HasUnitData(type))
                        return false;
                }
                return true;
            }

            selections.Clear();
            selections.AddRange(_allUnits);
            for (int i = selections.Count - 1; i >= 0; i--)
            {
                var unit = selections[i];
                if (HasRequireDatas(unit))
                    continue;
                selections.RemoveAt(i);
            }
        }
    }
}