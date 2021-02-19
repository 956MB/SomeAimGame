using UnityEngine;

namespace SomeAimGame.Console {
    public class ConsoleSubmitButton : MonoBehaviour {
        /// <summary>
        /// Submit button method for sending console input to be ran.
        /// </summary>
        public void SubmitButtonClick() {
            SAGConsole.SubmitConsoleInput();
        }
    }
}
