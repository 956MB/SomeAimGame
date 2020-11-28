using UnityEngine;

using SomeAimGame.Gamemode;
using SomeAimGame.Targets;

public class TargetGroundHit : MonoBehaviour {
    private float distToGround;
    private GameObject targetObj;
    //private static SpawnTargets ST;

    void FixedUpdate() {
        if (SpawnTargets.gamemode == GamemodeType.FLICK) {
            //Debug.Log("???????");
            targetObj = SpawnTargets.currentTargetObj;
            if (targetObj != null && IsGrounded(targetObj)) {
                //Debug.Log("targte grounded??");
                Destroy(targetObj.transform.gameObject);
                GameUI.DecreaseScore();
                SpawnTargets.SpawnSingle(SpawnTargets.primaryTargetObject);
            }
        }
    }

    private bool IsGrounded(GameObject target) {
        distToGround = target.GetComponent<Collider>().bounds.extents.y;
        return Physics.Raycast(target.transform.position, Vector3.down, distToGround + 0.4f);
    }
}
