using GamePlay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameApp : MonoBehaviour
{
    private UnitDict _unitDict;
    private SystemManager _systemManager;

    void Start()
    {
        _unitDict = new UnitDict();
        _systemManager = new SystemManager(_unitDict);
        _systemManager.AddSystem<ViewSystem>();
        _systemManager.AddSystem<MoveSystem>();

        var unit = new UnitBase();
        unit.AddComponent(new UnitComponent<MovementData>(new MovementData() { speed = 10 }));
        unit.AddComponent(new UnitComponent<DestinationData>(new DestinationData() { position = Vector3.left * 10 }));
        unit.AddComponent(new UnitComponent<TransformData>(new TransformData() { position = Vector3.zero, rotation = 0 }));
        unit.AddComponent(new UnitComponent<ViewData>(new ViewData() { transform = GameObject.CreatePrimitive(PrimitiveType.Capsule).transform }));
        _unitDict.AddUnit(unit);
    }

    void Update()
    {
        _systemManager.Update(Time.deltaTime);
    }
}
