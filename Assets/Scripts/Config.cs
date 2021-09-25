using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "")]
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
