using GamePlay.UnitData;
using System.Collections;
using UnityEngine;

namespace GamePlay
{
    public class FindApple : IThinkRule
    {
        public int priority => 0;

        public bool CanUse(UnitBase unit)
        {
            return true;
        }

        public void Execute(UnitBase unit)
        {

        }
    }

    public class FindAppleConfigurator : IBehaviorConfigurator
    {
        public void ConfigureBehavior(BrainBase brain, UnitBase unit)
        {
            if (unit.HasUnitData<MovementData>())
                return;
            brain.AddThinkRule(new FindApple());
        }
    }
}