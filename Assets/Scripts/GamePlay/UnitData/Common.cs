using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.UnitData
{
    public struct ViewData : IUnitData
    {
        public Transform transform;
    }

    public struct KillData : IUnitData
    {

    }

    public struct TransformData : IUnitData
    {
        public Vector3 position;

        public float rotation;
    }

    public struct MovementData : IUnitData
    {
        public float speed;
    }

    public struct DestinationData : IUnitData
    {
        public Vector3 position;
    }

    public struct PerceptionData : IUnitData
    {
        public List<UnitBase> targets;
    }
}