using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [Header("Projectile constants")]
    [SerializeField] private ProjectileColor _color;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _gunPoint;

    public void Shoot()
    {
        GameObject projectile = ObjectPooler.Instance.GetFromPool(PoolType.Projectile, _gunPoint.position, transform.rotation);
        projectile.GetComponent<Projectile>().Initialize(_color);
        projectile.GetComponent<Rigidbody2D>().AddForce(projectile.transform.up * _speed, ForceMode2D.Impulse);
    }
}

public enum ProjectileColor
{
    Green,
    Red
}

