using UnityEngine;
using PathCreation;

using SomeAimGame.Gamemode;
using SomeAimGame.Targets;

public class PathFollower : MonoBehaviour {
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public static float speed = 13;
    float distanceTravelled;

    private static PathFollower pathFollow;
    void Awake() { pathFollow = this; }

    void Update() {
        // Update target position along follow path if gamemode is "Gamemode-Follow" and 'pathCreator' not null.
        if (pathCreator != null && SpawnTargets.gamemode == GamemodeType.FOLLOW) {
            distanceTravelled += speed * Time.deltaTime;
            GenerateFollowPath.pathFollowerTarget.transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
        }
    }

    // If the path changes during the game, update the distance travelled so that the follower's position on the new path
    // is as close as possible to its position on the old path
    void OnPathChanged() {
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(GenerateFollowPath.pathFollowerTarget.transform.position);
    }

    /// <summary>
    /// Destroys path object when no longer needed.
    /// </summary>
    public static void DestroyPathObj() { pathFollow.pathCreator = null; }
}
