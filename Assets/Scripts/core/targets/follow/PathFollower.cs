using UnityEngine;
using PathCreation;

using SomeAimGame.Gamemode;
using SomeAimGame.Targets;
using System;

public class PathFollower : MonoBehaviour {
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public static float speed = 13;
    float distanceTravelled;

    private static PathFollower pathFollow;
    void Awake() { pathFollow = this; }

    void Update() {
        // Update target position along follow path if gamemode is "Gamemode-Follow" and 'pathCreator' not null.
        if (pathCreator != null) {
            if (SpawnTargets.gamemode == GamemodeType.FOLLOW || (SpawnTargets.gamemode == GamemodeType.GLOB && !SpawnTargets.globStarterActive)) {
                distanceTravelled += speed * Time.deltaTime;

                try {
                    TargetPathing.pathFollowerTarget.transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                } catch (NullReferenceException) {
                    // fff
                }
            }
        }
    }

    // If the path changes during the game, update the distance travelled so that the follower's position on the new path
    // is as close as possible to its position on the old path
    void OnPathChanged() {
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(TargetPathing.pathFollowerTarget.transform.position);
    }

    /// <summary>
    /// Destroys path object when no longer needed.
    /// </summary>
    public static void DestroyPathObj() {
        pathFollow.distanceTravelled = 0f;
        //pathFollow.pathCreator       = null;
        //pathFollow.pathCreator       = new PathCreator();
    }
}
