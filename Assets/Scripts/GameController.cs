using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public static Action OnGameStarted, OnAsteroidsDestroyed;

    [Header("Controllers")]
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private AsteroidsController _asteroidsController;
    [SerializeField] private UFOController _UFOController;

    [Space]

    [Header("Score for asteroids"), Tooltip("1 - Big, 2 - Medium, 3 - Small")]
    [SerializeField] private int[] _scoreAsteroidValues;
    [Header("Score for UFO")]
    [SerializeField] private int _scoreUFO;

    public int PlayerLifes { get; set; } = 3;

    public int PlayerScore { get; set; } = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        OnGameStarted?.Invoke();
    }

    public void ContactAsteroid(GameObject destroyedAsteroid, string collisionTag)
    {
        Asteroid asteroid = destroyedAsteroid.GetComponent<Asteroid>();
        AsteroidsController.ActiveAsteroids.Remove(asteroid);

        if (collisionTag != GameData.Config.ProjectileTag)
        {
            destroyedAsteroid.GetComponent<Asteroid>().AsteroidType = AsteroidType.Big;
            ObjectPooler.Instance.ReturnToPool(PoolType.Asteroid, destroyedAsteroid);         
        }
        else
        {
            _asteroidsController.SplitAsteroid(destroyedAsteroid);
            ObjectPooler.Instance.ReturnToPool(PoolType.Asteroid, destroyedAsteroid);                
        }
        destroyedAsteroid.GetComponent<Asteroid>().AsteroidType = AsteroidType.Big;


        if(AsteroidsController.ActiveAsteroids.Count == 0)
        {
            OnAsteroidsDestroyed?.Invoke();
        }

        IncreaseScore(_scoreAsteroidValues[(int)asteroid.AsteroidType]);
    }

    public void IncreaseScore(int value)
    {
        PlayerScore += value;
        UIManager.Instance.ShowScore(PlayerScore);
    }

    public void Pause()
    {
        
    }

    public void Resume()
    {

    }

    public void Restart()
    {

    }

    public void GameExit() => Application.Quit();
}
