namespace CodeBase.Data.General.Constants
{
    public static class AddressableConstants
    {
        // Game - Prefabs
        public const string Canvas = "Canvas";
        public const string AudioListener = "AudioListener";
        public const string AudioMixer = "AudioMixer";
        public const string MusicSource = "MusicSource";
        public const string SoundSource = "SoundSource";
        public const string ToyBabble = "Toy_Babble";
        public const string ToyShadow = "Toy_Shadow";
        
        // Game - Effects
        public const string ConfettiEffect = "Finish_effect";
        
        // UI - Windows
        public const string PauseWindow = "Pause_Window";
        
        // ScriptableObjects
        public const string LevelsConfig = "LevelsConfig";
        public const string CameraSettings = "Camera_Settings";
        public const string AudioSettings = "AudioSettings";
        
        // Materials
        public const string HighlightedToyMaterial = "Highlighted_Toys_Material";
        
        // Sprites
        public const string RussianFlag = "Russian_Flag";
        public const string EnglishFlag = "English_Flag";
        
        public static class MenuScene
        {
            // UI - Windows
            public const string MenuWindow = "Menu_Window";
            public const string LevelsWindow = "Levels_Window";
            
            // UI - Elements
            public const string CompletedLevelElement = "CompletedLevelElement";
            public const string ClosedLevelElement = "ClosedLevelElement";
            public const string OpenedLevelElement = "OpenedLevelElement";
        }
        
        public static class CompanyScene
        {
            // Game - Prefabs
            public const string FinishLine = "FinishLine";
            
            // UI - Windows
            public const string MainWindow = "CompanyMainWindow";

            // Effects
            public const string ToySelectEffect = "ToySelectEffect";
        }
        
        public static class InfinityScene
        {
            // Game - Prefabs
            public const string Level =  "InfinityLevel";
            public const string ToyChoicer = "ToyChoicer";
            public const string RecordLine = "RecordLine";
            
            // UI - Windows
            public const string MainWindow = "InfinitySceneMainWindow";
            
            // ScriptableObjects
            public const string ToysSettings = "InfinitySceneToysSettings";
        }
    }
}