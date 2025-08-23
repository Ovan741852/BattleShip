using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    public struct ViewData
    {
        public Transform transform;
    }

    public class ViewSystem : SystemBase
    {
        protected override bool Picker(UnitBase unit)
        {
            if (!unit.HasComponent<UnitComponent<TransformData>>())
                return false;
            if (!unit.HasComponent<UnitComponent<ViewData>>())
                return false;
            return true;
        }

        protected override void OnUpdate(float deltaTime, IReadOnlyList<UnitBase> selections)
        {
            foreach(var selection in selections)
            {
                var transformData = selection.GetComponent<UnitComponent<TransformData>>();
                var viewData = selection.GetComponent<UnitComponent<ViewData>>();
                viewData.value.transform.position = transformData.value.position;
            }
        }
    }
}