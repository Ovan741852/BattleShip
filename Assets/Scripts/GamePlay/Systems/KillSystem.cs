using System;
using System.Collections.Generic;
using UnityEngine;
using GamePlay.UnitData;

namespace GamePlay
{
    public class KillSystem : SystemBase
    {
        protected override Type[] GetRequiredDataTypes()
        {
            return new Type[] {
                typeof(KillData),
            };
        }

        protected override void OnUpdateUnit(float deltaTime, UnitBase unit)
        {
            unit.RemoveUnitData<KillData>();
            RemoveUnit(unit);
        }
    }
}