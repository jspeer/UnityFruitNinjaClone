using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Fruit")]
    public GameObject[] fruitToSpawn;
    public GameObject bomb;
    public Transform[] spawnPlaces;
    public float bombPercent = 10;
    public float minWait = .3f;
    public float maxWait = 1f;
    public float minForce = 12;
    public float maxForce = 17;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnFruits());
    }

    private IEnumerator SpawnFruits()
    {
        while (true) {
            // Wait a random duration
            yield return new WaitForSeconds(Random.Range(minWait, maxWait));

            // Pick a random spawner
            Transform t = spawnPlaces[Random.Range(0,spawnPlaces.Length)];

            // Pick spawn object from array.
            GameObject spawnObject = null;
            if (Random.Range(0, 100) <= bombPercent) {
                spawnObject = bomb;
                //spawnObject.transform.rotation.Set(spawnObject.transform.rotation.x-90, spawnObject.transform.rotation.y, spawnObject.transform.rotation.z, spawnObject.transform.rotation.w);
                //t.rotation.Set(t.rotation.x-90, t.rotation.y, t.rotation.z, t.rotation.w);
            } else {
                spawnObject = fruitToSpawn[Random.Range(0, fruitToSpawn.Length)];
            }

            // Instantiate the fruit object and apply force to it
            GameObject fruit = Instantiate(spawnObject, t.position, t.rotation);
            fruit.GetComponent<Rigidbody2D>().AddForce(t.transform.up * Random.Range(minForce, maxForce), ForceMode2D.Impulse);

            // Destroy the fruit after 5 seconds
            Destroy(fruit, 5);
        }
    }
}
