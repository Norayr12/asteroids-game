using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(ProjectileColor color)
    {
        Sprite greenProjectile = GameData.Config.GreenProjectile;
        Sprite redProjectile = GameData.Config.RedProjectile;
        _spriteRenderer.sprite = color == ProjectileColor.Green ? greenProjectile : redProjectile;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
