using GamePlay.UnitData;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    public class MoveSystem : SystemBase
    {
        protected override Type[] GetRequiredDataTypes()
        {
            return new Type[] {
                typeof(TransformData),
                typeof(MovementData),
                typeof(DestinationData),
            };
        }

        protected override void OnUpdateUnit(float deltaTime, UnitBase unit)
        {
            var transformData = unit.GetUnitData<TransformData>();
            var movementData = unit.GetUnitData<MovementData>();
            var destinationData = unit.GetUnitData<DestinationData>();

            var fromPos = transformData.value.position;
            var toPos = destinationData.value.position;
            var speed = movementData.value.speed;
            var offset = speed * deltaTime;

            var remain = Vector3.Distance(toPos, fromPos);
            if (remain <= offset)
            {
                var result = transformData.value;
                result.position = toPos;
                transformData.value = result;
                unit.RemoveUnitData<DestinationData>();
            }
            else
            {
                var direction = (toPos - fromPos).normalized;
                var newPos = fromPos + direction * offset;

                var result = transformData.value;
                result.position = newPos;
                transformData.value = result;
            }
        }
    }
}