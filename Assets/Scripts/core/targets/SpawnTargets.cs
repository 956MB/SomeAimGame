using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTargets : MonoBehaviour {
    public GameObject redTarget, orangeTarget, yellowTarget, greenTarget, blueTarget, purpleTarget, pinkTarget, whiteTarget;
    public static GameObject primaryTargetObject, currentTargetObj;
    static GameObject[] targetObjects;
    private static Rigidbody targetRb, secondaryTargetRb;

    public static List<Vector3> targetSpawns, targetSpawnsPrimary, targetSpawnsSecondary;
    private static float targetSize;
    private static Vector3 tinyTargetSize   = new Vector3(0.75f, 0.75f, 0.75f);
    private static Vector3 normalTargetSize = new Vector3(2.6f, 2.6f, 2.6f);
    public static bool targetAreasDestroyed = false;
    public static bool cosmeticsLoaded      = false;
    public GameObject targetSpawnArea, secondaryTargetSpawnArea;
    private static Bounds targetSpawnAreaBounds;
    private static BoxCollider targetSpawnAreaBox;

    private static Vector3 targetInArea, preFallTargetSpawn;
    public int startingTargetCount;
    public static int count, totalCount;
    private static int stepCount = 4;
    public bool targetFall       = false;

    public static int shotsTaken, shotsHit, shotMisses, accuracy;
    public static Gamemode gamemode;
    public static bool gamemodeRestart, targetColorReset;

    // Pairs
    public static bool pairStarterPrimaryActive, pairStarterSecondaryActive, pairStarterActive;
    public static GameObject secondaryTargetObject;
    public static Vector3 activePairLocation, starterTargetCords, targetSpawnAreaCenter;
    public static float targetSpawnAreaCenterY;

    public static List<Vector3> scatterTargetSpawns;

    private static SpawnTargets ST;
    void Awake() {
        ST = this;
        scatterTargetSpawns = new List<Vector3>();
    }

    /// <summary>
    /// Init game with target size, spawns list and saved gamemode. [EVENT]
    /// </summary>
    public static void InitSpawnTargets() {
        // Init gamemode setting.
        if (!cosmeticsLoaded) {
            CosmeticsSaveSystem.InitSavedCosmeticsSettings();

            // Use secondary spawning area if gamemode is "Gamemode-Pairs".
            try {
                if (gamemode == Gamemode.Pairs) {
                    targetSpawnAreaCenterY = ST.secondaryTargetSpawnArea.GetComponent<Renderer>().bounds.center.y;
                    targetSpawnAreaCenter  = ST.secondaryTargetSpawnArea.GetComponent<Renderer>().bounds.center;
                    targetSpawnAreaBox     = ST.secondaryTargetSpawnArea.GetComponent<BoxCollider>();
                    targetSpawnAreaBounds  = ST.secondaryTargetSpawnArea.GetComponent<BoxCollider>().bounds;
                    secondaryTargetRb      = secondaryTargetObject.GetComponent<Rigidbody>();
                    secondaryTargetRb.isKinematic = true;
                } else {
                    targetSpawnAreaCenterY = ST.targetSpawnArea.GetComponent<Renderer>().bounds.center.y;
                    targetSpawnAreaCenter  = ST.targetSpawnArea.GetComponent<Renderer>().bounds.center;
                    targetSpawnAreaBox     = ST.targetSpawnArea.GetComponent<BoxCollider>();
                    targetSpawnAreaBounds  = ST.targetSpawnArea.GetComponent<BoxCollider>().bounds;
                    targetRb               = primaryTargetObject.GetComponent<Rigidbody>();
                    targetRb.isKinematic = true;
                }
            } catch (MissingReferenceException mre) {
                //Debug.Log("missing reference exception here: " + mre);
            }

            targetSize      = primaryTargetObject.transform.lossyScale.y;
            cosmeticsLoaded = true;
        }

        // Init target spawns lists.
        targetSpawns          = new List<Vector3>();
        targetSpawnsPrimary   = new List<Vector3>();
        targetSpawnsSecondary = new List<Vector3>();

        // Clear any targets before starting game from saved gamemode.
        RespawnTargets();

        // EVENT:: for saved gamemode load
        //DevEventHandler.CheckGamemodeEvent($"\"{gamemode}\" {I18nTextTranslator.SetTranslatedText("eventgamemodeloaded")}");
    }

    /// <summary>
    /// Select corresponding gamemode and start target spawning actions.
    /// </summary>
    public static void SelectGamemode() {
        // Set target size to normal if gamemode is not "Gamemode-Grid2".
        if (gamemode == Gamemode.Grid2) {
            SetTinyTargets();
        } else {
            SetNormalTargets();
        }

        if (gamemode == Gamemode.Scatter) {
            // Init scatter spawns and destroy target spawn areas.
            scatterTargetSpawns = GetScatterTargetSpawns();
            DestroySpawnAreas();
            SpawnScatter();
        } else if (gamemode == Gamemode.Flick) {
            // Spawn initial single target for "Gamemode-Flick".
            targetRb.isKinematic = !ST.targetFall ? true : false;
            DestroySpawnAreas();
            SpawnSingle(primaryTargetObject);
        } else if (gamemode == Gamemode.Grid || gamemode == Gamemode.Grid2) {
            if (!targetAreasDestroyed) {
                try {
                    ST.targetSpawnArea.GetComponent<BoxCollider>().size = new Vector3(1f, 1f, 0.55f);
                    targetSpawnAreaBounds = ST.targetSpawnArea.GetComponent<BoxCollider>().bounds;
                } catch (MissingReferenceException mre) {
                    Debug.Log("missing reference exception here: " + mre);
                }
            }
            DestroySpawnAreas();

            // Spawn initial 3 targets for "Gamemode-Grid", or initial 8 for "Gamemode-Grid2".
            if (gamemode == Gamemode.Grid) {
                for (int i = 0; i < 3; i++) { SpawnSingle(primaryTargetObject); }
            } else if (gamemode == Gamemode.Grid2) {
                for (int i = 0; i < 8; i++) { SpawnSingle(primaryTargetObject); }
            } else if (gamemode == Gamemode.Grid3) {
                for (int i = 0; i < 2; i++) { SpawnSingle(secondaryTargetObject); }
                for (int i = 0; i < 1; i++) { SpawnSingle(primaryTargetObject); }
            }

        } else if (gamemode == Gamemode.Pairs) {
            // Destroy spawn areas and spawn starter target for "Gamemode-Pairs".
            DestroySpawnAreas();
            SpawnPairsStarter();
        } else if (gamemode == Gamemode.Follow) {
            // Generate path for "Gamemode-Follow" and start.
            GenerateFollowPath.StartFollowGamemode();
        }

        // TODO: potential bug, dont know if spawn area is somehow not being destroyed, or copy is being created.
        DestroyNewSpawnAreas();
    }

    /// <summary>
    /// Start new game (restart) with supplied new gamemode string (newGamemode).
    /// </summary>
    /// <param name="newGamemode"></param>
    public static void StartNewGamemode(Gamemode newGamemode) {
        GameUI.RestartGame(newGamemode);
    }

    /// <summary>
    /// Sets targets size to tiny if current gamemode "Gamemode-Grid2".
    /// </summary>
    public static void SetTinyTargets() {
        ST.redTarget.transform.localScale    = tinyTargetSize;
        ST.orangeTarget.transform.localScale = tinyTargetSize;
        ST.yellowTarget.transform.localScale = tinyTargetSize;
        ST.greenTarget.transform.localScale  = tinyTargetSize;
        ST.blueTarget.transform.localScale   = tinyTargetSize;
        ST.purpleTarget.transform.localScale = tinyTargetSize;
        ST.pinkTarget.transform.localScale   = tinyTargetSize;
        ST.whiteTarget.transform.localScale  = tinyTargetSize;
        targetSize = primaryTargetObject.transform.lossyScale.y;
    }

    /// <summary>
    /// Sets targets size to normal if current gamemode is anything but "Gamemode-Grid2".
    /// </summary>
    public static void SetNormalTargets() {
        ST.redTarget.transform.localScale    = normalTargetSize;
        ST.orangeTarget.transform.localScale = normalTargetSize;
        ST.yellowTarget.transform.localScale = normalTargetSize;
        ST.greenTarget.transform.localScale  = normalTargetSize;
        ST.blueTarget.transform.localScale   = normalTargetSize;
        ST.purpleTarget.transform.localScale = normalTargetSize;
        ST.pinkTarget.transform.localScale   = normalTargetSize;
        ST.whiteTarget.transform.localScale  = normalTargetSize;
        targetSize = primaryTargetObject.transform.lossyScale.y;
    }

    /// <summary>
    /// Spawns single target at random point in spawn area bounds, then increases counts.
    /// </summary>
    public static void SpawnSingle(GameObject targetType) {
        targetInArea     = CheckTargetSpawn(RandomPointInBounds(targetSpawnAreaBounds));
        currentTargetObj = Instantiate(targetType, targetInArea, Quaternion.identity);

        preFallTargetSpawn = currentTargetObj.transform.position;
        count              += 1;
        totalCount         += 1;
    }

    /// <summary>
    /// Spawns single scatter target if count in scatter spawns list >= 1.
    /// </summary>
    public static void CheckScatterSpawns() {
        if (scatterTargetSpawns.Count >= 1) {
            SpawnSingleScatter();
        }
    }

    /// <summary>
    /// Clears scatter spawns list and populates new list.
    /// </summary>
    public static void ClearResetScatterSpawns() {
        scatterTargetSpawns.Clear();
        scatterTargetSpawns = GetScatterTargetSpawns();
    }

    /// <summary>
    /// Spawns single random target from scatter spawns list, then removes spawned target positon from scatter spawns list. Increases counts.
    /// </summary>
    public static void SpawnSingleScatter() {
        targetInArea     = scatterTargetSpawns[UnityEngine.Random.Range(0, scatterTargetSpawns.Count)];
        currentTargetObj = Instantiate(primaryTargetObject, targetInArea, Quaternion.identity);
        scatterTargetSpawns.Remove(targetInArea);

        preFallTargetSpawn = currentTargetObj.transform.position;
        count              += 1;
        totalCount         += 1;
    }

    /// <summary>
    /// Spawns initial targets from scatter spawns list, then removes those from list. Increases counts.
    /// </summary>
    public static void SpawnScatter() {
        while (count < ST.startingTargetCount + 1) {
            targetInArea     = scatterTargetSpawns[UnityEngine.Random.Range(0, scatterTargetSpawns.Count)];
            currentTargetObj = Instantiate(primaryTargetObject, targetInArea, Quaternion.identity);
            scatterTargetSpawns.Remove(targetInArea);

            preFallTargetSpawn = currentTargetObj.transform.position;
            count              += 1;
            totalCount         += 1;
        }
    }

    /// <summary>
    /// Spawns inital target as pairs starter (if gamemode "Gamemode-Pairs"). Increases counts.
    /// </summary>
    public static void SpawnPairsStarter() {
        pairStarterActive  = true;
        starterTargetCords = targetSpawnAreaCenter;
        int randomPick     = UnityEngine.Random.Range(0, 2);

        // Picks 0 or 1 randomly, spawns either primary or secondary target color.
        if (randomPick == 0) {
            // Primary
            currentTargetObj           = Instantiate(primaryTargetObject, starterTargetCords, Quaternion.identity);
            pairStarterPrimaryActive   = false;
            pairStarterSecondaryActive = true;
            targetSpawnsPrimary.Add(starterTargetCords);
        } else {
            // Secondary
            currentTargetObj           = Instantiate(secondaryTargetObject, starterTargetCords, Quaternion.identity);
            pairStarterPrimaryActive   = true;
            pairStarterSecondaryActive = false;
            targetSpawnsSecondary.Add(starterTargetCords);
        }

        targetSpawns.Add(starterTargetCords);
        count      += 1;
        totalCount += 1;
    }

    /// <summary>
    /// Spawns both primary and secondary color targets, picks randomly to spawn on left or right sides. Increases counts.
    /// </summary>
    public static void SpawnPairs() {
        pairStarterActive = false;
        int randomPick    = UnityEngine.Random.Range(0, 2);
        Vector3 pairPrimary, pairSecondary;

        // Picks 0 or 1 randomly, spawns primary on right / secondary on left OR primary on left / secondary on right.
        if (randomPick == 0) {
            pairPrimary   = PickRandomPairs(targetSpawnAreaBounds, "left");
            pairSecondary = PickRandomPairs(targetSpawnAreaBounds, "right");
        } else {
            pairPrimary   = PickRandomPairs(targetSpawnAreaBounds, "right");
            pairSecondary = PickRandomPairs(targetSpawnAreaBounds, "left");
        }

        // Spawns both primary/secondary targets, then adds them to target spawns lists.
        currentTargetObj = Instantiate(primaryTargetObject, pairPrimary, Quaternion.identity);
        currentTargetObj = Instantiate(secondaryTargetObject, pairSecondary, Quaternion.identity);
        targetSpawns.Add(pairPrimary);
        targetSpawns.Add(pairSecondary);
        targetSpawnsPrimary.Add(pairPrimary);
        targetSpawnsSecondary.Add(pairSecondary);

        if (pairStarterPrimaryActive) {
            activePairLocation = pairSecondary;
        } else {
            activePairLocation = pairPrimary;
        }

        count += 2;
        totalCount += 2;
    }

    /// <summary>
    /// Returns whether or not correct active pair target hit.
    /// </summary>
    /// <param name="hitTarget"></param>
    /// <returns></returns>
    public static bool CheckPairHit(Vector3 hitTarget) {
        if (Vector3.Distance(hitTarget, activePairLocation) == 0) {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Sets active pair starters to false and destroys all targets.
    /// </summary>
    public static void ClearPairs() {
        pairStarterPrimaryActive = pairStarterSecondaryActive = false;
        DestroyTargetObjects();
    }

    /// <summary>
    /// Picks random points (X/Y/Z) inside corresponding spawn area bounds for supplied side (left/right), returns spawn location Vector3.
    /// </summary>
    /// <param name="bounds"></param>
    /// <param name="side"></param>
    /// <returns></returns>
    public static Vector3 PickRandomPairs(Bounds bounds, string side) {
        float randomX, randomY, randomZ;
        randomX = randomY = randomZ = 0;

        switch (side) {
            case "left":
                randomX = UnityEngine.Random.Range(bounds.min.x + targetSize, bounds.max.x - targetSize);
                randomY = UnityEngine.Random.Range(bounds.min.y + targetSize, bounds.max.y - targetSize);
                randomZ = UnityEngine.Random.Range(bounds.min.z, bounds.center.z);
                break;
            case "right":
                randomX = UnityEngine.Random.Range(bounds.min.x + targetSize, bounds.max.x - targetSize);
                randomY = UnityEngine.Random.Range(bounds.min.y + targetSize, bounds.max.y - targetSize);
                randomZ = UnityEngine.Random.Range(bounds.center.z, bounds.max.z);
                break;
        }

        return new Vector3(
            randomX,
            randomY,
            randomZ
        );
    }

    /// <summary>
    /// Destroys all currently active scatter spawned targets and respawns them.
    /// </summary>
    public static void FindAndReplaceCurrentScatterTargets() {
        targetObjects = GameObject.FindGameObjectsWithTag("Target");

        for (int i = 0; i < targetObjects.Length; i++) {
            Vector3 currentScatterPos = targetObjects[i].transform.position;
            Destroy(targetObjects[i]);
            currentTargetObj = Instantiate(primaryTargetObject, currentScatterPos, Quaternion.identity);
        }
    }

    /// <summary>
    /// Sets new target color with supplied color (setColor), and sets opposing secondary target color. If gamemode is "Gamemode-Follow" (gamemodeFollow), target being raycast has its color changed (ChangeFollowTargetColor).
    /// </summary>
    /// <param name="setColor"></param>
    /// <param name="gamemodeFollow"></param>
    public static void SetTargetColor(TargetColor setColor, bool gamemodeFollow) {
        switch (setColor) {
            case TargetColor.Red:
                primaryTargetObject   = ST.redTarget;
                secondaryTargetObject = ST.blueTarget;
                if (gamemodeFollow) { FollowRaycast.ChangeFollowTargetColor(TargetColors.RedAlbedo(), TargetColors.RedEmission(), TargetColors.RedLight()); }
                break;
            case TargetColor.Orange:
                primaryTargetObject   = ST.orangeTarget;
                secondaryTargetObject = ST.blueTarget;
                if (gamemodeFollow) { FollowRaycast.ChangeFollowTargetColor(TargetColors.OrangeAlbedo(), TargetColors.OrangeEmission(), TargetColors.OrangeLight()); }
                break;
            case TargetColor.Yellow:
                primaryTargetObject   = ST.yellowTarget;
                secondaryTargetObject = ST.redTarget;
                if (gamemodeFollow) { FollowRaycast.ChangeFollowTargetColor(TargetColors.YellowAlbedo(), TargetColors.YellowEmission(), TargetColors.YellowLight()); }
                break;
            case TargetColor.Green:
                primaryTargetObject   = ST.greenTarget;
                secondaryTargetObject = ST.redTarget;
                if (gamemodeFollow) { FollowRaycast.ChangeFollowTargetColor(TargetColors.GreenAlbedo(), TargetColors.GreenEmission(), TargetColors.GreenLight()); }
                break;
            case TargetColor.Blue:
                primaryTargetObject   = ST.blueTarget;
                secondaryTargetObject = ST.redTarget;
                if (gamemodeFollow) { FollowRaycast.ChangeFollowTargetColor(TargetColors.BlueAlbedo(), TargetColors.BlueEmission(), TargetColors.BlueLight()); }
                break;
            case TargetColor.Purple:
                primaryTargetObject   = ST.purpleTarget;
                secondaryTargetObject = ST.yellowTarget;
                if (gamemodeFollow) { FollowRaycast.ChangeFollowTargetColor(TargetColors.PurpleAlbedo(), TargetColors.PurpleEmission(), TargetColors.PurpleLight()); }
                break;
            case TargetColor.Pink:
                primaryTargetObject   = ST.pinkTarget;
                secondaryTargetObject = ST.yellowTarget;
                if (gamemodeFollow) { FollowRaycast.ChangeFollowTargetColor(TargetColors.PinkAlbedo(), TargetColors.PinkEmission(), TargetColors.PinkLight()); }
                break;
            case TargetColor.White:
                primaryTargetObject   = ST.whiteTarget;
                secondaryTargetObject = ST.blueTarget;
                if (gamemodeFollow) { FollowRaycast.ChangeFollowTargetColor(TargetColors.WhiteAlbedo(), TargetColors.WhiteEmission(), TargetColors.WhiteLight()); }
                break;
        }
    }

    /// <summary>
    /// Replaces all currently spawned target colors to new supplied color (setColor).
    /// </summary>
    /// <param name="setColor"></param>
    public static void ReplaceCurrentTargetColors(TargetColor setColor) {
        if (gamemode != Gamemode.Follow) {
            // Destroy all targets and set new target color if gamemode is not "Gamemode-Follow" or "Gamemode-Scatter".
            SetTargetColor(setColor, false);
            if (gamemode != Gamemode.Scatter) { DestroyTargetObjects(); }

            if (gamemode != Gamemode.Pairs) {
                if (gamemode == Gamemode.Scatter) {
                    FindAndReplaceCurrentScatterTargets();
                } else {
                    // Loop all currently spawned targets and re-instantiate them.
                    for (int i = 0; i < targetSpawns.Count; i++) {
                        currentTargetObj   = Instantiate(primaryTargetObject, targetSpawns[i], Quaternion.identity);
                        preFallTargetSpawn = currentTargetObj.transform.position;
                    }
                }
            } else {
                if (targetSpawns.Count == 1 && targetSpawnsPrimary.Count == 1) {
                    currentTargetObj = Instantiate(primaryTargetObject, targetSpawns[0], Quaternion.identity);
                } else if (targetSpawns.Count == 1 && targetSpawnsSecondary.Count == 1) {
                    currentTargetObj = Instantiate(secondaryTargetObject, targetSpawns[0], Quaternion.identity);
                }

                for (int i = 0; i < targetSpawnsPrimary.Count; i++) {
                    try { currentTargetObj = Instantiate(primaryTargetObject, targetSpawnsPrimary[i], Quaternion.identity);
                    } catch (ArgumentOutOfRangeException AORE) { Debug.Log($"ArgumentOutOfRangeException I GUESS: {AORE}"); }

                    try { currentTargetObj = Instantiate(secondaryTargetObject, targetSpawnsSecondary[i], Quaternion.identity);
                    } catch (ArgumentOutOfRangeException AORE) { Debug.Log($"ArgumentOutOfRangeException I GUESS: {AORE}"); }
                }
            }
        } else {
            SetTargetColor(setColor, true);
        }
    }

    /// <summary>
    /// Checks count of currently spawned targets and spawns appropriate type, also increases shotsHit/score if hit.
    /// </summary>
    /// <param name="hitTarget"></param>
    /// <param name="hit"></param>
    public static void CheckTargetCount(RaycastHit hitTarget, bool hit) {
        if (hit) {
            if (!pairStarterActive) {
                shotsHit += 1;
            }

            if (gamemode == Gamemode.Scatter) {
                count -= 1;
                if (count <= 0) {
                    scatterTargetSpawns.Remove(hitTarget.transform.position);
                    ClearResetScatterSpawns();
                    SpawnScatter();
                    GameUI.IncreaseScore_Bonus();
                }
            } else if (gamemode == Gamemode.Flick || gamemode == Gamemode.Grid || gamemode == Gamemode.Grid2) {
                SpawnSingle(primaryTargetObject);
            } else if (gamemode == Gamemode.Pairs) {
                if (pairStarterActive) {
                    shotsHit += 1;
                    SpawnPairs();
                }
                else {
                    ClearTargetLists();
                    ClearPairs();
                    SpawnPairsStarter();
                }
            }

            FindAndRemoveTargetFromList(hitTarget.transform.position);
            Destroy(hitTarget.transform.gameObject);
        } else {
            shotMisses += 1;
        }

        shotsTaken += 1;
        GameUI.UpdateAccuracy(shotsHit, shotsTaken);
    }

    /// <summary>
    /// Returns random spawn point (X/Y/Z) inside spawn area bounds based on current gamemode.
    /// </summary>
    /// <param name="bounds"></param>
    /// <returns></returns>
    public static Vector3 RandomPointInBounds(Bounds bounds) {
        float randomX = UnityEngine.Random.Range(bounds.min.x + targetSize, bounds.max.x - targetSize);
        float randomY = UnityEngine.Random.Range(bounds.min.y + targetSize, bounds.max.y - targetSize);
        float randomZ = UnityEngine.Random.Range(bounds.min.z + targetSize, bounds.max.z - targetSize);

        if (gamemode == Gamemode.Grid || gamemode == Gamemode.Grid2) {
            randomX = (float)(bounds.size.x * 1.75) - targetSize * 3;
            randomY = Mathf.Floor(randomY / stepCount);
            randomZ = Mathf.Floor(randomZ / stepCount);
            randomY = randomY * stepCount;
            randomZ = randomZ * stepCount;
        } else if (gamemode == Gamemode.Scatter) {
            randomX = Mathf.Floor(randomX / stepCount);
            randomY = Mathf.Floor(randomY / stepCount);
            randomZ = Mathf.Floor(randomZ / stepCount);
            randomX = randomX * stepCount;
            randomY = randomY * stepCount;
            randomZ = randomZ * stepCount;
        }

        return new Vector3(
            randomX,
            randomY,
            randomZ
        );
    }

    /// <summary>
    /// Checks supplied spawn point (targetSpawn) to see if targetSpawns list already contains it (target location already used), then returns valid spawn point after while loop. [EVENT //TODO]
    /// </summary>
    /// <param name="targetSpawn"></param>
    /// <returns></returns>
    private static Vector3 CheckTargetSpawn(Vector3 targetSpawn) {
        Vector3 newSpawn = targetSpawn;

        // Loops until valid spawn point selected from random supplied spawn (targetSpawn).
        while (true) {
            if (targetSpawns.Contains(newSpawn)) {
                newSpawn = RandomPointInBounds(targetSpawnAreaBounds);
            } else {
                break;
            }
        }

        targetSpawns.Add(newSpawn);
        return newSpawn;
    }

    /// <summary>
    /// Returns initial list of available scatter spawns (Vector3). [EVENT]
    /// </summary>
    /// <returns></returns>
    private static List<Vector3> GetScatterTargetSpawns() {
        Vector3 newSpawn;

        while (true) {
            newSpawn = RandomPointInBounds(targetSpawnAreaBounds);
            if (!scatterTargetSpawns.Contains(newSpawn)) {
                scatterTargetSpawns.Add(newSpawn);
            }

            if (scatterTargetSpawns.Count >= 72) {
                // EVENT:: for initial scatter spawns list
                //DevEventHandler.CheckTargetsEvent($"{I18nTextTranslator.SetTranslatedText("eventtargetsscatterinitial")} ({scatterTargetSpawns.Count})");

                return scatterTargetSpawns;
            }
        }

        //return scatterTargetSpawns;
    }

    /// <summary>
    /// Finds and removes supplied target position (targetPos) from target spawns list. [EVENT]
    /// </summary>
    /// <param name="targetPos"></param>
    private static void FindAndRemoveTargetFromList(Vector3 targetPos) {
        Vector3 pos = targetPos;

        if (ST.targetFall) { pos = preFallTargetSpawn; }

        if (gamemode == Gamemode.Scatter) {
            if (count != 0) { scatterTargetSpawns.Add(pos); }
        } else {
            targetSpawns.Remove(pos);
            if (targetSpawnsPrimary.Contains(targetPos)) {
                targetSpawnsPrimary.Remove(targetPos);
            } else if (targetSpawnsSecondary.Contains(targetPos)) {
                targetSpawnsSecondary.Remove(targetPos);
            }
        }

        // EVENT:: for remove target from spawn list
        //DevEventHandler.CheckTargetsEvent($"{I18nTextTranslator.SetTranslatedText("eventtargetsremove")} ({pos})");
    }

    /// <summary>
    /// Clears all target spawns lists. [EVENT]
    /// </summary>
    public static void ClearTargetLists() {
        targetSpawns.Clear();
        targetSpawnsPrimary.Clear();
        targetSpawnsSecondary.Clear();

        // EVENT:: for clear target lists
        //DevEventHandler.CheckGamemodeEvent($"{I18nTextTranslator.SetTranslatedText("eventinterfaceelementsreset")} \"{gamemode}\"");
    }

    /// <summary>
    /// Destroys target spawn area boxes after their bounds are saved. [EVENT]
    /// </summary>
    private static void DestroySpawnAreas() {
        if (!targetAreasDestroyed) {
            try {
                Destroy(ST.targetSpawnArea.gameObject);
                Destroy(ST.secondaryTargetSpawnArea.gameObject);
                targetAreasDestroyed = true;
            } catch (MissingReferenceException mre) {
                Debug.Log("missing reference exception here, couldnt destroy spawn area: " + mre);
                targetAreasDestroyed = true;
            }

            // EVENT:: for destroy spawn areas
            //DevEventHandler.CheckTargetsEvent($"{I18nTextTranslator.SetTranslatedText("eventtargetsdestroyspawnarea")} \"{gamemode}\"");
        }
    }

    /// <summary>
    /// Loops and finds any lasting target spawn area boxes and destroys them (possible bug). [EVENT]
    /// </summary>
    private static void DestroyNewSpawnAreas() {
        // Finds any gameObjects with "TargetSpawnArea" tag and destroys them.
        try {
            GameObject[] newTargetSpawnAreas = GameObject.FindGameObjectsWithTag("TargetSpawnArea");
            foreach (GameObject targetSpawnAreaGO in newTargetSpawnAreas) {
                Destroy(targetSpawnAreaGO);
            }
        } catch (MissingReferenceException mre) {
            Debug.Log("missing reference exception here, couldnt destroy \"NEW\" spawn area: " + mre);
        }

        // EVENT:: for destroy new spawn areas
        //DevEventHandler.CheckTargetsEvent($"*NEW* {I18nTextTranslator.SetTranslatedText("eventtargetsdestroyspawnarea")} \"{gamemode}\"");
    }

    /// <summary>
    /// Finds any gameObjects with tag "Target" and destroys them. [EVENT]
    /// </summary>
    public static void DestroyTargetObjects() {
        targetObjects = GameObject.FindGameObjectsWithTag("Target");
        for (int i = 0; i < targetObjects.Length; i++) { Destroy(targetObjects[i]); }

        // EVENT:: for destroy new spawn areas
        //DevEventHandler.CheckTargetsEvent($"{I18nTextTranslator.SetTranslatedText("eventtargetsalldestroyed")} \"{gamemode}\"");
    }

    /// <summary>
    /// Destroys all target gameObjects and respawns them with current gamemode. [EVENT]
    /// </summary>
    public static void RespawnTargets() {
        DestroyTargetObjects();

        // EVENT:: for respawn targets
        //DevEventHandler.CheckTargetsEvent($"{I18nTextTranslator.SetTranslatedText("eventtargetsrespawn")} \"{gamemode}\"");

        SelectGamemode();
    }

    /// <summary>
    /// Resets all target game values. [EVENT]
    /// </summary>
    public static void ResetSpawnTargetsValues() {
        shotsHit               = 0;
        shotsTaken             = 0;
        count                  = 0;
        totalCount             = 0;
        GunAction.timerRunning = true;
        cosmeticsLoaded        = false;

        // EVENT:: for reset targets spawn values
        //DevEventHandler.CheckGamemodeEvent($"{I18nTextTranslator.SetTranslatedText("eventtargetsreset")}");
    }
}
