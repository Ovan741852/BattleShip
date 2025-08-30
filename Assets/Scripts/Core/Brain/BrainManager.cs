using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    public class BrainManager
    {
        public BrainManager(BrainFactory brainFactory)
        {
            _brainFactory = brainFactory;
            _brains = new List<BrainBase>();
        }

        private List<BrainBase> _brains;
        private BrainFactory _brainFactory;

        public void AddBrain(UnitBase unit)
        {
            var brain = _brainFactory.CreateBrain(unit);
            _brains.Add(brain);
        }

        public void RemoveBrain(UnitBase unit)
        {
            for (int i = _brains.Count - 1; i >= 0; i--)
            {
                if (_brains[i].unit == unit)
                {
                    _brains.RemoveAt(i);
                    break;
                }
            }
        }

        public void Tick()
        {
            for (int i = 0; i < _brains.Count; i++)
            {
                _brains[i].Tick();
            }
        }

        public void Clear()
        {
            _brains.Clear();
        }
    }
}