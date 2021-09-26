using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    [Header("UFO settings")]
    [SerializeField] private float _speed;

    private void Start()
    {
        GameController.Instance.OnGameOver += () => ObjectPooler.Instance.ReturnToPool(PoolType.UFO, gameObject);
        GameController.Instance.OnGameRestart += () => ObjectPooler.Instance.ReturnToPool(PoolType.Projectile, gameObject);
    }
}
