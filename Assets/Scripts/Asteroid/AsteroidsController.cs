using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsController : MonoBehaviour
{
    [Header("Scale settings")]
    [SerializeField] private List<Vector3> _asteroidScaleByType;

    private Dictionary<AsteroidType, Vector3> _scaleByTypeDictionary;

    private void Awake()
    {
        for (int i = 0; i < (int)AsteroidType.Small; i++)
            _scaleByTypeDictionary.Add((AsteroidType)i, _asteroidScaleByType[i]);
    }

    private void Start()
    {
        SpawnNewAsteroid(AsteroidType.Big, 2);
    }

    public void SpawnNewAsteroid(AsteroidType type, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject asteroidObject = ObjectPooler.Instance.GetFromPool(PoolType.Asteroid, CalculatePosition(), Quaternion.identity);
            Asteroid asteroid = asteroidObject.GetComponent<Asteroid>();
            asteroid.Initialize(_scaleByTypeDictionary[asteroid.AsteroidType]);
            asteroidObject.GetComponent<Rigidbody2D>().AddForce(asteroidObject.transform.up, ForceMode2D.Impulse);
        }

    }

    public Vector2 CalculatePosition()
    {
        return new Vector2();
    }
}
