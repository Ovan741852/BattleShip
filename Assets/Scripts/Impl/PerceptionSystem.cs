using GamePlay.UnitData;
using System;
using System.Collections.Generic;

namespace GamePlay
{
    public class PerceptionSystem : SystemBase
    {
        public PerceptionSystem()
        {
            _selections = new List<UnitBase>();
        }

        private List<UnitBase> _selections;

        protected override Type[] GetRequiredDataTypes()
        {
            return new Type[]
            {
                typeof(MovementData)
            };
        }

        protected override void OnUpdateUnit(float deltaTime, UnitBase unit)
        {
            unitDict.GetUnits(target => !target.HasUnitData<MovementData>(), ref _selections);
            unit.RemoveUnitData<DestinationData>();
            unit.AddUnitData(new DestinationData()
            {
                position = _selections[0].GetUnitData<TransformData>().value.position
            });
        }
    }
}