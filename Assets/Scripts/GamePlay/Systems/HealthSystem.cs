using System;
using GamePlay.UnitData;

namespace GamePlay
{
    public class HealthSystem : SystemBase
    {
        protected override Type[] GetRequiredDataTypes()
        {
            return new Type[] {
                typeof(HealthData),
            };
        }

        protected override void OnUpdateUnit(float deltaTime, UnitBase unit)
        {
            var health = unit.GetData<HealthData>();
            if (health.health > 0)
                return;
            RemoveUnit(unit);
        }
    }
}