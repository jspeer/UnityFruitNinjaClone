using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Get the blade
        Blade b = collision.GetComponent<Blade>();
        if (!b) return;
        // Slice bomb
        FindObjectOfType<GameManager>().OnBombHit();

    }
}
