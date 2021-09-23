using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Header("Asteroid settings")]
    [SerializeField] private Range _speedRange;
    [SerializeField] private float _rotationValue;

    private SpriteRenderer _spriteRenderer;
    private AsteroidType _asteroidType = AsteroidType.Big;

    public AsteroidType AsteroidType
    {
        get
        {
            return _asteroidType;
        }
        set
        {
            _asteroidType = value;
        }
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = GameData.Config.Shapes[Random.Range(0, GameData.Config.Shapes.Length - 1)];
    }

    public void Initialize(Vector3 scale)
    {

    }

}

[System.Serializable]
public class Range
{
    public float Min;
    public float Max;
}

public enum AsteroidType
{
    Big, 
    Medium,
    Small
}

