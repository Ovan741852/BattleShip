using GamePlay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePlay.UnitData;

public class GameApp : MonoBehaviour
{
    public GameObject ship;
    private UnitDict _unitDict;
    private SystemManager _systemManager;
    private BrainManager _brainManager;

    void Start()
    {
        _unitDict = new UnitDict();
        _systemManager = new SystemManager(_unitDict);

        // �]�m�t��
        _systemManager.AddSystem<ViewSystem>();
        _systemManager.AddSystem<MoveSystem>();
        _systemManager.AddSystem<PerceptionSystem>();
        _systemManager.AddSystem<HealthSystem>();

        var brainFactory = new BrainFactory();
        brainFactory.AddBehaviorConfigurator(new AppleBehaviorConfigurator());
        _brainManager = new BrainManager(brainFactory);

        CreateUnits();
    }

    private void CreateShip(int teamId, Vector3 position, float rotation)
    {
        var unit = new UnitBase();
        unit.AddUnitData(new MovementData()
        {
            speed = 0f,
            maxSpeed = 15f,
            acceleration = 8f,
            angularSpeed = 60f
        });
        unit.AddUnitData(new TransformData() { position = position, rotation = rotation });
        unit.AddUnitData(new ViewData() { transform = Instantiate(ship).transform });
        unit.AddUnitData(new TeamData() { id = teamId });
        unit.AddUnitData(new ColliderData() { radius = 2 });
        _unitDict.AddUnit(unit);
        _brainManager.AddBrain(unit);
    }

    private void CreateUnits()
    {
        for (int i = 0; i < 10; i++)
        {
            var teamId = i % 2 + 1;
            var position = new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20));
            var rotation = Random.Range(0, 360);
            CreateShip(teamId, position, rotation);
        }
    }

    void Update()
    {
        _brainManager.Tick();
        _systemManager.Update(Time.deltaTime);
    }
}
