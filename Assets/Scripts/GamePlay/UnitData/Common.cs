using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.UnitData
{
    public struct TeamData : IUnitData
    {
        public int id;
    }

    public struct ViewData : IUnitData
    {
        public Transform transform;
    }

    public struct HealthData : IUnitData {
        public int health;

        public int maxHealth;
    }

    public struct TransformData : IUnitData
    {
        public Vector3 position;

        public float rotation;
    }

    public struct MovementData : IUnitData
    {
        public float speed;

        public float maxSpeed;

        public float acceleration;

        public float angularSpeed;
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