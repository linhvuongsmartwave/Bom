using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public float speedMove;
    public Slider sliderheath;

    public Animator anm;
    public Rigidbody2D rb;
    public Vector2 movement;
    public CharacterData characterData;

    public virtual void Start()
    {
        anm = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        maxHealth = characterData.health;
        speedMove = characterData.speedMove;
        Debug.Log("speedMove: "+ speedMove);
    }

    public virtual void Update()
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
    public virtual  void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speedMove * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SpeedUp"))
        {
            speedMove += speedMove/3;
            Destroy(collision.gameObject);

            Debug.Log("speedMove: "+ speedMove);

        }
    }

}
