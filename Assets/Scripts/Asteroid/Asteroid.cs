using UnityEngine;

public class Asteroid : MonoBehaviour 
{   
    public AsteroidType AsteroidType { get; set; } = AsteroidType.Big;

    private void Start()
    {
        GameController.Instance.OnGameOver += () =>
        {
            AsteroidType = AsteroidType.Big;
            ObjectPooler.Instance.ReturnToPool(PoolType.Asteroid, gameObject);
        };

        GameController.Instance.OnGameRestart += () =>
        {
            AsteroidType = AsteroidType.Big;
            ObjectPooler.Instance.ReturnToPool(PoolType.Asteroid, gameObject);
        };
    }

    public void Initialize(Vector3 scale)
    {
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameController.Instance.ContactAsteroid(gameObject, collision.gameObject.tag);
    }
}

public enum AsteroidType
{
    Big, 
    Medium,
    Small
}


[System.Serializable]
public class Range
{
    public float Min;
    public float Max;
}