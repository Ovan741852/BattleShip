using System;
using System.Collections.Generic;
using GamePlay.UnitData;

namespace GamePlay
{
    public class PerceptionSystem : SystemBase
    {
        public PerceptionSystem()
        {
            _selections = new List<UnitBase>();
            _containers = new Dictionary<int, List<UnitBase>>();
        }

        private List<UnitBase> _selections;
        private Dictionary<int, List<UnitBase>> _containers;

        protected override Type[] GetRequiredDataTypes()
        {
            return new Type[]
            {
                typeof(TeamData)
            };
        }

        protected override void OnUpdateUnit(float deltaTime, UnitBase unit)
        {
            unit.RemoveUnitData<PerceptionData>();
            var selfTeam = unit.GetData<TeamData>();
            var selfTeamId = selfTeam.id;
            unitDict.GetUnits(target => {
                var otherTeam = target.GetUnitData<TeamData>();
                return otherTeam != null && otherTeam.value.id != selfTeamId;
            }, ref _selections);
            if (_selections.Count == 0)
                return;

            if(!_containers.TryGetValue(unit.UnitId, out var targets))
            {
                targets = new List<UnitBase>();
                _containers.Add(unit.UnitId, targets);
            }
            targets.Clear();
            targets.AddRange(_selections);
            unit.AddUnitData(new PerceptionData()
            {
                targets = targets
            });
        }

        public override void OnUnitRemoved(UnitBase unit)
        {
            _containers.Remove(unit.UnitId);
        }
    }
}