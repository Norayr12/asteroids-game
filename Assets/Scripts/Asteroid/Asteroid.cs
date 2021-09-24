using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour //, IDestroyable
{   
    public AsteroidType AsteroidType { get; set; } = AsteroidType.Big;

    public void Destroy()
    {
        throw new System.NotImplementedException();
    }

    public void Initialize(Vector3 scale)
    {
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameController.Instance.SplitAsteroid(gameObject);
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