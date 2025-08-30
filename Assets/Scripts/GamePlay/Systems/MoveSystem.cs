using System;
using System.Collections.Generic;
using UnityEngine;
using GamePlay.UnitData;

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

            var currentPos = transformData.value.position;
            var currentRotation = transformData.value.rotation;
            var targetPos = destinationData.value.position;
            var currentSpeed = movementData.value.speed;
            var maxSpeed = movementData.value.maxSpeed;
            var acceleration = movementData.value.acceleration;
            var angularSpeed = movementData.value.angularSpeed;

            // 計算到目標的方向和距離
            var directionToTarget = (targetPos - currentPos).normalized;
            var targetRotation = Mathf.Atan2(directionToTarget.x, directionToTarget.z) * Mathf.Rad2Deg;
            var distanceToTarget = Vector3.Distance(currentPos, targetPos);

            // 檢查是否到達目標
            if (distanceToTarget < 0.1f)
            {
                unit.RemoveUnitData<DestinationData>();
                return;
            }

            // 1. 轉向邏輯：船隻會持續轉向目標方向
            var angleDifference = Mathf.DeltaAngle(currentRotation, targetRotation);
            var newRotation = currentRotation;

            if (Mathf.Abs(angleDifference) > 0.1f)
            {
                var rotationStep = angularSpeed * deltaTime;
                var rotationDirection = Mathf.Sign(angleDifference);
                
                if (Mathf.Abs(angleDifference) <= rotationStep)
                {
                    newRotation = targetRotation;
                }
                else
                {
                    newRotation = currentRotation + rotationDirection * rotationStep;
                }
            }

            // 2. 速度控制邏輯：根據距離和角度調整目標速度
            var targetSpeed = CalculateTargetSpeed(distanceToTarget, angleDifference, maxSpeed);
            var newSpeed = CalculateNewSpeed(currentSpeed, targetSpeed, acceleration, deltaTime);

            // 3. 移動邏輯：船隻會以當前速度沿著船頭方向移動
            var forwardDirection = new Vector3(Mathf.Sin(newRotation * Mathf.Deg2Rad), 0, Mathf.Cos(newRotation * Mathf.Deg2Rad));
            var newPosition = currentPos + forwardDirection * newSpeed * deltaTime;

            // 更新數據
            var transformResult = transformData.value;
            transformResult.position = newPosition;
            transformResult.rotation = newRotation;
            transformData.value = transformResult;

            var movementResult = movementData.value;
            movementResult.speed = newSpeed;
            movementData.value = movementResult;
        }

        private float CalculateTargetSpeed(float distance, float angleDifference, float maxSpeed)
        {
            var distanceFactor = Mathf.Clamp01(distance / 20f); // 3米內開始減速
            var angleFactor = Mathf.Clamp01(1f - Mathf.Abs(angleDifference) / 180f * 0.3f); // 最多只減速20%
            var minSpeed = maxSpeed * 0.2f; 
            var targetSpeed = maxSpeed * distanceFactor * angleFactor;
            
            return Mathf.Max(targetSpeed, minSpeed);
        }

        private float CalculateNewSpeed(float currentSpeed, float targetSpeed, float acceleration, float deltaTime)
        {
            // 使用加速度平滑調整速度
            if (currentSpeed < targetSpeed)
            {
                return Mathf.Min(currentSpeed + acceleration * deltaTime, targetSpeed);
            }
            else if (currentSpeed > targetSpeed)
            {
                return Mathf.Max(currentSpeed - acceleration * deltaTime, targetSpeed);
            }
            return currentSpeed;
        }
    }
}