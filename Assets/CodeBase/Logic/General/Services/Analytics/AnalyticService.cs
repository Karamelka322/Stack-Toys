using Firebase.Analytics;

namespace CodeBase.Logic.General.Services.Analytics
{
    public class AnalyticService : IAnalyticService
    {
        public void RegisterEvent(string eventName)
        {
            FirebaseAnalytics.LogEvent(eventName);
        }
        
        public void RegisterEvent(string eventName, string parameterName, int parameterValue)
        {
            FirebaseAnalytics.LogEvent(eventName, parameterName, parameterValue);
        }
        
        public void RegisterEvent(string eventName, params AnalyticsParameterData[] parameters)
        {
            var array = new Parameter[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                array[i] = new Parameter(parameters[i].Name, parameters[i].Value);
            }
            
            FirebaseAnalytics.LogEvent(eventName, array);
        }
    }
}