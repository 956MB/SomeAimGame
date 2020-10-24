using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Steamworks;
using TMPro;

public class LoadSteamData : MonoBehaviour {
    public static CSteamID userSteamID;
    public Image steamAvatarImage;
    public Sprite placeholderAvatar;
    public TMP_Text steamUsernameText;

    private static int avatarInt;
    private static uint avatarWidth, avatarHeight;
    private static Texture2D downloadedAvatarTexture;
    private static Rect avatarRect = new Rect(0f, 0f, 184f, 184f);
    private static Vector2 avatarPivot = new Vector2(0.5f, 0.5f);

    private static LoadSteamData steamData;
    private void Awake() { steamData = this; }

    void Start() {
        SetSteamDataDefaults();
        if (!SteamManager.Initialized) { return; }
        userSteamID = SteamUser.GetSteamID();

        //Debug.Log($"SteamManager LOADED! ID: {userSteamID}");

        LoadSteamUsername();
        StartCoroutine(LoadSteamAvatar());
    }

    public static void LoadSteamUsername() {
        string steamUsername = SteamFriends.GetPersonaName();
        steamData.steamUsernameText.SetText($"{steamUsername}");
    }

    public static IEnumerator LoadSteamAvatar() {
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

                Texture2D flippedAvatar = FlipTexture(downloadedAvatarTexture);
                steamData.steamAvatarImage.sprite = Sprite.Create(flippedAvatar, avatarRect, avatarPivot);
            }
        }
    }

    public static void SetSteamDataDefaults() {
        steamData.steamUsernameText.SetText($"not logged in");
        steamData.steamAvatarImage.sprite = steamData.placeholderAvatar;
    }

    private static Texture2D FlipTexture(Texture2D original) {
        Texture2D flipped = new Texture2D(original.width, original.height);

        int xN = original.width;
        int yN = original.height;

        for (int i = 0; i < xN; i++) {
            for (int j = 0; j < yN; j++) {
                flipped.SetPixel(i, yN - j - 1, original.GetPixel(i, j));
            }
        }

        flipped.Apply();

        return flipped;
    }
}
