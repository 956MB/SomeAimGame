using UnityEngine;

namespace SomeAimGame.Utilities {
    public class GameTime : MonoBehaviour {
        /// <summary>
        /// Changes game timeScale to 0 (Pause).
        /// </summary>
        public static void PauseGameTime() { Time.timeScale = 0; }
        /// <summary>
        /// Changes game timeScale to 1 (Play).
        /// </summary>
        public static void ContinueGameTime() { Time.timeScale = 1; }
    }
}