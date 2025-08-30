namespace GamePlay
{
    public static class UnitExtensions
    {
        public static T GetData<T>(this UnitBase unit) where T : struct, IUnitData
        {
            var data = unit.GetUnitData<T>();
            return data?.value ?? default(T);
        }
    }
}