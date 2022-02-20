using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    [Header("Mouse Settings")]
    public float minVelocity = 0.1f;
    private Vector3 lastMousePos;
    private Vector3 mouseVelocity;
    private Collider2D col;

    private Rigidbody2D rigidBody;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        col.enabled = IsMouseMoving();
        SetBladeToMouse();
    }

    private void SetBladeToMouse()
    {
        var mousePos = Input.mousePosition;  // Get the mouse position
        mousePos.z = 10;                     // Make the mouse z axis 10 units from the camera
        // Assign the mouse position to the rigid body position
        rigidBody.position = Camera.main.ScreenToWorldPoint(mousePos);
    }

    // This function is to make sure blade isn't active if the mouse isn't moving
    private bool IsMouseMoving()
    {
        Vector3 curMousePos = transform.position;
        float traveled = (lastMousePos - curMousePos).magnitude;
        lastMousePos = curMousePos;

        return (traveled > minVelocity);
    }
}
