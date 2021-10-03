using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public event Action OnGameStarted, OnAsteroidsDestroyed, OnGameOver, OnGameRestart;
    public event Action OnUpdate;
    public event Action OnFixedUpdate;

    [Header("Controllers")]
    [SerializeField] private Player _player;
    [SerializeField] private AsteroidsController _asteroidsController;
    [SerializeField] private UFOController _UFOController;

    [Space]

    [Header("Score for asteroids"), Tooltip("1 - Big, 2 - Medium, 3 - Small")]
    [SerializeField] private int[] _scoreAsteroidValues;
    [Header("Score for UFO")]
    [SerializeField] private int _scoreUFO;

    public Player Player { get { return _player; } }

    public int PlayerLifes { get; set; }

    public int PlayerScore { get; set; } = 0;

    public ControllerType CurrentController { get; set; } = ControllerType.Keyboard;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        PlayerLifes = GameData.PlayerConfig.PlayerMaxLifes;
        StartGame();
    }

    private void Update()
    {
        OnUpdate?.Invoke();
    }

    private void FixedUpdate()
    {
        OnFixedUpdate?.Invoke();
    }

    public void StartGame()
    {
        OnGameStarted?.Invoke();
    }

    public void OnDestroyAsteroid(GameObject destroyedAsteroid, string collisionTag)
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

        if(AsteroidsController.ActiveAsteroids.Count == 0)
        {
            OnAsteroidsDestroyed?.Invoke();
        }

        IncreaseScore(_scoreAsteroidValues[(int)asteroid.AsteroidType]);
        destroyedAsteroid.GetComponent<Asteroid>().AsteroidType = AsteroidType.Big;

        AudioManager.Instance.PlaySound(GameData.SoundConfig.ExplosionSound);
    }

    public void OnDestroyPlayer()
    {
        DecreaseLife();
        if(PlayerLifes > 0)
            _player.PlayerRespawn();

        AudioManager.Instance.PlaySound(GameData.SoundConfig.ExplosionSound);
    }

    public void OnDestroyUFO(GameObject destroyed, string collisionTag)
    {
        if (collisionTag == GameData.Config.ProjectileTag)
            IncreaseScore(_scoreUFO);

        ObjectPooler.Instance.ReturnToPool(PoolType.UFO, destroyed);

        _UFOController.RespawnUFO();

        AudioManager.Instance.PlaySound(GameData.SoundConfig.ExplosionSound);
    }

    public void OnUfoOutOfBounds() => _UFOController.RespawnUFO();

    public void IncreaseScore(int value)
    {
        PlayerScore += value;
        UIManager.Instance.ShowScore(PlayerScore);
    }

    public void DecreaseLife()
    {
        if(--PlayerLifes <= 0)
        {
            Pause();
            AudioManager.Instance.StopEngine();
            OnGameOver?.Invoke();
        }

        UIManager.Instance.ShowLifes();
    }

    public void Pause()
    {
        AudioManager.Instance.StopEngine();
        Time.timeScale = 0;
    }

    public void Resume() => Time.timeScale = 1;

    public void Restart()
    {
        PlayerLifes = 3;
        PlayerScore = 0;
        OnGameRestart?.Invoke();
        StartGame();
    }

    public void ChangeController()
    {
        KeyboardController keyboard = _player.GetComponent<KeyboardController>();
        MouseController mouse = _player.GetComponent<MouseController>();

        if(CurrentController == ControllerType.Keyboard)
        {
            keyboard.Unsubscribe();
            mouse.Subscribe();
            CurrentController = ControllerType.Mouse;
        }
        else
        {
            mouse.Unsubscribe();
            keyboard.Subscribe();
            CurrentController = ControllerType.Keyboard;
        }
        UIManager.Instance.ChangeController(CurrentController);

    }

    public void GameExit() => Application.Quit();
}

public enum ControllerType
{
    Keyboard,
    Mouse
}