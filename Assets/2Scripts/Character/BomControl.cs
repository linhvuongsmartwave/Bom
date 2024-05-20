using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomControl : MonoBehaviour
{
    [Header("Bom")]
    public GameObject bomPrefabs;
    public float bomFuseTime = 3f;
    public int bombAmount = 1;
    private int bomRemaining;
    bool isPushBom = false;
    public LayerMask effectLayer;
    [Header("Effect")]
    public Explosion effect;
    public float duration = 1f;
    public int radius = 1;


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
        GameObject bom = Instantiate(bomPrefabs, position, Quaternion.identity);
        bomRemaining--;
        yield return new WaitForSeconds(bomFuseTime);

        position = bom.transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        Explosion explosion = Instantiate(effect, position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        explosion.DestroyAfter(duration);
        Explode(position, Vector2.up, radius);
        Explode(position, Vector2.down, radius);
        Explode(position, Vector2.left, radius);
        Explode(position, Vector2.right, radius);

        Destroy(bom);
        bomRemaining++;

    }

    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length <= 0) return;
        position += direction;
        if (Physics2D.OverlapBox(position,Vector2.one/2f,0,effectLayer))
            return;

        Explosion explosion = Instantiate(effect, position, Quaternion.identity);
        explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end);
        explosion.SetDirection(direction);
        explosion.DestroyAfter(duration);

        Explode(position,direction,length-1);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bom"))
        {
            collision.isTrigger = false;
        }
    }
}
