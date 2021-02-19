using UnityEngine;
using UnityEngine.Video;

public class PreviewManager : MonoBehaviour {
    public VideoClip[] allVideoClips;
    public static VideoClip[] gamemodeVideoClips = new VideoClip[8];
    VideoClip[] previewsObject                   = new VideoClip[8];

    public static PreviewManager previewManager;
    private void Awake() { previewManager = this; }

    /// <summary>
    /// Populates all gamemode preview gameobjects with videoclips and plays.
    /// </summary>
    /// <param name="currentGamemode"></param>
    /// <param name="targetColor"></param>
    /// <param name="skybox"></param>
    public static VideoClip[] PopulateGamemodePreviews(string currentGamemode, string targetColor, string skybox) {
        string useTargetColor = targetColor;
        if (useTargetColor == "TargetColor-Custom") { useTargetColor = "TargetColor-White"; }

        previewManager.previewsObject[0] = LoopVideoClips("Gamemode-Scatter", targetColor, "Skybox-Slate");
        previewManager.previewsObject[1] = LoopVideoClips("Gamemode-Flick", targetColor, "Skybox-Slate");
        previewManager.previewsObject[2] = LoopVideoClips("Gamemode-Grid", targetColor, "Skybox-Slate");
        previewManager.previewsObject[3] = LoopVideoClips("Gamemode-Grid2", targetColor, "Skybox-Slate");
        previewManager.previewsObject[4] = LoopVideoClips("Gamemode-Pairs", targetColor, "Skybox-Slate");
        previewManager.previewsObject[5] = LoopVideoClips("Gamemode-Follow", targetColor, "Skybox-Slate");
        previewManager.previewsObject[6] = LoopVideoClips("Gamemode-Follow", targetColor, "Skybox-Slate"); // TODO: Create glob gamemode clip when ready
        previewManager.previewsObject[7] = LoopVideoClips("Gamemode-Follow", targetColor, "Skybox-Slate");

        return previewManager.previewsObject;
    }

    /// <summary>
    /// Returns individual VideoClip for supplied (gamemode) (targetColor) (skybox).
    /// </summary>
    /// <param name="gamemode"></param>
    /// <param name="targetColor"></param>
    /// <param name="skybox"></param>
    /// <returns></returns>
    public static VideoClip PopulateIndividualClip(string gamemode, string targetColor, string skybox) {
        return LoopVideoClips(gamemode, targetColor, skybox);
    }

    /// <summary>
    /// Loops all VideoClips for gamemode previews and returns VideoClip if its the right name.
    /// </summary>
    /// <param name="gamemode"></param>
    /// <param name="targetColor"></param>
    /// <param name="skybox"></param>
    /// <returns></returns>
    public static VideoClip LoopVideoClips(string gamemode, string targetColor, string skybox) {
        string clipString = GetClipString(gamemode, targetColor, skybox);

        foreach (VideoClip clip in previewManager.allVideoClips) {
            if (clip.name == clipString) { return clip; }
        }

        return null;
    }

    /// <summary>
    /// Returns formatted string for video clip using gamemode, target color and skybox. "scatter_red_pink"
    /// </summary>
    /// <param name="gamemode"></param>
    /// <param name="targetColor"></param>
    /// <param name="skybox"></param>
    /// <returns></returns>
    public static string GetClipString(string gamemode, string targetColor, string skybox) {
        return $"{GetItemShorthand(gamemode)}_{GetItemShorthand(targetColor)}_{GetItemShorthand(skybox)}";
    }

    /// <summary>
    /// Returns shorthand strings for items. Like "Gamemode-Scatter" = "scatter".
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public static string GetItemShorthand(string item) {
        return item.Split('-')[1].ToLower();
    }
}
