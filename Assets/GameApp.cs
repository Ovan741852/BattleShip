using GamePlay;
using GamePlay.UnitData;
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

        unit.AddUnitData(new MovementData() { speed = 10 });
        unit.AddUnitData(new DestinationData() { position = Vector3.left * 10 });
        unit.AddUnitData(new TransformData() { position = Vector3.zero, rotation = 0 });
        unit.AddUnitData(new ViewData() { transform = GameObject.CreatePrimitive(PrimitiveType.Capsule).transform });
        _unitDict.AddUnit(unit);
    }

    void Update()
    {
        _systemManager.Update(Time.deltaTime);
    }
}
