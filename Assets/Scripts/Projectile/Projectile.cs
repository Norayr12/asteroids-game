using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour //, IDestroyable
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();       
    }

    public void Initialize(ProjectileColor color, float lifeTime)
    {
        Sprite greenProjectile = GameData.Config.GreenProjectile;
        Sprite redProjectile = GameData.Config.RedProjectile;
        _spriteRenderer.sprite = color == ProjectileColor.Green ? greenProjectile : redProjectile;
        StartCoroutine(LifeTimer(lifeTime));
    }

    private IEnumerator LifeTimer(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        ObjectPooler.Instance.ReturnToPool(PoolType.Projectile, gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != GameData.Config.PlayerTag)
        {
            ObjectPooler.Instance.ReturnToPool(PoolType.Projectile, gameObject);
        }
    }

}
