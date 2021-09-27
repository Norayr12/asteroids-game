using System.Collections.Generic;
using UnityEngine;

public class AsteroidsController : MonoBehaviour
{
    public static List<Asteroid> ActiveAsteroids;

    [Header("Asteroid settings")]
    [SerializeField] private List<Vector3> _asteroidScaleByType;
    [SerializeField] private Range _speedRange;

    private Dictionary<AsteroidType, Vector3> _scaleByTypeDictionary;

    public int RespawnedAsteroidsCount { get; set; } = 2;

    private void Start()
    {
        ActiveAsteroids = new List<Asteroid>();

        GameController.Instance.OnGameStarted += OnGameStart;
        GameController.Instance.OnAsteroidsDestroyed += OnAsteroidsDestroy;
        GameController.Instance.OnGameRestart += OnGameRestart;

        _scaleByTypeDictionary = new Dictionary<AsteroidType, Vector3>();
        for (int i = 0; i <= (int)AsteroidType.Small; i++)
            _scaleByTypeDictionary.Add((AsteroidType)i, _asteroidScaleByType[i]);
    }

    public void SplitAsteroid(GameObject asteroidObject)
    {
        Asteroid asteroid = asteroidObject.GetComponent<Asteroid>();
        ActiveAsteroids.Remove(asteroid);

        Quaternion leftAngle = Quaternion.Euler(0, 0, asteroidObject.transform.rotation.z - 45);
        Quaternion rightAngle = Quaternion.Euler(0, 0, asteroidObject.transform.rotation.z + 45);

        float newSpeed = Random.Range(_speedRange.Min, _speedRange.Max);

        if (asteroid.AsteroidType != AsteroidType.Small)
        {
            GameObject toLeft = ObjectPooler.Instance.GetFromPool(PoolType.Asteroid, asteroidObject.transform.position, leftAngle);
            Asteroid leftAsteroid = toLeft.GetComponent<Asteroid>();
            leftAsteroid.AsteroidType = asteroid.AsteroidType + 1;
            leftAsteroid.Initialize(_scaleByTypeDictionary[leftAsteroid.AsteroidType]);
            ActiveAsteroids.Add(leftAsteroid);

            GameObject toRight = ObjectPooler.Instance.GetFromPool(PoolType.Asteroid, asteroidObject.transform.position, rightAngle);
            Asteroid rightAsteroid = toRight.GetComponent<Asteroid>();
            rightAsteroid.AsteroidType = asteroid.AsteroidType + 1;
            rightAsteroid.Initialize(_scaleByTypeDictionary[rightAsteroid.AsteroidType]);
            ActiveAsteroids.Add(rightAsteroid);

            toLeft.GetComponent<Rigidbody2D>().AddForce(toLeft.transform.up * newSpeed, ForceMode2D.Impulse);
            toRight.GetComponent<Rigidbody2D>().AddForce(toRight.transform.up * newSpeed, ForceMode2D.Impulse);
        }
    }

    private void OnGameStart()
    {
        RespawnNewAsteroids(AsteroidType.Big, RespawnedAsteroidsCount);
    }

    private void OnGameRestart()
    {
        ActiveAsteroids = new List<Asteroid>();
        RespawnedAsteroidsCount = 2;
    }

    private void OnAsteroidsDestroy()
    {
        if (GameController.Instance.PlayerLifes > 0)
        {
            ++RespawnedAsteroidsCount;
            RespawnNewAsteroids(AsteroidType.Big, RespawnedAsteroidsCount);
        }
    }


    private void RespawnNewAsteroids(AsteroidType type, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject asteroidObject = ObjectPooler.Instance.GetFromPool(PoolType.Asteroid, CalculatePosition(), CalculateRotation());
            Asteroid asteroid = asteroidObject.GetComponent<Asteroid>();
            asteroid.Initialize(_scaleByTypeDictionary[asteroid.AsteroidType]);
            asteroidObject.GetComponent<Rigidbody2D>().AddForce(asteroidObject.transform.up * Random.Range(_speedRange.Min, _speedRange.Max), ForceMode2D.Impulse);
            ActiveAsteroids.Add(asteroid);
        }
    }

    private Vector2 CalculatePosition()
    {
        Vector2 result;
        int generated = Random.Range(0, 100);
        int randomXPos = Random.Range(-Screen.width / 2, Screen.width / 2);
        int randomYPos = Random.Range(-Screen.height / 2, Screen.height / 2);

        if (generated > 40)
            result = Camera.main.ScreenToWorldPoint(new Vector3(randomXPos, generated > 70 ? Screen.height : -Screen.height));
        else
            result = Camera.main.ScreenToWorldPoint(new Vector3(generated < 20 ? Screen.width : -Screen.width, randomYPos));           

        return result;
    }

    private Quaternion CalculateRotation()
    {
        return Quaternion.Euler(0, 0, Random.Range(0, 360));
    }
}

