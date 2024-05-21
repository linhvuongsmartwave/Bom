using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    bool isDie = false;
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
            isDie = false;
        }

        if (collision.gameObject.CompareTag("Shield"))
        {
            isDie = true;
            Destroy(collision.gameObject);
        }
    }

    IEnumerator HandleEffectCollision()
    {
        canTakeDame = false;
        Die();
        yield return new WaitForSeconds(1.0f); // Cooldown period to prevent multiple triggers
        canTakeDame = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

    }
    public void Die()
    {
        if (!isDie)
        {
            Debug.Log("Die");

        }
    }
}
