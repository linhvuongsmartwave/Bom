using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public TypePlayer typePlayer;
    public enum TypePlayer
    {
        normalPlayer,
        HpPlayer,
        speedPlayer
    }


    bool isTakeDamage = false;
    bool canTakeDame = true;
    int touchBom = 0;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SpeedUp"))
        {
            speedMove += speedMove / 3;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Effect") && canTakeDame)
        {
            StartCoroutine(HandleEffectCollision());
            isTakeDamage = false;
        }

        if (collision.gameObject.CompareTag("Shield"))
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
            else if (typePlayer == TypePlayer.HpPlayer)
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
