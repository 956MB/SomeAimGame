using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class SteamTest : MonoBehaviour {
    void Start() {
        if (!SteamManager.Initialized) { return; }

        string steamUsername = SteamFriends.GetPersonaName();
        ulong steamID = SteamUser.GetSteamID().m_SteamID;

        Debug.Log($"Username: {steamUsername}, ID: {steamID}");
    }
}
