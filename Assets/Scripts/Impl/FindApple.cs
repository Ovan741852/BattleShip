using GamePlay.UnitData;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace GamePlay
{
    public struct FindAppleData : IUnitData
    {
        public UnitBase target;
    }

    public class FindApple : IThinkRule
    {
        public int priority => 0;

        public bool CanUse(UnitBase unit)
        {
            return unit.HasUnitData<PerceptionData>();
        }

        public void Execute(UnitBase unit)
        {
            var perception = unit.GetData<PerceptionData>();
            var self = unit.GetData<TransformData>();

            // 簡化後的邏輯
            var nearest = perception.targets
                .OrderBy(t => Vector3.Distance(self.position, t.GetData<TransformData>().position))
                .FirstOrDefault();

            unit.SetData(new FindAppleData { target = nearest });
        }
    }

    public class FindAppleConfigurator : IBehaviorConfigurator
    {
        public void ConfigureBehavior(BrainBase brain, UnitBase unit)
        {
            if (!unit.HasUnitData<MovementData>())
                return;
            brain.AddThinkRule(new FindApple());
        }
    }
}