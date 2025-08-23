using System.Collections.Generic;

namespace GamePlay
{
    public class SystemManager 
    {
        public SystemManager(UnitDict unitDict)
        {
            _unitDict = unitDict;
            _systems = new List<SystemBase>();
        }

        private UnitDict _unitDict;
        private List<SystemBase> _systems;

        public void AddSystem<T>() where T : SystemBase, new()
        {
            var instance = new T();
            instance.Init(_unitDict);
            _systems.Add(instance);
        }

        public void Update(float deltaTime)
        {
            for (int i = 0; i < _systems.Count; i++)
            {
                _systems[i].Update(deltaTime);
            }
        }
    }
}