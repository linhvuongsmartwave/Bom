using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Effect"))
        {
            anim.SetBool("Destroy",true);
        }
    }

    public void DestroyBrick()
    {
        Destroy(this.gameObject);
    }
}
