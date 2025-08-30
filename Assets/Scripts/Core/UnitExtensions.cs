namespace GamePlay
{
    public static class UnitExtensions
    {
        // 簡化數據獲取
        public static T GetData<T>(this UnitBase unit) where T : struct, IUnitData
        {
            var data = unit.GetUnitData<T>();
            return data?.value ?? default(T);
        }

        // 簡化數據設置
        public static void SetData<T>(this UnitBase unit, T data) where T : struct, IUnitData
        {
            unit.AddUnitData(data);
        }

        // 簡化數據檢查
        public static bool HasData<T>(this UnitBase unit) where T : struct, IUnitData
        {
            return unit.HasUnitData<T>();
        }

        // 簡化數據移除
        public static void RemoveData<T>(this UnitBase unit) where T : struct, IUnitData
        {
            unit.RemoveUnitData<T>();
        }
    }
}