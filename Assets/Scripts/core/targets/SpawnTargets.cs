﻿using System;
using System.Collections.Generic;
using UnityEngine;

using SomeAimGame.Gamemode;
using SomeAimGame.Utilities;

namespace SomeAimGame.Targets {
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
        public static GamemodeType gamemode;
        public static bool gamemodeRestart, targetColorReset;

        // Pairs
        public static bool pairStarterPrimaryActive, pairStarterSecondaryActive, pairStarterActive, globStarterActive;
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
                    if (gamemode == GamemodeType.PAIRS) {
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
                        targetRb.isKinematic   = true;
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
        }

        /// <summary>
        /// Select corresponding gamemode and start target spawning actions.
        /// </summary>
        public static void SelectGamemode() {
            // Set target size to normal if gamemode is not "Gamemode-Grid2".
            if (gamemode == GamemodeType.GRID_2) {
                SetTinyTargets();
            } else {
                SetNormalTargets();
            }

            if (gamemode == GamemodeType.SCATTER) {
                // Init scatter spawns and destroy target spawn areas.
                scatterTargetSpawns = GetScatterTargetSpawns();
                DestroySpawnAreas();
                SpawnScatter();
            } else if (gamemode == GamemodeType.FLICK) {
                // Spawn initial single target for "Gamemode-Flick".
                targetRb.isKinematic = !ST.targetFall ? true : false;
                DestroySpawnAreas();
                SpawnSingle(primaryTargetObject);
            } else if (gamemode == GamemodeType.GRID || gamemode == GamemodeType.GRID_2 || gamemode == GamemodeType.GRID_3) {
                //if (!targetAreasDestroyed) {
                try {
                    ST.targetSpawnArea.GetComponent<BoxCollider>().size = new Vector3(1f, 1f, 0.55f);
                    targetSpawnAreaBounds = ST.targetSpawnArea.GetComponent<BoxCollider>().bounds;
                } catch (MissingReferenceException mre) {
                    //Debug.Log("missing reference exception here: " + mre);
                }
                //}
                DestroySpawnAreas();
                SpawnInitialGrid();
            } else if (gamemode == GamemodeType.PAIRS) {
                // Destroy spawn areas and spawn starter target for "Gamemode-Pairs".
                DestroySpawnAreas();
                SpawnPairsStarter();
            } else if (gamemode == GamemodeType.FOLLOW) {
                // Generate path for "Gamemode-Follow" and start.
                TargetPathing.StartFollowGamemode();
            } else if (gamemode == GamemodeType.GLOB) {
                TargetPathing.InitSpawnAreaBounds_Glob();
                SpawnGlobCenter();
            }

            // TODO: potential bug, dont know if spawn area is somehow not being destroyed, or copy is being created.
            DestroyNewSpawnAreas();
        }


        #region Scatter

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
        /// Returns initial list of available scatter spawns (Vector3). [EVENT]
        /// </summary>
        /// <returns></returns>
        private static List<Vector3> GetScatterTargetSpawns() {
            Vector3 newSpawn;

            while (true) {
                newSpawn = TargetUtil.RandomPointInBounds(targetSpawnAreaBounds, gamemode, targetSize, stepCount);
                if (!scatterTargetSpawns.Contains(newSpawn)) {
                    scatterTargetSpawns.Add(newSpawn);
                }

                if (scatterTargetSpawns.Count >= 72) {
                    return scatterTargetSpawns;
                }
            }
        }

        private static void HandleScatterHit(RaycastHit hitTarget) {
            count -= 1;
            if (count <= 0) {
                scatterTargetSpawns.Remove(hitTarget.transform.position);
                ClearResetScatterSpawns();
                SpawnScatter();
                GameUI.IncreaseScore_Bonus();
            }
        }

        #endregion


        #region Glob

        public static void SpawnGlobCenter() {
            //globStarterActive  = true;
            starterTargetCords = targetSpawnAreaCenter;

            // spawn center
            currentTargetObj = Instantiate(primaryTargetObject, starterTargetCords, Quaternion.identity);
            targetSpawns.Add(starterTargetCords);
            count      += 1;
            totalCount += 1;
        }

