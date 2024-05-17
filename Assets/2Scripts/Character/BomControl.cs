using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomControl : MonoBehaviour
{
    public GameObject bomPrefabs;
    public float bomFuseTime = 3f;
    public int bombAmount = 1;
    private int bomRemaining;
    bool isPushBom = false;

    private void OnEnable()
    {
        bomRemaining = bombAmount;
    }

    private void Update()
    {
        if (bomRemaining > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(PlaceBom());
        }
    }
    private IEnumerator PlaceBom()
    {
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        GameObject bom = Instantiate(bomPrefabs,position, Quaternion.identity);
        bomRemaining--;
        yield return new WaitForSeconds(bomFuseTime);
        Destroy(bom);
        bomRemaining++;

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Bom"))
        {
            collision.isTrigger = false;
        }
    }
}
