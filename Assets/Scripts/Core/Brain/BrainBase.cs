using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    public interface IThinkRule
    {
        int priority { get; }

        bool CanUse(UnitBase unit);

        void Execute(UnitBase unit);
    }

    public class BrainBase 
    {
        public BrainBase(UnitBase unit)
        {
            _unit = unit;
            _thinkRules = new List<IThinkRule>();
        }

        private UnitBase _unit;
        private List<IThinkRule> _thinkRules;

        public void AddThinkRule(IThinkRule thinkRule)
        {
            _thinkRules.Add(thinkRule);
            _thinkRules.Sort((a, b) => b.priority.CompareTo(a.priority));
        }

        public void Tick()
        {
            for (int i = 0; i < _thinkRules.Count; i++)
            {
                var thinkRule = _thinkRules[i];
                if (!thinkRule.CanUse(_unit))
                    continue;
                thinkRule.Execute(_unit);
                return;
            }
        }
    }
}