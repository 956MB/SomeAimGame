using UnityEngine;

namespace SomeAimGame.SFX {
    public class SFXUtil : MonoBehaviour {
        /// <summary>
        /// Returns appropriate target sound string name from supplied SFXType (sfxType).
        /// </summary>
        /// <param name="sfxType"></param>
        /// <returns></returns>
        public static string ReturnTargetSoundStrings(SFXType sfxType) {
            switch (sfxType) {
                case SFXType.TARGET_HIT_SFX_0:  return "00";
                case SFXType.TARGET_HIT_SFX_1:  return "01";
                case SFXType.TARGET_HIT_SFX_2:  return "02";
                case SFXType.TARGET_MISS_SFX_0: return "00";
                case SFXType.TARGET_MISS_SFX_1: return "01";
                case SFXType.TARGET_MISS_SFX_2: return "02";
                default:                        return "00";
            }
        }
    }
}
