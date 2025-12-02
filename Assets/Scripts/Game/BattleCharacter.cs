using UnityEngine;
using System.Collections;

public class BattleCharacter : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite idleSprite;
    public Sprite attackSprite;
    public Sprite hurtSprite;
    public Sprite deadSprite;

    [Header("Stats")]
    public int maxHP = 10;
    public int hp;

    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        hp = maxHP;
        sr.sprite = idleSprite;
    }

    public IEnumerator Attack(BattleCharacter target, int damage)
    {
        // 1) mostrar ataque
        sr.sprite = attackSprite;
        yield return new WaitForSeconds(0.25f);

        // 2) aplicar daño al target
        yield return target.TakeDamage(damage);

        // 3) volver a idle
        sr.sprite = idleSprite;
    }

    public IEnumerator TakeDamage(int damage)
    {
        hp -= damage;

        // sprite de recibir daño
        sr.sprite = hurtSprite;
        yield return new WaitForSeconds(0.2f);

        if (hp <= 0)
        {
            hp = 0;
            sr.sprite = deadSprite != null ? deadSprite : hurtSprite;
            // aquí podrías disparar lógica de muerte
        }
        else
        {
            sr.sprite = idleSprite;
        }
    }
}
