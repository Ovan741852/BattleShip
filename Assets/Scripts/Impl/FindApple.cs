using GamePlay.UnitData;
using System.Collections;
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
            var perceptionData = unit.GetUnitData<PerceptionData>();
            return perceptionData != null;
        }

        public void Execute(UnitBase unit)
        {
            var self = unit.GetUnitData<TransformData>();
            unit.RemoveUnitData<DestinationData>();
            var perceptionData = unit.GetUnitData<PerceptionData>();
            float shortest = float.MaxValue;
            UnitBase nearest = null;
            foreach(var target in perceptionData.value.targets)
            {
                var transformData = target.GetUnitData<TransformData>();
                var targetPos = transformData.value.position;
                var distance = Vector3.Distance(self.value.position, targetPos);
                if (distance > shortest)
                    continue;
                shortest = distance;
                nearest = target;
            }
            unit.AddUnitData(new FindAppleData()
            {
                target = nearest
            });
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