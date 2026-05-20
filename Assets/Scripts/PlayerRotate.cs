using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotate : MonoBehaviour
{
    private float startingRot;
    [SerializeField] float rotationLowerBound;
    [SerializeField] float rotationUpperBound;
    void Start()
    {   
        // Need to add this to the angle so the offset is right
        startingRot = transform.rotation.eulerAngles.z;
    }

    void Update()
    {
        rotateAngle();
    }

    void rotateAngle()
    {   
        // Gets mouseposition and converts it to world coordinates
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = ((Vector2)mousePos - (Vector2)transform.position).normalized;

        // Gets the new angle from the direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Clamped so you can't look behind
        transform.rotation = Quaternion.Euler(0, 0, Math.Clamp(angle + startingRot, rotationLowerBound, rotationUpperBound));
    }
}
