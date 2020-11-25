using UnityEngine;

public class QuitGame : MonoBehaviour {
    public GameObject parentContainerObject, confirmationContainer, quitButton, cancelButton;
    public static bool gameQuitConfirmationOpen = false;
    public static bool gameQuitButtonOpen       = false;

    private static QuitGame quit;
    private void Awake() { quit = this; }

    //private void Start() {
    //    CloseQuitConfirmation();
    //}

    /// <summary>
    /// Quits current game.
    /// </summary>
    public void QuitCurrentGame() {
        //Debug.Log($"QUIT GAME PRESSED.");
        Application.Quit();
    }

    /// <summary>
    /// Opens game quit confirmation if not open, otherwise quits game.
    /// </summary>
    public void ConfirmGameQuit() {
        if (!gameQuitConfirmationOpen) {
            OpenQuitConfirmation();
        } else {
            CloseQuitConfirmation();
        }
    }

    /// <summary>
    /// Shows quit button.
    /// </summary>
    public static void OpenQuitButton() {
        //Debug.Log("OPEN QUIT");
        quit.quitButton.SetActive(true);
        gameQuitButtonOpen = true;
        //Util.RefreshRootLayoutGroup(quit.parentContainerObject);
        //Debug.Log("OPEN QUIT AFTER");
    }

    /// <summary>
    /// Hides quit button.
    /// </summary>
    public static void CloseQuitButton() {
        //Debug.Log("CLOSE QUIT");
        quit.quitButton.SetActive(false);
        gameQuitButtonOpen = false;
        Util.RefreshRootLayoutGroup(quit.parentContainerObject);
    }

    /// <summary>
    /// Opens quit confirmation section with "Are you sure?" and "Cancel" buttons.
    /// </summary>
    public static void OpenQuitConfirmation() {
        quit.confirmationContainer.SetActive(true);
        quit.quitButton.SetActive(true);
        quit.cancelButton.SetActive(true);
        gameQuitConfirmationOpen = true;
        gameQuitButtonOpen       = true;
        Util.RefreshRootLayoutGroup(quit.parentContainerObject);
    }

    /// <summary>
    /// Closes quit confirmation section.
    /// </summary>
    public static void CloseQuitConfirmation() {
        quit.confirmationContainer.SetActive(false);
        quit.quitButton.SetActive(false);
        quit.cancelButton.SetActive(false);

        gameQuitConfirmationOpen                          = false;
        gameQuitButtonOpen                                = false;
        ButtonHoverHandler_EventTrigger.optionsObjectOpen = false;

        Util.RefreshRootLayoutGroup(quit.parentContainerObject);
    }

    /// <summary>
    /// Cancels quit game if quit confirmation section open.
    /// </summary>
    public void CancelQuitGame() {
       if (gameQuitConfirmationOpen) { CloseQuitConfirmation(); }
    }
}
