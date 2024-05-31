using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public LayerMask obstacleLayer;
    public Vector2 movementDirection = Vector2.right; // Hướng di chuyển mặc định
    Vector2[] randomDirections = new Vector2[] { Vector2.down, Vector2.left, Vector2.right, Vector2.up };

    public void Update()
    {
        // Di chuyển Enemy liên tục theo hướng mặc định
        movement = movementDirection;

        anm.SetFloat("Horizontal", movement.x);
        anm.SetFloat("Vertical", movement.y);
        anm.SetFloat("Speed", movement.sqrMagnitude);
        CheckForObstacles();
    }

    public void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speedMove * Time.fixedDeltaTime);
    }

    private void CheckForObstacles()
    {
        Vector2 position = rb.position;
        Vector2 direction = new Vector2(movement.x, movement.y).normalized;
        float distance = 0.8f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, obstacleLayer);
        if (hit.collider != null)
        {
            Debug.Log("Đã va chạm với: " + hit.collider.name);
            // Đổi hướng di chuyển khi gặp vật cản
            movementDirection = GetNewDirection(movementDirection);
        }

        Debug.DrawRay(position, direction * distance, Color.red);
    }

    private Vector2 GetNewDirection(Vector2 currentDirection)
    {
        List<Vector2> newDirections = new List<Vector2>(randomDirections);
        newDirections.Remove(currentDirection);
        int randomIndex = Random.Range(0, newDirections.Count);
        return newDirections[randomIndex];
    }
}
