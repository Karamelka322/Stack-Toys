namespace CodeBase.Logic.General.Services.Analytics
{
    public struct AnalyticsParameterData
    {
        public readonly string Name;
        public readonly int Value;

        public AnalyticsParameterData(string name, int value)
        {
            Name = name;
            Value = value;
        }
    }
}