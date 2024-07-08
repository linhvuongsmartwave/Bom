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
    bool touchS=false;
    bool isTakeDamage = false;
    bool canTakeDame = true;
    int touchBom = 0;
    private GameObject iconShield;

    public override void Awake()
    {
        base.Awake();
        //if (typePlayer==TypePlayer.speedPlayer) speedMove += 1;
    }
    private void OnEnable()
    {
        movementJoystick = GameObject.Find("Joystick").GetComponent<FixedJoystick>();
        iconShield = GameObject.Find("ShieldFalse");
        Debug.Log("player");

    }

    public void ShowIconShield()
    {
        iconShield.SetActive(false);
    }
    public void HideIconShield()
    {
        iconShield.SetActive(true);
    }

    public void Update()
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

    public void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speedMove * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Const.speedUp))
        {
            speedMove += 0.5f;
            Destroy(collision.gameObject);
            RfHolder.Instance.UpdateSpeed();
        }
        if (collision.gameObject.CompareTag(Const.effectEnemy) || collision.gameObject.CompareTag(Const.enemy) || collision.gameObject.CompareTag(Const.effectPlayer) && canTakeDame)
        {
            StartCoroutine(HandleEffectCollision());
            isTakeDamage = false;
            HideIconShield();
        }

        if (collision.gameObject.CompareTag(Const.shield))
        {
            isTakeDamage = true;
            Destroy(collision.gameObject);
            ShowIconShield();
            if (!touchS)
            {
                currentHealth++;
                RfHolder.Instance.UpdateHeart();
                touchS = true;
            }
      
            
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
        currentHealth--;
        RfHolder.Instance.UpdateHeart();
        if (!isTakeDamage)
        {
            touchS=false;
            touchBom += 1;
       
            if (typePlayer == TypePlayer.normalPlayer || typePlayer == TypePlayer.speedPlayer)
            {
                if (touchBom == 1)
                {
                    Die();
                    touchBom = 0;
                }
            }
            else if (typePlayer == TypePlayer.buffaloPlayer)
            {
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
        anm.SetBool("Die", true);
        GameManager.Instance.panelLose.PanelFadeIn();
        GameManager.Instance.isPause = false;

    }

    void EventAnimDestroy()
    {
        Destroy(this.gameObject);
    }

    //public void heal(int damage)
    //{
    //    currentHealth -= damage;
    //}
}
