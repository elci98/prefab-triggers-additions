using System.Collections;
using UnityEngine;

/**
 * This component instantiates a given prefab at random time intervals and random bias from its object position.
 */
public class TimedSpawnerRandom: MonoBehaviour {
    [SerializeField] Mover prefabToSpawn;
    [SerializeField] Mover SecondPrefabToSpawn;
    [SerializeField] Vector3 velocityOfSpawnedObject;
    [SerializeField] Vector3 velocityOfSecondSpawnedObject;
    [Tooltip("interval in seconds for spawning mirrors")] [SerializeField] float mirrorSpawningDelay = 6f;
    [Tooltip("Minimum time between consecutive spawns, in seconds")] [SerializeField] float minTimeBetweenSpawns = 1f;
    [Tooltip("Maximum time between consecutive spawns, in seconds")] [SerializeField] float maxTimeBetweenSpawns = 3f;
    [Tooltip("Maximum distance in X between spawner and spawned objects, in meters")] [SerializeField] float maxXDistance = 0.5f;
    private float timer = 0;
    void Start() {
        this.StartCoroutine(SpawnRoutine());
    }
    void Update()
    {
        timer += Time.deltaTime;
    }

    private IEnumerator SpawnRoutine() {
        while (true) {
            float timeBetweenSpawns = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
            yield return new WaitForSeconds(timeBetweenSpawns);
            Vector3 positionOfSpawnedObject = new Vector3(
                transform.position.x + Random.Range(-maxXDistance, +maxXDistance),
                transform.position.y,
                transform.position.z);
            GameObject newObject = Instantiate(prefabToSpawn.gameObject, positionOfSpawnedObject, Quaternion.identity);
            newObject.GetComponent<Mover>().SetVelocity(velocityOfSpawnedObject);
            
            Vector3 positionOfSecondSpawnedObject = new Vector3(
                transform.position.x + Random.Range(-2*maxXDistance, +2*maxXDistance),
                transform.position.y + Random.Range(-2*maxXDistance, +2*maxXDistance),
                transform.position.z);
            if (Mathf.Round(timer) % mirrorSpawningDelay == 0)
            {
                GameObject Object = Instantiate(SecondPrefabToSpawn.gameObject, positionOfSecondSpawnedObject, Quaternion.identity);
                    Object.GetComponent<Mover>().SetVelocity(velocityOfSecondSpawnedObject);
            }
        }
    }
}
