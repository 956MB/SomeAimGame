using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Steamworks;
using TMPro;

public class LoadSteamData : MonoBehaviour {
    public static CSteamID userSteamID;
    public Image steamAvatarImage;
    public Sprite placeholderAvatar;
    public TMP_Text steamUsernameText;
    public GameObject rootGroup;

    private static int avatarInt;
    private static uint avatarWidth, avatarHeight;
    private static Texture2D downloadedAvatarTexture;

    private static LoadSteamData steamData;
    private void Awake() { steamData = this; }

    void Start() {
        SetSteamDataDefaults();

        if (!SteamManager.Initialized) { return; }
        userSteamID = SteamUser.GetSteamID();

        FetchSteamUsername();
        StartCoroutine(FetchSteamAvatar());

        //ButtonHoverHandler_EventTrigger.ToggleOptionsObject_Static();
        //ButtonHoverHandler_EventTrigger.ToggleOptionsObject_Static();
        Util.RefreshRootLayoutGroup(steamData.rootGroup);
    }

    /// <summary>
    /// Retrieves users steam username and sets username text in steam data container.
    /// </summary>
    public static void FetchSteamUsername() {
        //string steamUsername = SteamFriends.GetPersonaName();
        //string steamUsername = "Bloumbs";
        //string steamUsername = "BjornWillDoMatteBlackFjorg";
        string steamUsername = $"{I18nTextTranslator.SetTranslatedText("testuser")}";

        if (steamUsername.Length > 16) { steamData.steamUsernameText.SetText($"{ShortenSteamUsername(steamUsername)}");
        } else { steamData.steamUsernameText.SetText($"{steamUsername}"); }
    }

    public static string ShortenSteamUsername(string username) {
        string usernameResult = "";
        for (int i = 0; i < 16; i++) { usernameResult += username[i]; }

        return $"{usernameResult}...";
    }

    /// <summary>
    /// Fetches users steam avatar image and sets sprite in steam data container (steamAvatarImage).
    /// </summary>
    /// <returns></returns>
    public static IEnumerator FetchSteamAvatar() {
        avatarInt = SteamFriends.GetLargeFriendAvatar(userSteamID);
        while(avatarInt == -1) { yield return null; }

        // Users steam avatar available.
        if (avatarInt > 0) {
            bool avatarExists = SteamUtils.GetImageSize(avatarInt, out avatarWidth, out avatarHeight);

            if (avatarExists && avatarWidth > 0 && avatarHeight > 0) {
                byte[] avatarStream = new byte[4 * (int)avatarWidth * (int)avatarHeight];
                SteamUtils.GetImageRGBA(avatarInt, avatarStream, 4 * (int)avatarWidth * (int)avatarHeight);

                downloadedAvatarTexture = new Texture2D((int)avatarWidth, (int)avatarHeight, TextureFormat.RGBA32, false);
                downloadedAvatarTexture.LoadRawTextureData(avatarStream);
                downloadedAvatarTexture.Apply();

                // Flips upside down steam avatar
                Texture2D flippedAvatar = FlipTexture(downloadedAvatarTexture);
                steamData.steamAvatarImage.sprite = Sprite.Create(flippedAvatar, new Rect(0f, 0f, 184f, 184f), new Vector2(0.5f, 0.5f));
            }
        }
    }

    /// <summary>
    /// Sets default avatar and username text if users steam data not available, or steam manager not initialized.
    /// </summary>
    public static void SetSteamDataDefaults() {
        steamData.steamUsernameText.SetText($"{I18nTextTranslator.SetTranslatedText("steamdataplaceholderusername")}");
        steamData.steamAvatarImage.sprite = steamData.placeholderAvatar;
    }

    /// <summary>
    /// Returns supplied steam avatar texture (originalAvatar) flipped.
    /// </summary>
    /// <param name="originalAvatar"></param>
    /// <returns></returns>
    private static Texture2D FlipTexture(Texture2D originalAvatar) {
        Texture2D flipped = new Texture2D(originalAvatar.width, originalAvatar.height);
        int xN = originalAvatar.width;
        int yN = originalAvatar.height;

        for (int i = 0; i < xN; i++) {
            for (int j = 0; j < yN; j++) {
                flipped.SetPixel(i, yN - j - 1, originalAvatar.GetPixel(i, j));
            }
        }

        flipped.Apply();

        return flipped;
    }
}
