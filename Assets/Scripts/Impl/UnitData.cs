using System.Collections;
using UnityEngine;

namespace GamePlay.UnitData
{
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

    public struct ViewData : IUnitData
    {
        public Transform transform;
    }

    public struct PerceptionData : IUnitData
    {

    }
}