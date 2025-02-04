using Cysharp.Threading.Tasks;

namespace CodeBase.Logic.General.Services.Crashlytics
{
    public class CrashlyticsService
    {
        public CrashlyticsService()
        {
            InitializeAsync().Forget();
        }

        private static async UniTask InitializeAsync()
        {
            var task = Firebase.FirebaseApp.CheckAndFixDependenciesAsync().AsUniTask();

            var dependencyStatus = await task;
            
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                // Crashlytics will use the DefaultInstance, as well;
                // this ensures that Crashlytics is initialized.
                Firebase.FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;
            
                // When this property is set to true, Crashlytics will report all
                // uncaught exceptions as fatal events. This is the recommended behavior.
                Firebase.Crashlytics.Crashlytics.ReportUncaughtExceptionsAsFatal = true;
            
                // Set a flag here for indicating that your project is ready to use Firebase.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                    "Could not resolve all Firebase dependencies: {0}",dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        }
    }
}