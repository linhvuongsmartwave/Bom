using UnityEngine;

public class Explosion : MonoBehaviour
{
    public BomDataPlayer bomDataPlayer;
    public BomDataPlayer bomDataEnemy;
    public TypeAttack typeAttack;


    public enum TypeAttack
    {
        enemy,
        player
    }

    public AnimatedSpriteRenderer start;
    public AnimatedSpriteRenderer middle;
    public AnimatedSpriteRenderer end;

    public void SetActiveRenderer(AnimatedSpriteRenderer renderer)
    {
        start.enabled = renderer == start;
        middle.enabled = renderer == middle;
        end.enabled = renderer == end;
    }

    public void SetDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void DestroyAfter(float seconds)
    {
        Destroy(gameObject, seconds);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (typeAttack == TypeAttack.player  || typeAttack == TypeAttack.enemy)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Player player = collision.gameObject.GetComponent<Player>();
                if (player != null)
                {
                    if (typeAttack == TypeAttack.player)
                    {

                    player.heal(bomDataPlayer.damage);
                    }
                    else
                    {
                    player.heal(bomDataEnemy.damage);

                    }
                }
         
            }
        }

    }

}
