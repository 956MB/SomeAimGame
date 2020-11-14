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

    public void ConfirmGameQuit() {
        if (!gameQuitConfirmationOpen) {
            OpenQuitConfirmation();
        } else {
            QuitCurrentGame();
        }
    }

    public static void OpenQuitButton() {
        //Debug.Log("OPEN QUIT");
        quit.quitButton.SetActive(true);
        gameQuitButtonOpen = true;
        //Util.RefreshRootLayoutGroup(quit.parentContainerObject);
        //Debug.Log("OPEN QUIT AFTER");
    }
    public static void CloseQuitButton() {
        //Debug.Log("CLOSE QUIT");
        quit.quitButton.SetActive(false);
        gameQuitButtonOpen = false;
        Util.RefreshRootLayoutGroup(quit.parentContainerObject);
    }

    public static void OpenQuitConfirmation() {
        quit.confirmationContainer.SetActive(true);
        quit.quitButton.SetActive(true);
        quit.cancelButton.SetActive(true);
        gameQuitConfirmationOpen = true;
        gameQuitButtonOpen       = true;
        Util.RefreshRootLayoutGroup(quit.parentContainerObject);
    }

    public static void CloseQuitConfirmation() {
        quit.confirmationContainer.SetActive(false);
        quit.quitButton.SetActive(false);
        quit.cancelButton.SetActive(false);

        gameQuitConfirmationOpen                          = false;
        gameQuitButtonOpen                                = false;
        ButtonHoverHandler_EventTrigger.optionsObjectOpen = false;

        Util.RefreshRootLayoutGroup(quit.parentContainerObject);
    }

    public void CancelQuitGame() {
       if (gameQuitConfirmationOpen) { CloseQuitConfirmation(); }
    }
}
