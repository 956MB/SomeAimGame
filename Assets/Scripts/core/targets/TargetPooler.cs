using System.Collections.Generic;
using UnityEngine;

public class TargetPooler : MonoBehaviour
{
    [System.Serializable]
    public class TargetPool {
        public string targetTag;
        public GameObject targetPrefab;
        public int poolSize;

    }

    #region Singleton
    public static TargetPooler Instance;
    private void Awake() { Instance = this; }
    #endregion

    public List<TargetPool> targetPools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Start() {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (TargetPool pool in targetPools) {
            Queue<GameObject> targetPool = new Queue<GameObject>();

            for (int i = 0; i < pool.poolSize; i++) {
                GameObject targetObj = Instantiate(pool.targetPrefab);
                targetObj.SetActive(false);
                targetPool.Enqueue(targetObj);
            }

            poolDictionary.Add(pool.targetTag, targetPool);
        }
    }

    public GameObject SpawnFromPool(string targetTag, Vector3 targetPosition, Quaternion targetRotation) {
        if (!poolDictionary.ContainsKey(targetTag)) {
            //Debug.Log("Pool with tag " + targetTag + "doesnt exist.");
            return null;
        }

        GameObject targetToSpawn = poolDictionary[targetTag].Dequeue();

        targetToSpawn.SetActive(true);
        targetToSpawn.transform.position = targetPosition;
        targetToSpawn.transform.rotation = targetRotation;

        poolDictionary[targetTag].Enqueue(targetToSpawn);

        return targetToSpawn;
    }
}
