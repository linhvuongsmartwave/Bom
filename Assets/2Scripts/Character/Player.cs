using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public FixedJoystick movementJoystick;

    public TypePlayer typePlayer;
    public enum TypePlayer
    {
        normalPlayer,
        buffaloPlayer,
        speedPlayer
    }


    bool isTakeDamage = false;
    bool canTakeDame = true;
    int touchBom = 0;
    private void Awake()
    {
        movementJoystick = GameObject.Find("Joystick").GetComponent<FixedJoystick>();

    }
    public override void Start()
    {
        base.Start();
        if (typePlayer==TypePlayer.speedPlayer)
        {
            speedMove += 5;
        }
    }
    public  void Update()
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
    public  void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speedMove * Time.fixedDeltaTime);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Const.speedUp))
        {
            speedMove += 1;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag(Const.effectEnemy)|| collision.gameObject.CompareTag(Const.enemy) || collision.gameObject.CompareTag(Const.effectPlayer) && canTakeDame)
        {
            StartCoroutine(HandleEffectCollision());
            isTakeDamage = false;
        }

        if (collision.gameObject.CompareTag(Const.shield))
        {
            isTakeDamage = true;
            Destroy(collision.gameObject);
        }
    }

    IEnumerator HandleEffectCollision()
    {
        canTakeDame = false;
        TakeDamage();
        yield return new WaitForSeconds(1.0f); 
        canTakeDame = true;
    }

    public void TakeDamage()
    {
        if (!isTakeDamage)
        {
            touchBom += 1;
            if (typePlayer == TypePlayer.normalPlayer || typePlayer == TypePlayer.speedPlayer)
            {
                heal(maxHealth);
                if (touchBom == 1)
                {
                    Die();
                    touchBom = 0;
                }
            }
            else if (typePlayer == TypePlayer.buffaloPlayer)
            {
                heal(maxHealth/2);

                if (touchBom == 2)
                {
                    Die();
                    touchBom = 0;
                }
            }
        }
    }

    public void Die()
    {
        Debug.Log("Die");
    }

    public void heal(int damage)
    {
        currentHealth -= damage;
        sliderheath.value = currentHealth;

    }




}
