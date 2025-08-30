using GamePlay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePlay.UnitData;

public class GameApp : MonoBehaviour
{
    private UnitDict _unitDict;
    private SystemManager _systemManager;
    private BrainManager _brainManager;

    void Start()
    {
        _unitDict = new UnitDict();
        _systemManager = new SystemManager(_unitDict);

        // 設置系統
        _systemManager.AddSystem<ViewSystem>();
        _systemManager.AddSystem<MoveSystem>();
        _systemManager.AddSystem<PerceptionSystem>();
        _systemManager.AddSystem<KillSystem>();

        var brainFactory = new BrainFactory();
        brainFactory.AddBehaviorConfigurator(new AppleBehaviorConfigurator());
        _brainManager = new BrainManager(brainFactory);

        CreateUnits();
    }

    private void CreateUnits()
    {
        var unit = new UnitBase();
        unit.AddUnitData(new MovementData() { speed = 10 });
        unit.AddUnitData(new TransformData() { position = Vector3.zero, rotation = 0 });
        unit.AddUnitData(new ViewData() { transform = GameObject.CreatePrimitive(PrimitiveType.Capsule).transform });
        _unitDict.AddUnit(unit);
        _brainManager.AddBrain(unit);  // 統一添加 Brain

        for (int i = 0; i < 10; i++)
        {
            var apple = new UnitBase();
            var position = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
            apple.AddUnitData(new TransformData() { position = position, rotation = 0 });
            apple.AddUnitData(new ViewData() { transform = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform });
            _unitDict.AddUnit(apple);
        }
    }

    void Update()
    {
        _brainManager.Tick();
        _systemManager.Update(Time.deltaTime);
    }
}
