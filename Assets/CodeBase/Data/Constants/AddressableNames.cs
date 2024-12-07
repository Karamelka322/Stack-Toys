namespace CodeBase.Data.Constants
{
    public static class AddressableNames
    {
        // Prefabs
        public const string Canvas = "Canvas";
        public const string ToyBabble = "Toy_Babble";
        public const string ToyShadow = "Toy_Shadow"; 
        public const string PauseWindow = "Pause_Window";
        
        // Effects
        public const string ToyTowerBuildEffect = "Toy_Tower_Build_Effect";
        
        // ScriptableObjects
        public const string LevelsConfig = "LevelsConfig";
        public const string CameraSettings = "Camera_Settings";
        
        // Materials
        public const string HighlightedToyMaterial = "Highlighted_Toys_Material";
        
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
            // Prefabs
            public const string FinishLine = "FinishLine";
            
            // UI - Elements
            public const string ToyRotator = "ToyRotator";
            public const string MainWindow = "CompanyMainWindow";
            public const string FinishWindow = "CompanyFinishWindow";

            // Effects
            public const string ToySelectEffect = "ToySelectEffect";
            public const string FinishEffect = "Finish_effect";
        }
    }
}