using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public  void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        if (horizontalInput != 0)
        {
            movement.x = horizontalInput;
            movement.y = 0;
        }
        else if (verticalInput != 0)
        {
            movement.x = 0;
            movement.y = verticalInput;
        }
        else
        {
            movement.x = 0;
            movement.y = 0;
        }

        anm.SetFloat("Horizontal", movement.x);
        anm.SetFloat("Vertical", movement.y);
        anm.SetFloat("Speed", movement.sqrMagnitude);
    }
    public  void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speedMove * Time.fixedDeltaTime);
    }
}
