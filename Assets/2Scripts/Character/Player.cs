using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    bool isTakeDamage = false;
    bool canTakeDame = true;

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
        yield return new WaitForSeconds(1.0f); // Cooldown period to prevent multiple triggers
        canTakeDame = true;
    }

    public void TakeDamage()
    {
        if (!isTakeDamage)
        {
            Debug.Log("TakeDamage");
        }
    }

    public void heal(int damage)
    {
        currentHealth -= damage;
        sliderheath.value = currentHealth;
        Debug.Log("currentHealth : "+ currentHealth);

        if (currentHealth<=0)
        {
            Debug.Log("cut");
        }
    }




}
