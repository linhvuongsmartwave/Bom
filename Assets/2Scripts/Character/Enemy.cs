using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public TypeEnemy typeEnemy;
    int bossHp = 3;
    public enum TypeEnemy
    {
        boss,
        enemy
    }

    [Header("Bom")]
    public GameObject bomPrefabs;
    public float bomFuseTime = 3f;
    public int bomRemaining;

    public LayerMask obstacleLayer;
    public Vector2 movementDirection = Vector2.right;
    Vector2[] randomDirections = new Vector2[] { Vector2.down, Vector2.left, Vector2.right, Vector2.up };

    public override void Start()
    {
        base.Start();
        bomRemaining = 1;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnEnemyDestroyed();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Const.effectPlayer))
        {
            if (typeEnemy == TypeEnemy.boss)
            {
                bossHp--;
                if (bossHp <= 0) Destroy(this.gameObject);
            }
            else Destroy(this.gameObject);
        }
    }

    public void PutBom()
    {
        if (bomRemaining > 0)
        {
            if (GameManager.Instance.isPause)
            {
                StartCoroutine(PlaceBom());
            }
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

        Destroy(bom);
        bomRemaining++;
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
        if (GameManager.Instance.isPause)
        {

            rb.MovePosition(rb.position + movement * speedMove * Time.fixedDeltaTime);
        }
    }

    private void CheckForObstacles()
    {
        Vector2 position = rb.position;
        Vector2 forward = new Vector2(movement.x, movement.y).normalized;
        Vector2 left = new Vector2(-movement.y, movement.x).normalized;
        Vector2 right = new Vector2(movement.y, -movement.x).normalized;
        float distance = 0.6f;

        RaycastHit2D hitForward = Physics2D.Raycast(new Vector2(rb.position.x, rb.position.y - 0.2f), forward, distance, obstacleLayer);
        if (hitForward.collider != null)
        {
            movementDirection = GetNewDirection(movementDirection);
            PutBom();
        }
    }

    private Vector2 GetNewDirection(Vector2 currentDirection)
    {
        List<Vector2> newDirections = new List<Vector2>(randomDirections);
        newDirections.Remove(currentDirection);
        int randomIndex = Random.Range(0, newDirections.Count);
        return newDirections[randomIndex];
    }
}