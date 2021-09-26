using UnityEngine;

[CreateAssetMenu(fileName = "Main", menuName = "Config/Main")]
public class Config : ScriptableObject
{
    [Header("Projectile types")]
    public Sprite GreenProjectile;
    public Sprite RedProjectile;

    [Space]

    [Header("Asteroid shapes")]
    public Sprite[] Shapes;

    [Space]

    [Header("Custom tags")]
    public string PlayerTag;
    public string ProjectileTag;
    public string AsteroidTag;
}
