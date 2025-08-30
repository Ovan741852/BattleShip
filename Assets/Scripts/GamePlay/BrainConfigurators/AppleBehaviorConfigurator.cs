using System.Collections;
using System.Linq;
using UnityEngine;
using GamePlay.UnitData;

namespace GamePlay
{
    public struct FindAppleData : IUnitData
    {
        public UnitBase target;
    }

    public class AppleBehaviorConfigurator : IBehaviorConfigurator
    {
        public void ConfigureBehavior(BrainBase brain, UnitBase unit)
        {
            if (!unit.HasUnitData<MovementData>()) return;

            // 找蘋果
            brain.AddThinkRule(new SimpleThinkRule(0,
                u => u.HasUnitData<PerceptionData>(),
                u =>
                {
                    var perception = u.GetData<PerceptionData>();
                    var self = u.GetData<TransformData>();
                    var nearest = perception.targets
                        .OrderBy(t => Vector3.Distance(self.position, t.GetData<TransformData>().position))
                        .FirstOrDefault();
                    u.AddUnitData(new FindAppleData { target = nearest });
                }
            ));

            // 移動到蘋果
            brain.AddThinkRule(new SimpleThinkRule(1,
                u => u.HasUnitData<FindAppleData>(),
                u =>
                {
                    u.RemoveUnitData<DestinationData>();
                    var found = u.GetData<FindAppleData>();
                    var targetPos = found.target.GetData<TransformData>().position;
                    u.AddUnitData(new DestinationData { position = targetPos });
                    u.RemoveUnitData<FindAppleData>();
                }
            ));

            // 吃蘋果
            brain.AddThinkRule(new SimpleThinkRule(2,
                u =>
                {
                    var found = u.GetData<FindAppleData>();
                    if (found.target == null) return false;
                    var self = u.GetData<TransformData>();
                    var target = found.target.GetData<TransformData>();
                    return Vector3.Distance(self.position, target.position) < 0.1f;
                },
                u =>
                {
                    var found = u.GetData<FindAppleData>();
                    found.target.AddUnitData(new KillData());
                    u.RemoveUnitData<FindAppleData>();
                }
            ));
        }
    }
}