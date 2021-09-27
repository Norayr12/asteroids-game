using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Projectile : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        GameController.Instance.OnGameOver += OnGameOverRestart;

        GameController.Instance.OnGameRestart += OnGameOverRestart;
    }

    public void Initialize(ProjectileColor color, float lifeTime)
    {
        Sprite greenProjectile = GameData.Config.GreenProjectile;
        Sprite redProjectile = GameData.Config.RedProjectile;
        _spriteRenderer.sprite = color == ProjectileColor.Green ? greenProjectile : redProjectile;
        StartCoroutine(LifeTimer(lifeTime));
    }


    private void OnGameOverRestart()
    {
        if (!ObjectPooler.Instance.Exist(PoolType.Projectile, gameObject))
            ObjectPooler.Instance.ReturnToPool(PoolType.Projectile, gameObject);
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
