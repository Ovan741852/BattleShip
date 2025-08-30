using GamePlay.UnitData;
using System.Collections;
using UnityEngine;

namespace GamePlay
{
    public class MoveToApple : IThinkRule
    {
        public int priority => 1;

        public bool CanUse(UnitBase unit)
        {
            var found = unit.GetUnitData<FindAppleData>();
            return found != null;
        }

        public void Execute(UnitBase unit)
        {
            var found = unit.GetUnitData<FindAppleData>();
            var foundTransform = found.value.target.GetUnitData<TransformData>();
            unit.AddUnitData(new DestinationData()
            {
                position = foundTransform.value.position,
            });
            unit.RemoveUnitData<FindAppleData>();
        }
    }

    public class MoveToAppleConfigurator : IBehaviorConfigurator
    {
        public void ConfigureBehavior(BrainBase brain, UnitBase unit)
        {
            if (!unit.HasUnitData<MovementData>())
                return;
            brain.AddThinkRule(new MoveToApple());
        }
    }
}