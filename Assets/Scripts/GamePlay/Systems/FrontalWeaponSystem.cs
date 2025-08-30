using System;
using GamePlay.UnitData;

namespace GamePlay
{
    public class FrontalWeaponSystem : SystemBase
    {
        protected override Type[] GetRequiredDataTypes()
        {
            return new Type[] {
                typeof(HealthData),
            };
        }

        protected override void OnUpdateUnit(float deltaTime, UnitBase unit)
        {
        }
    }
}