using System;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    public struct TransformData
    {
        public Vector3 position;

        public float rotation;
    }

    public struct MovementData
    {
        public float speed;
    }

    public struct DestinationData
    {
        public Vector3 position;
    }

    public class MoveSystem : SystemBase
    {
        protected override bool Picker(UnitBase unit)
        {
            if (!unit.HasComponent<UnitComponent<TransformData>>())
                return false;
            if (!unit.HasComponent<UnitComponent<MovementData>>())
                return false;
            if (!unit.HasComponent<UnitComponent<DestinationData>>())
                return false;
            return true;
        }

        protected override void OnUpdate(float deltaTime, IReadOnlyList<UnitBase> selections)
        {
            foreach(var selection in selections)
            {
                var transformData = selection.GetComponent<UnitComponent<TransformData>>();
                var movementData = selection.GetComponent<UnitComponent<MovementData>>();
                var destinationData = selection.GetComponent<UnitComponent<DestinationData>>();

                var fromPos = transformData.value.position;
                var toPos = destinationData.value.position;
                var speed = movementData.value.speed;
                var offset = speed * deltaTime;

                var remain = Vector3.Distance(toPos, fromPos);
                if(remain <= offset)
                {
                    var result = transformData.value;
                    result.position = toPos;
                    transformData.value = result;

                    selection.RemoveComponent<UnitComponent<DestinationData>>();
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
}