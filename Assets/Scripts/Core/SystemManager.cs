using System.Collections.Generic;

namespace GamePlay
{
    public class SystemManager 
    {
        public SystemManager(UnitDict unitDict)
        {
            _unitDict = unitDict;
            _removedUnits = new HashSet<UnitBase>();
            _systems = new List<SystemBase>();
            _unitDict.onUnitRemoved = OnUnitRemoved;
        }

        private UnitDict _unitDict;
        private HashSet<UnitBase> _removedUnits;
        private List<SystemBase> _systems;

        public void AddSystem<T>() where T : SystemBase, new()
        {
            var instance = new T();
            instance.Init(_unitDict);
            _systems.Add(instance);
        }

        public void Update(float deltaTime)
        {
            foreach(var removeUnit in _removedUnits)
            {
                for (int i = 0; i < _systems.Count; i++)
                {
                    _systems[i].OnUnitRemoved(removeUnit);
                }
            }
            _removedUnits.Clear();

            for (int i = 0; i < _systems.Count; i++)
            {
                _systems[i].Update(deltaTime);
            }
        }

        private void OnUnitRemoved(UnitBase unit)
        {
            _removedUnits.Add(unit);
        }
    }
}