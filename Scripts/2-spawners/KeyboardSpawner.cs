using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This component spawns the given object whenever the player clicks a given key.
 */
public class KeyboardSpawner: MonoBehaviour {
    [SerializeField] protected KeyCode keyToPress;
    [SerializeField] protected GameObject prefabToSpawn;
    [SerializeField] protected Vector3 velocityOfSpawnedObject;
    [SerializeField] protected float shootDelay = 1.0f;
    [SerializeField] DelayField delayField;
    private float timer;
    private float lastShotTime = 0;
    
    protected virtual GameObject spawnObject() {

        // Step 1: spawn the new object.
        Vector3 positionOfSpawnedObject = transform.position;  // span at the containing object position.
        Quaternion rotationOfSpawnedObject = Quaternion.identity;  // no rotation.
        GameObject newObject = Instantiate(prefabToSpawn, positionOfSpawnedObject, rotationOfSpawnedObject);

        // Step 2: modify the velocity of the new object.
        Mover newObjectMover = newObject.GetComponent<Mover>();
        if (newObjectMover) {
            newObjectMover.SetVelocity(velocityOfSpawnedObject);
        }

        return newObject;
    }
    private void Start()
    {
        timer = shootDelay;
    }
    private void Update() {
        timer += Time.deltaTime;
        if (Input.GetKeyDown(keyToPress) && (timer - lastShotTime) >= shootDelay){
            delayField.Vanish();
            lastShotTime = timer;
            spawnObject();

        }else if ((timer - lastShotTime) < shootDelay){
            delayField.Show();
            delayField.SetNumber(timer - lastShotTime);
        }else{
            delayField.Vanish();
            
        }
    }
    
}
