using GamePlay;
using GamePlay.UnitData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameApp : MonoBehaviour
{
    private UnitDict _unitDict;
    private SystemManager _systemManager;
    private List<BrainBase> _brains;
    private float _elapsed;

    void Start()
    {
        _unitDict = new UnitDict();
        _systemManager = new SystemManager(_unitDict);
        _systemManager.AddSystem<ViewSystem>();
        _systemManager.AddSystem<MoveSystem>();
        _systemManager.AddSystem<PerceptionSystem>();
        _systemManager.AddSystem<KillSystem>();

        var unit = new UnitBase();
        unit.AddUnitData(new MovementData() { speed = 10 });
        unit.AddUnitData(new DestinationData() { position = Vector3.left * 10 });
        unit.AddUnitData(new TransformData() { position = Vector3.zero, rotation = 0 });
        unit.AddUnitData(new ViewData() { transform = GameObject.CreatePrimitive(PrimitiveType.Capsule).transform });
        _unitDict.AddUnit(unit);

        for (int i = 0; i < 10; i++)
        {
            var apple = new UnitBase();
            var position = new Vector3()
            {
                x = Random.Range(-5, 5),
                z = Random.Range(-5, 5),
            };
            apple.AddUnitData(new TransformData() { position = position, rotation = 0 });
            apple.AddUnitData(new ViewData() { transform = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform });
            _unitDict.AddUnit(apple);
        }

        var brainFactory = new BrainFactory();
        brainFactory.AddBehaviorConfigurator(new AppleBehaviorConfigurator());

        _brains = new List<BrainBase>();
        _brains.Add(brainFactory.CreateBrain(unit));
    }

    void Update()
    {
        foreach (var brain in _brains)
            brain.Tick();
        _systemManager.Update(Time.deltaTime);
        _elapsed += Time.deltaTime;
        while(_elapsed > 1)
        {
            _elapsed -= 1;
       
        }
    }
}
