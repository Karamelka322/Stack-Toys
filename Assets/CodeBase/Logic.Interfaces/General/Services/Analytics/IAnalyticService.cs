namespace CodeBase.Logic.General.Services.Analytics
{
    public interface IAnalyticService
    {
        void RegisterEvent(string eventName);
        void RegisterEvent(string eventName, string parameterName, int parameterValue);
        void RegisterEvent(string eventName, params AnalyticsParameterData[] parameters);
    }
}