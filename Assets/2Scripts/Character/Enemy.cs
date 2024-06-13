using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public TypeEnemy typeEnemy;
    int bossHp=3;
    public enum TypeEnemy
    {
        boss,
        enemy
    }


    [Header("Bom")]
    public GameObject bomPrefabs;
    public float bomFuseTime = 3f;
    public int bomRemaining;
    public LayerMask effectLayer;

    [Header("Effect")]
    public Explosion effect;
    public float duration = 1f;
    public int radius;

    public LayerMask obstacleLayer;
    public Vector2 movementDirection = Vector2.right;
    Vector2[] randomDirections = new Vector2[] { Vector2.down, Vector2.left, Vector2.right, Vector2.up };

    public override void Start()
    {
        base.Start();
        radius = 1;
        bomRemaining = 2;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Const.effectPlayer))
        {
            if (typeEnemy==TypeEnemy.boss)
            {
                bossHp--;
                if (bossHp <= 0)
                {
                    Debug.Log("- damage");
                    Destroy(this.gameObject);
                    GameManager.Instance.count--;
                    if (GameManager.Instance.count <= 0)
                    {
                        Debug.Log("Win");
                    }
                }
            }
            else
            {
                Destroy(this.gameObject);
                GameManager.Instance.count--;
                if (GameManager.Instance.count<=0)
                {
                    Debug.Log("Win");
                }
            }
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

        Collider2D existingBom = Physics2D.OverlapBox(position, Vector2.one / 2f, 0, LayerMask.GetMask("Bom"));
        if (existingBom != null)
        {
            yield break;
        }
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
        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0, effectLayer))
            return;

        Explosion explosion = Instantiate(effect, position, Quaternion.identity);
        explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end);
        explosion.SetDirection(direction);
        explosion.DestroyAfter(duration);

        Explode(position, direction, length - 1);
    }

    public void Update()
    {
        movement = movementDirection;

        anm.SetFloat("Horizontal", movement.x);
        anm.SetFloat("Vertical", movement.y);
        anm.SetFloat("Speed", movement.sqrMagnitude);
        CheckForObstacles();
    }

    public void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speedMove * Time.fixedDeltaTime);
    }

    private void CheckForObstacles()
    {
        Vector2 position = rb.position;
        Vector2 forward = new Vector2(movement.x, movement.y).normalized;
        Vector2 left = new Vector2(-movement.y, movement.x).normalized;
        Vector2 right = new Vector2(movement.y, -movement.x).normalized;
        float distance = 0.6f;

        RaycastHit2D hitForward = Physics2D.Raycast(new Vector2(rb.position.x, rb.position.y - 0.2f), forward, distance, obstacleLayer);
        RaycastHit2D hitLeft = Physics2D.Raycast(new Vector2(rb.position.x, rb.position.y - 0.2f), left, distance, obstacleLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(new Vector2(rb.position.x, rb.position.y - 0.2f), right, distance, obstacleLayer);
        if (hitForward.collider != null)
        {
            movementDirection = GetNewDirection(movementDirection);
            //PutBom();

        }
        if (hitForward.collider != null && hitForward.collider.gameObject.layer == LayerMask.NameToLayer("Brick")
            || hitLeft.collider != null && hitLeft.collider.gameObject.layer == LayerMask.NameToLayer("Brick")
            || hitRight.collider != null && hitRight.collider.gameObject.layer == LayerMask.NameToLayer("Brick"))
        {
            PutBom();
        }

        Debug.DrawRay(new Vector2(rb.position.x, rb.position.y - 0.2f), forward * distance, Color.red);
        Debug.DrawRay(new Vector2(rb.position.x, rb.position.y - 0.2f), left * distance, Color.red);
        Debug.DrawRay(new Vector2(rb.position.x, rb.position.y - 0.2f), right * distance, Color.red);
    }

    private Vector2 GetNewDirection(Vector2 currentDirection)
    {
        List<Vector2> newDirections = new List<Vector2>(randomDirections);
        newDirections.Remove(currentDirection);
        int randomIndex = Random.Range(0, newDirections.Count);
        return newDirections[randomIndex];
    }
}
