using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsController : MonoBehaviour
{
    [Header("Asteroid settings")]
    [SerializeField] private List<Vector3> _asteroidScaleByType;
    [SerializeField] private Range _speedRange;

    private Dictionary<AsteroidType, Vector3> _scaleByTypeDictionary = new Dictionary<AsteroidType, Vector3>();

    public int RespawnedAsteroidsCount { get; set; } = 1;
    public int AllAsteroidsCount { get; set; } = 2;

    private void Awake()
    {
        for (int i = 0; i <= (int)AsteroidType.Small; i++)
        {
            _scaleByTypeDictionary.Add((AsteroidType)i, _asteroidScaleByType[i]);
        }
    }

    private void Start()
    {
        RespawnNewAsteroids(AsteroidType.Big, RespawnedAsteroidsCount);
    }

    public void SplitAsteroid(GameObject asteroidObject)
    {
        Asteroid asteroid = asteroidObject.GetComponent<Asteroid>();
        --AllAsteroidsCount;

        Quaternion leftAngle = Quaternion.Euler(0, 0, asteroidObject.transform.rotation.z - 45);
        Quaternion rightAngle = Quaternion.Euler(0, 0, asteroidObject.transform.rotation.z + 45);

        float newSpeed = Random.Range(_speedRange.Min, _speedRange.Max);

        if (asteroid.AsteroidType != AsteroidType.Small)
        {
            AllAsteroidsCount += 2;

            GameObject toLeft = ObjectPooler.Instance.GetFromPool(PoolType.Asteroid, asteroidObject.transform.position, leftAngle);
            Asteroid leftAsteroid = toLeft.GetComponent<Asteroid>();
            leftAsteroid.AsteroidType = asteroid.AsteroidType + 1; 
            leftAsteroid.Initialize(_scaleByTypeDictionary[leftAsteroid.AsteroidType]);

            GameObject toRight = ObjectPooler.Instance.GetFromPool(PoolType.Asteroid, asteroidObject.transform.position, rightAngle);
            Asteroid rightAsteroid = toRight.GetComponent<Asteroid>();
            rightAsteroid.AsteroidType = asteroid.AsteroidType + 1;
            rightAsteroid.Initialize(_scaleByTypeDictionary[rightAsteroid.AsteroidType]);

            toLeft.GetComponent<Rigidbody2D>().AddForce(toLeft.transform.up * newSpeed, ForceMode2D.Impulse);
            toRight.GetComponent<Rigidbody2D>().AddForce(toRight.transform.up * newSpeed, ForceMode2D.Impulse);
        }
        else
            ObjectPooler.Instance.ReturnToPool(PoolType.Asteroid, asteroidObject);

    }

    private void RespawnNewAsteroids(AsteroidType type, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject asteroidObject = ObjectPooler.Instance.GetFromPool(PoolType.Asteroid, CalculatePosition(), CalculateRotation());
            Asteroid asteroid = asteroidObject.GetComponent<Asteroid>();
            asteroid.Initialize(_scaleByTypeDictionary[asteroid.AsteroidType]);
            asteroidObject.GetComponent<Rigidbody2D>().AddForce(asteroidObject.transform.up * Random.Range(_speedRange.Min, _speedRange.Max), ForceMode2D.Impulse);
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

