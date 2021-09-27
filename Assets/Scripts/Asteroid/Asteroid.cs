using UnityEngine;

public class Asteroid : MonoBehaviour 
{   
    public AsteroidType AsteroidType { get; set; } = AsteroidType.Big;

    private void Start()
    {
        GameController.Instance.OnGameOver += OnGameOverRestart;
       
        GameController.Instance.OnGameRestart += OnGameOverRestart;
    }

    public void Initialize(Vector3 scale)
    {
        transform.localScale = scale;
    }

    private void OnGameOverRestart()
    {
        AsteroidType = AsteroidType.Big;
        if (!ObjectPooler.Instance.Exist(PoolType.Asteroid, gameObject))
            ObjectPooler.Instance.ReturnToPool(PoolType.Asteroid, gameObject);
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