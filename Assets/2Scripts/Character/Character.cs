using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [HideInInspector] public Animator anm;
    [HideInInspector] public int maxHealth;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public float speedMove;
    [HideInInspector] public Vector2 movement;
    [HideInInspector] public int currentHealth;
//    public Joystick movementJoystick;
    public FixedJoystick movementJoystick;
    public Slider sliderheath;
    public CharacterData characterData;

    public virtual void Start()
    {
        anm = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        maxHealth = characterData.health;
        currentHealth = maxHealth;
        sliderheath.value = currentHealth;
        speedMove = characterData.speedMove;
    }

    public virtual void Update()
    {
        Vector2 direction = movementJoystick.Direction;
        float horizontalInput = direction.x;
        float verticalInput = direction.y;

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
    public virtual void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speedMove * Time.fixedDeltaTime);
    }

   

}
