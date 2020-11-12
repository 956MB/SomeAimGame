using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour {
    public VideoClip[] allVideoClips;
    public static VideoClip[] gamemodeVideoClips = new VideoClip[7];
    VideoClip[] previewsObject                   = new VideoClip[7];

    public static VideoManager videoManager;
    private void Awake() { videoManager = this; }

    /// <summary>
    /// Populates all gamemode preview gameobjects with videoclips and plays.
    /// </summary>
    /// <param name="currentGamemode"></param>
    /// <param name="targetColor"></param>
    /// <param name="skybox"></param>
    public static VideoClip[] PopulateGamemodePreviews(string currentGamemode, string targetColor, string skybox) {
        videoManager.previewsObject[0] = LoopVideoClips("Gamemode-Scatter", targetColor, skybox);
        videoManager.previewsObject[1] = LoopVideoClips("Gamemode-Flick", targetColor, skybox);
        videoManager.previewsObject[2] = LoopVideoClips("Gamemode-Grid", targetColor, skybox);
        videoManager.previewsObject[3] = LoopVideoClips("Gamemode-Grid2", targetColor, skybox);
        videoManager.previewsObject[4] = LoopVideoClips("Gamemode-Pairs", targetColor, skybox);
        videoManager.previewsObject[5] = LoopVideoClips("Gamemode-Follow", targetColor, skybox);
        videoManager.previewsObject[6] = LoopVideoClips(currentGamemode, targetColor, skybox);

        return videoManager.previewsObject;
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

        foreach (VideoClip clip in videoManager.allVideoClips) {
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
