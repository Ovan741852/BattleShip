using System;
using System.Collections.Generic;
using UnityEngine;
using GamePlay.UnitData;

namespace GamePlay
{
    public class ViewSystem : SystemBase
    {
        protected override Type[] GetRequiredDataTypes()
        {
            return new Type[] {
                typeof(TransformData),
                typeof(ViewData),
            };
        }

        protected override void OnUpdateUnit(float deltaTime, UnitBase unit)
        {
            var transformData = unit.GetUnitData<TransformData>();
            var viewData = unit.GetUnitData<ViewData>();

            var transform = viewData.value.transform;
            transform.position = transformData.value.position;
            transform.rotation = Quaternion.Euler(0, transformData.value.rotation, 0);
        }

        public override void OnUnitRemoved(UnitBase unit)
        {
            base.OnUnitRemoved(unit);
            var viewData = unit.GetUnitData<ViewData>();
            GameObject.Destroy(viewData.value.transform.gameObject);
        }
    }
}