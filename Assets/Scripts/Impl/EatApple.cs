using GamePlay.UnitData;
using System.Collections;
using UnityEngine;

namespace GamePlay
{
    public class EatApple : IThinkRule
    {
        public int priority => 2;

        public bool CanUse(UnitBase unit)
        {
            var transform = unit.GetUnitData<TransformData>();
            var findAppleData = unit.GetUnitData<FindAppleData>();
            if (findAppleData == null)
                return false;
            var firstElement = findAppleData.value.target;
            var targetTransform = firstElement.GetUnitData<TransformData>();
            return Vector3.Distance(transform.value.position, targetTransform.value.position) < 0.1f;
        }

        public void Execute(UnitBase unit)
        {
            var findAppleData = unit.GetUnitData<FindAppleData>();
            var target = findAppleData.value.target;
            target.AddUnitData(new KillData());
            unit.RemoveUnitData<FindAppleData>();
        }
    }

    public class EatAppleConfigurator : IBehaviorConfigurator
    {
        public void ConfigureBehavior(BrainBase brain, UnitBase unit)
        {
            if (!unit.HasUnitData<MovementData>())
                return;
            brain.AddThinkRule(new EatApple());
        }
    }
}