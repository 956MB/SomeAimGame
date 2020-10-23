using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class GenerateFollowPath : MonoBehaviour {
    public static bool closedLoop = true;
    public static List<Vector3> waypoints;
    public static int waypointCount = 50;
    public GameObject followTargetSpawnArea;
    public static Bounds followTargetSpawnAreaBounds;
    public static GameObject pathFollowerTarget;
    public GameObject randomWaypointSphere;

    private static GenerateFollowPath generateFollow;
    void Awake() { generateFollow = this; }

    /// <summary>
    /// Generates random waypoints and creates path object for "Gamemode-Follow".
    /// </summary>
    public static void StartFollowGamemode() {
        //Debug.Log("startFollowGamemode START::");
        waypoints = new List<Vector3>();
        followTargetSpawnAreaBounds = generateFollow.followTargetSpawnArea.GetComponent<BoxCollider>().bounds;
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
                waypoints.Add(RandomPointInBounds(followTargetSpawnAreaBounds));
                //Instantiate(generateFollow.randomWaypointSphere, new Vector3(waypoints[i].x*2, waypoints[i].y*2, waypoints[i].z*2), Quaternion.identity);
            }

            // Make first waypoint location start right in front of player camera.
            waypoints[0] = new Vector3(17.6f/2, 10.7f/2, 51.2f/3);
        }
    }

    /// <summary>
    /// Creates follow path object using randomly generated waypoints.
    /// </summary>
    public static void CreatePathObj() {
        BezierPath bezierPath = new BezierPath(waypoints, closedLoop, PathSpace.xyz);
        generateFollow.GetComponent<PathCreator>().bezierPath = bezierPath;
    }

    /// <summary>
    /// Randomly selects points in supplied 'targetSpawnAre' bounds to return new random waypoint (Vector3).
    /// </summary>
    /// <param name="bounds"></param>
    /// <returns></returns>
    public static Vector3 RandomPointInBounds(Bounds bounds) {
        // Random X/Y/Z points inside bounds.
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(
            randomX/2,
            randomY/2,
            randomZ/3
        );
    }
}
