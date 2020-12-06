namespace SomeAimGame.SFX {
    [System.Serializable]
    public class SFXSettingsDataSerial {
        public SFXType targetSoundClip;
        public SFXType targetMissSoundClip;
        public bool targetSoundOn;
        public bool targetMissSoundOn;
        public bool uiSoundOn;

        public SFXSettingsDataSerial() {
            targetSoundClip     = SFXSettings.targetSoundClip;
            targetMissSoundClip = SFXSettings.targetMissSoundClip;
            targetSoundOn       = SFXSettings.targetSoundOn;
            targetMissSoundOn   = SFXSettings.targetMissSoundOn;
            uiSoundOn           = SFXSettings.uiSoundOn;
        }
    }
}
