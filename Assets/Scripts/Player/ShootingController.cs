using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [Header("Projectile constants")]
    [SerializeField] private ProjectileColor _color;
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    [SerializeField] private Transform _gunPoint;

    public void Shoot()
    {
        GameObject projectileObject = ObjectPooler.Instance.GetFromPool(PoolType.Projectile, _gunPoint.position, transform.rotation);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Initialize(_color, _lifeTime);
        projectileObject.GetComponent<Rigidbody2D>().AddForce(projectileObject.transform.up * _speed, ForceMode2D.Impulse);
    }
}

public enum ProjectileColor
{
    Green,
    Red
}

