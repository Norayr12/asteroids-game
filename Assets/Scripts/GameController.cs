using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [Header("Controllers")]
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private AsteroidsController _asteroidsController;
    [SerializeField] private UFOController _UFOController;

    private int _playerLifes = 3;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void SplitAsteroid(GameObject asteroid)
    {
        _asteroidsController.SplitAsteroid(asteroid);
        ObjectPooler.Instance.ReturnToPool(PoolType.Asteroid, asteroid);
        asteroid.GetComponent<Asteroid>().AsteroidType = AsteroidType.Big;   
    }
}
