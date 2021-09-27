using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(BoundChecker))]
public class Projectile : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private BoundChecker _boundChecker;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boundChecker = GetComponent<BoundChecker>();
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
        gameObject.layer = color == ProjectileColor.Green ? GameData.Config.GreenProjectileLayer : GameData.Config.RedProjectileLayer;
        
        if (gameObject.layer == GameData.Config.GreenProjectileLayer)
        {
            _boundChecker.SetObjectType(OutOfBoundsObjectType.Teleportable);
            StartCoroutine(LifeTimer(lifeTime));
        }
        else
        {
            _boundChecker.SetObjectType(OutOfBoundsObjectType.Destroyable);
        }
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
        ObjectPooler.Instance.ReturnToPool(PoolType.Projectile, gameObject);
    }
}
