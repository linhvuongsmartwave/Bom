using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomControl : MonoBehaviour
{
    [Header("Bom")]
    public GameObject bomPrefabs;
    public float bomFuseTime = 3f;
    public int bomRemaining;
    bool isPushBom = false;
    public LayerMask effectLayer;

    [Header("Effect")]
    public Explosion effect;
    public float duration = 1f;
    public int radius ;

    private void Start()
    {
        radius = 1;
        bomRemaining = 1;
    }


    private void Update()
    {
        if (bomRemaining > 0 &&  Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(PlaceBom());
        }

    }

    public void PutBom()
    {
        if (bomRemaining > 0)
        {

            StartCoroutine(PlaceBom());
        }
    }
    public IEnumerator PlaceBom()
    {
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        Collider2D bomAlready = Physics2D.OverlapBox(position, Vector2.one / 2f, 0, LayerMask.GetMask("Bom"));
        if (bomAlready != null)
        {
            yield break; 
        }
        GameObject bom = Instantiate(bomPrefabs, position, Quaternion.identity);
        bomRemaining--;
        AudioManager.Instance.CoolDown();   
        yield return new WaitForSeconds(bomFuseTime);

        StartCoroutine(VibrateCamera(0.2f, 0.07f));
        AudioManager.Instance.BomExp();

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
        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0, effectLayer))
            return;

        Explosion explosion = Instantiate(effect, position, Quaternion.identity);
        explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end);
        explosion.SetDirection(direction);
        explosion.DestroyAfter(duration);

        Explode(position, direction, length - 1);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isPushBom && collision.gameObject.layer == LayerMask.NameToLayer("Bom"))
        {
            collision.isTrigger = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PushBom"))
        {
            isPushBom = true;
            Destroy(collision.gameObject);
        }
    }

    IEnumerator VibrateCamera(float duration, float magnitude)
    {
        Vector3 originalPosition = Camera.main.transform.position;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            Camera.main.transform.position = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Camera.main.transform.position = originalPosition;
    }
}
