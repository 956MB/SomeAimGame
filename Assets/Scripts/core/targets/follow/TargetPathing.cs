using System.Collections.Generic;
using UnityEngine;
using PathCreation;

namespace SomeAimGame.Targets {
    public class TargetPathing : MonoBehaviour {
        public static List<Vector3> waypoints;
        public static bool closedLoop   = false;
        public static int waypointCount = 50;
        public GameObject followTargetSpawnArea;
        public static Bounds followTargetSpawnAreaBounds;
        public static GameObject pathFollowerTarget;
        public GameObject randomWaypointSphere;

        private static TargetPathing targetPathing;
        void Awake() {
            targetPathing = this;
            waypoints     = new List<Vector3>();
        }

        #region Glob

        public static void CreateGlobPath(Bounds spawnArea, Vector3 center) {
            if (PathFollower.speed != 20) { PathFollower.speed = 20; }

            waypoints.Clear();
            waypoints.Add(new Vector3(center.x / 2, center.y / 2, center.z / 3));
            //waypoints.Add(new Vector3(17.6f / 2, 10.7f / 2, 51.2f / 3));
            waypoints.Add(targetPathing.ReturnRandomPoint(followTargetSpawnAreaBounds));
            //Debug.Log($"1: {waypoints[0]}, 2: {waypoints[1]}");

            pathFollowerTarget = Instantiate(SpawnTargets.primaryTargetObject, waypoints[0], Quaternion.identity);
            CreatePathObj();
        }

        private Vector3 ReturnRandomPoint(Bounds area) {
            int randomPick = Random.Range(0, 2);
            return TargetUtil.RandomPointInBounds_Glob(area, randomPick);
        }

        public static void InitSpawnAreaBounds_Glob() {
            followTargetSpawnAreaBounds = targetPathing.followTargetSpawnArea.GetComponent<BoxCollider>().bounds;
        }

        #endregion

        #region Follow

        /// <summary>
        /// Generates random waypoints and creates path object for "Gamemode-Follow".
        /// </summary>
        public static void StartFollowGamemode() {
            closedLoop = true;
            followTargetSpawnAreaBounds = targetPathing.followTargetSpawnArea.GetComponent<BoxCollider>().bounds;
            GenerateRandomWaypoints();
            pathFollowerTarget = Instantiate(SpawnTargets.primaryTargetObject, waypoints[0], Quaternion.identity);
            CreatePathObj();
        }

        /// <summary>
        /// Generates list of random waypoints.
        /// </summary>
        public static void GenerateRandomWaypoints() {
            if (followTargetSpawnAreaBounds != null) {
                for (int i = 0; i < waypointCount; i++) {
                    waypoints.Add(TargetUtil.RandomPointInBounds_Follow(followTargetSpawnAreaBounds));
                }

                // Make first waypoint location start right in front of player camera.
                waypoints[0] = new Vector3(17.6f/2, 10.7f/2, 51.2f/3);
            }
        }

        #endregion
        
        /// <summary>
        /// Creates follow path object using randomly generated waypoints.
        /// </summary>
        public static void CreatePathObj() {
            BezierPath bezierPath = new BezierPath(waypoints, closedLoop, PathSpace.xyz);
            targetPathing.GetComponent<PathCreator>().bezierPath = bezierPath;
        }
    }
}