        public static void SpawnNewGlobPath() {
            TargetPathing.CreateGlobPath(targetSpawnAreaBounds, starterTargetCords);
        }

        private static void HandleGlobHit() {
            if (globStarterActive) {
                globStarterActive = false;
                shotsHit += 1;
                // spawn glob path target
                SpawnNewGlobPath();
            } else {
                globStarterActive = true;
                // path target hit
                PathFollower.DestroyPathObj();
                ClearTargetLists();
                //DestroyTargetObjects();
                SpawnGlobCenter();
            }
        }

        #endregion


        #region Pairs

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
                pairPrimary   = TargetUtil.PickRandomPairs(targetSpawnAreaBounds, true, targetSize);
                pairSecondary = TargetUtil.PickRandomPairs(targetSpawnAreaBounds, false, targetSize);
            } else {
                pairPrimary   = TargetUtil.PickRandomPairs(targetSpawnAreaBounds, false, targetSize);
                pairSecondary = TargetUtil.PickRandomPairs(targetSpawnAreaBounds, true, targetSize);
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

            count      += 2;
            totalCount += 2;
        }

        /// <summary>
        /// Sets active pair starters to false and destroys all targets.
        /// </summary>
        public static void ClearPairs() {
            pairStarterPrimaryActive = pairStarterSecondaryActive = false;
            DestroyTargetObjects();
        }

        /// <summary>
        /// Applies correct missed shot if gamemode is pairs.
        /// </summary>
        public static void GamemodePairsMiss() {
            shotsHit   -= 1;
            shotMisses += 1;
        }

        private static void HandlePairsHit() {
            if (pairStarterActive) {
                shotsHit += 1;
                SpawnPairs();
            } else {
                ClearTargetLists();
                ClearPairs();
                SpawnPairsStarter();
            }
        }

        #endregion


        #region Grid

        private static void SpawnInitialGrid() {
            // Spawn initial 3 targets for "Gamemode-Grid", or initial 8 for "Gamemode-Grid2".
            if (gamemode == GamemodeType.GRID) {
                for (int i = 0; i < 3; i++) { SpawnSingle(primaryTargetObject); }
            } else if (gamemode == GamemodeType.GRID_2) {
                for (int i = 0; i < 8; i++) { SpawnSingle(primaryTargetObject); }
            } else if (gamemode == GamemodeType.GRID_3) {
                for (int i = 0; i < 2; i++) { SpawnSingle(secondaryTargetObject); }
                for (int i = 0; i < 1; i++) { SpawnSingle(primaryTargetObject); }
            }
        }

        #endregion


        #region Util

        /// <summary>
        /// Start new game (restart) with supplied new gamemode string (newGamemode).
        /// </summary>
        /// <param name="newGamemode"></param>
        public static void StartNewGamemode(GamemodeType newGamemode) {
            GameUI.RestartGame(newGamemode, true);
        }

        /// <summary>
        /// Sets targets size to tiny if current gamemode "Gamemode-Grid2".
        /// </summary>
        public static void SetTinyTargets() {
            Util.GameObjectLoops.Util_SetObjectsLocalScale(tinyTargetSize, ST.redTarget, ST.orangeTarget, ST.yellowTarget, ST.greenTarget, ST.blueTarget, ST.purpleTarget, ST.pinkTarget, ST.whiteTarget);
            targetSize = primaryTargetObject.transform.lossyScale.y;
        }

        /// <summary>
        /// Sets targets size to normal if current gamemode is anything but "Gamemode-Grid2".
        /// </summary>
        public static void SetNormalTargets() {
            Util.GameObjectLoops.Util_SetObjectsLocalScale(normalTargetSize, ST.redTarget, ST.orangeTarget, ST.yellowTarget, ST.greenTarget, ST.blueTarget, ST.purpleTarget, ST.pinkTarget, ST.whiteTarget);
            targetSize = primaryTargetObject.transform.lossyScale.y;
        }

        /// <summary>
        /// Spawns single target at random point in spawn area bounds, then increases counts.
        /// </summary>
        public static void SpawnSingle(GameObject targetType) {
            targetInArea     = CheckTargetSpawn(TargetUtil.RandomPointInBounds(targetSpawnAreaBounds, gamemode, targetSize, stepCount));
            currentTargetObj = Instantiate(targetType, targetInArea, Quaternion.identity);

            preFallTargetSpawn = currentTargetObj.transform.position;
            count              += 1;
            totalCount         += 1;
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
        public static void SetTargetColor(TargetType setColor, bool gamemodeFollow) {
            switch (setColor) {
                case TargetType.RED:    SetTargetObjects(ST.redTarget, ST.blueTarget, gamemodeFollow, TargetColors.RedAlbedo, TargetColors.RedEmission, TargetColors.RedLight);               break;
                case TargetType.ORANGE: SetTargetObjects(ST.orangeTarget, ST.blueTarget, gamemodeFollow, TargetColors.OrangeAlbedo, TargetColors.OrangeEmission, TargetColors.OrangeLight);   break;
                case TargetType.YELLOW: SetTargetObjects(ST.yellowTarget, ST.redTarget, gamemodeFollow, TargetColors.YellowAlbedo, TargetColors.YellowEmission, TargetColors.YellowLight);    break;
                case TargetType.GREEN:  SetTargetObjects(ST.greenTarget, ST.redTarget, gamemodeFollow, TargetColors.GreenAlbedo, TargetColors.GreenEmission, TargetColors.GreenLight);        break;
                case TargetType.BLUE:   SetTargetObjects(ST.blueTarget, ST.redTarget, gamemodeFollow, TargetColors.BlueAlbedo, TargetColors.BlueEmission, TargetColors.BlueLight);            break;
                case TargetType.PURPLE: SetTargetObjects(ST.purpleTarget, ST.yellowTarget, gamemodeFollow, TargetColors.PurpleAlbedo, TargetColors.PurpleEmission, TargetColors.PurpleLight); break;
                case TargetType.PINK:   SetTargetObjects(ST.pinkTarget, ST.yellowTarget, gamemodeFollow, TargetColors.PinkAlbedo, TargetColors.PinkEmission, TargetColors.PinkLight);         break;
                case TargetType.WHITE:  SetTargetObjects(ST.whiteTarget, ST.blueTarget, gamemodeFollow, TargetColors.WhiteAlbedo, TargetColors.WhiteEmission, TargetColors.WhiteLight);       break;
            }
        }

        /// <summary>
        /// Replaces all currently spawned target colors to new supplied color (setColor).
        /// </summary>
        /// <param name="setColor"></param>
        public static void ReplaceCurrentTargetColors(TargetType setColor) {
            if (gamemode != GamemodeType.FOLLOW) {
                // Destroy all targets and set new target color if gamemode is not "Gamemode-Follow" or "Gamemode-Scatter".
                SetTargetColor(setColor, false);
                if (gamemode != GamemodeType.SCATTER) { DestroyTargetObjects(); }

                if (gamemode != GamemodeType.PAIRS) {
                    if (gamemode == GamemodeType.SCATTER) {
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
                        try {
                            currentTargetObj = Instantiate(primaryTargetObject, targetSpawnsPrimary[i], Quaternion.identity);
                        } catch (ArgumentOutOfRangeException AORE) {
                            Debug.Log($"ArgumentOutOfRangeException I GUESS: {AORE}");
                        }

                        try {
                            currentTargetObj = Instantiate(secondaryTargetObject, targetSpawnsSecondary[i], Quaternion.identity);
                        } catch (ArgumentOutOfRangeException AORE) {
                            Debug.Log($"ArgumentOutOfRangeException I GUESS: {AORE}");
                        }
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
        public static void CheckTargetHit(RaycastHit hitTarget, bool hit) {
            if (hit) {
                if (!pairStarterActive || !globStarterActive) { shotsHit += 1; }

                if (gamemode == GamemodeType.SCATTER) {
                    HandleScatterHit(hitTarget);
                } else if (gamemode == GamemodeType.FLICK || gamemode == GamemodeType.GRID || gamemode == GamemodeType.GRID_2) {
                    SpawnSingle(primaryTargetObject);
                } else if (gamemode == GamemodeType.PAIRS) {
                    HandlePairsHit();
                } else if (gamemode == GamemodeType.GLOB) {
                    HandleGlobHit();
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
        /// Checks supplied spawn point (targetSpawn) to see if targetSpawns list already contains it (target location already used), then returns valid spawn point after while loop. [EVENT //TODO]
        /// </summary>
        /// <param name="targetSpawn"></param>
        /// <returns></returns>
        private static Vector3 CheckTargetSpawn(Vector3 targetSpawn) {
            Vector3 newSpawn = targetSpawn;

            // Loops until valid spawn point selected from random supplied spawn (targetSpawn).
            while (true) {
                if (targetSpawns.Contains(newSpawn)) {
                    newSpawn = TargetUtil.RandomPointInBounds(targetSpawnAreaBounds, gamemode, targetSize, stepCount);
                } else {
                    break;
                }
            }

            targetSpawns.Add(newSpawn);
            return newSpawn;
        }

        /// <summary>
        /// Finds and removes supplied target position (targetPos) from target spawns list. [EVENT]
        /// </summary>
        /// <param name="targetPos"></param>
        private static void FindAndRemoveTargetFromList(Vector3 targetPos) {
            Vector3 pos = targetPos;

            if (ST.targetFall) { pos = preFallTargetSpawn; }

            if (gamemode == GamemodeType.SCATTER) {
                if (count != 0) { scatterTargetSpawns.Add(pos); }
            } else {
                targetSpawns.Remove(pos);
                if (targetSpawnsPrimary.Contains(targetPos)) {
                    targetSpawnsPrimary.Remove(targetPos);
                } else if (targetSpawnsSecondary.Contains(targetPos)) {
                    targetSpawnsSecondary.Remove(targetPos);
                }
            }
        }

        /// <summary>
        /// Clears all target spawns lists. [EVENT]
        /// </summary>
        public static void ClearTargetLists() {
            targetSpawns.Clear();
            targetSpawnsPrimary.Clear();
            targetSpawnsSecondary.Clear();
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
                    //Debug.Log("missing reference exception here, couldnt destroy spawn area: " + mre);
                    targetAreasDestroyed = true;
                }
            }
        }

        /// <summary>
        /// Sets new primary/secondary targets and follow target colors from supplied GameObject (setPrimaryTarget, setSecondaryTarget) and Color (setAlbedo, setEmission, setLight).
        /// </summary>
        /// <param name="setPrimaryTarget"></param>
        /// <param name="setSecondaryTarget"></param>
        /// <param name="gamemodeFollow"></param>
        /// <param name="setAlbedo"></param>
        /// <param name="setEmission"></param>
        /// <param name="setLight"></param>
        private static void SetTargetObjects(GameObject setPrimaryTarget, GameObject setSecondaryTarget, bool gamemodeFollow, Color setAlbedo, Color setEmission, Color setLight) {
            primaryTargetObject   = setPrimaryTarget;
            secondaryTargetObject = setSecondaryTarget;

            if (gamemodeFollow) { FollowRaycast.ChangeFollowTargetColor(setAlbedo, setEmission, setLight); }
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
                //Debug.Log("missing reference exception here, couldnt destroy \"NEW\" spawn area: " + mre);
            }
        }

        /// <summary>
        /// Finds any gameObjects with tag "Target" and destroys them. [EVENT]
        /// </summary>
        public static void DestroyTargetObjects() {
            targetObjects = GameObject.FindGameObjectsWithTag("Target");
            for (int i = 0; i < targetObjects.Length; i++) { Destroy(targetObjects[i]); }
        }

        /// <summary>
        /// Destroys all target gameObjects and respawns them with current gamemode. [EVENT]
        /// </summary>
        public static void RespawnTargets() {
            DestroyTargetObjects();
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
            //TempValues.SetTimerRunningTemp(true);
            cosmeticsLoaded        = false;
        }

        #endregion
    }
}
