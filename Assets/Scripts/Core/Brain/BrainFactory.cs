using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    public interface IBehaviorConfigurator
    {
        void ConfigureBehavior(BrainBase brain, UnitBase unit);
    }

    public class BrainFactory 
    {
        public BrainFactory()
        {
            _configurators = new List<IBehaviorConfigurator>();
        }

        private List<IBehaviorConfigurator> _configurators;

        public void AddBehaviorConfigurator(IBehaviorConfigurator configurator)
        {
            _configurators.Add(configurator);
        }

        public BrainBase CreateBrain(UnitBase unit)
        {
            var brain = new BrainBase(unit);
            foreach(var configurator in _configurators)
            {
                configurator.ConfigureBehavior(brain, unit);
            }
            return brain;
        }
    }
}