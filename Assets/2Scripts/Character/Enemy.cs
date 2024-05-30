using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public LayerMask obstacleLayer;
    public Vector2 movementDirection = Vector2.right; // Hướng di chuyển mặc định

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
        float distance = 2f;

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
        // Thay đổi hướng di chuyển, ví dụ: đổi từ phải sang trái, từ trên xuống dưới, v.v.
        if (currentDirection == Vector2.right)
            return Vector2.left;
        else if (currentDirection == Vector2.left)
            return Vector2.right;
        else if (currentDirection == Vector2.up)
            return Vector2.down;
        else if (currentDirection == Vector2.down)
            return Vector2.up;

        return Vector2.right; // Hướng mặc định nếu không khớp
    }
}
