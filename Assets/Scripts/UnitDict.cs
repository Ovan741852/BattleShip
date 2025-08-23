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

        public void GetUnits(Func<UnitBase, bool> picker, ref List<UnitBase> selections)
        {
            selections.Clear();
            selections.AddRange(_allUnits);
            for (int i = selections.Count - 1; i >= 0; i--)
            {
                if (picker(selections[i]))
                    continue;
                selections.RemoveAt(i);
            }
        }
    }
}