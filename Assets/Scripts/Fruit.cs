using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [Header("Mechanics")]
    public GameObject slicedFruitPrefab;

    public void CreateSlicedFruit()
    {
        // Play sound
        FindObjectOfType<GameManager>().PlayRandomSliceSound();

        // Instantiate fruit slices
        GameObject instanceOfSlicedBody = (GameObject)Instantiate(slicedFruitPrefab, transform.position, transform.rotation);
        Rigidbody[] rigidBodiesOnSliced = instanceOfSlicedBody.GetComponentsInChildren<Rigidbody>();

        // Add explosion force to each part of the slices
        foreach (Rigidbody rigidBody in rigidBodiesOnSliced) {
            rigidBody.rotation = Random.rotation;
            rigidBody.AddExplosionForce(Random.Range(500, 1000), transform.position, 5f);
        }

        // Increase the score
        FindObjectOfType<GameManager>().IncreaseScore(1);

        // Clean up
        Destroy(gameObject);                          // Destroy fruit
        Destroy(instanceOfSlicedBody.gameObject, 5);  // Destroy fruit slices after 5 seconds
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Get the blade
        Blade b = collision.GetComponent<Blade>();
        // If the collision isn't a blade, ignore it
        if (!b) return;
        // Slice fruit
        CreateSlicedFruit();
    }
}
