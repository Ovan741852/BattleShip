using GamePlay.UnitData;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    public struct KillData : IUnitData
    {

    }

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