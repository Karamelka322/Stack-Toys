namespace CodeBase.Data.Constants
{
    public static class AddressableNames
    {
        // Prefabs
        public const string Canvas = "Canvas";
        public const string ToyBabble = "Toy_Babble";
        
        // ScriptableObjects
        public const string LevelsConfig = "LevelsConfig";
        public const string CameraSettings = "Camera_Settings";

        public const string PauseWindow = "Pause_Window";
        
        public static class MenuScene
        {
            // UI
            public const string MenuWindow = "Menu_Window";
            public const string LevelsWindow = "Levels_Window";
            public const string CompletedLevelElement = "CompletedLevelElement";
            public const string ClosedLevelElement = "ClosedLevelElement";
            public const string OpenedLevelElement = "OpenedLevelElement";
        }
        
        public static class CompanyScene
        {
            // UI
            public const string ToyRotator = "ToyRotator";
            public const string MainWindow = "CompanyMainWindow";
            public const string FinishWindow = "CompanyFinishWindow";

            // Effects
            public const string ToySelectEffect = "ToySelectEffect";
        }
    }
}