using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(ShootingController))]
public abstract class PlayerController : MonoBehaviour
{
    protected Rigidbody2D PlayerRigidbody;
    protected ShootingController ShootingController;

    protected float Speed;
    protected float MaxSpeed;
    protected float RotationSpeed;

    protected virtual void Awake()
    {
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        ShootingController = GetComponent<ShootingController>();
    }

    protected virtual void Start()
    {
        Speed = GameData.ControlConfig.PlayerSpeed;
        MaxSpeed = GameData.ControlConfig.PlayerMaxSpeed;
        RotationSpeed = GameData.ControlConfig.PlayerRotationSpeed;
    }

    public void Unsubscribe()
    {
        GameController.Instance.OnUpdate -= OnUpdate;
        GameController.Instance.OnFixedUpdate -= OnFixedUpdate;
    }

    public void Subscribe()
    {
        GameController.Instance.OnUpdate += OnUpdate;
        GameController.Instance.OnFixedUpdate += OnFixedUpdate;
    }

    protected void Shoot()
    {
        if(!GameController.Instance.IsStoped)
            ShootingController.Shoot();
    }

    protected void AddForce()
    {
        if (PlayerRigidbody.velocity.magnitude <= MaxSpeed)       
            PlayerRigidbody.AddForce(transform.up * Speed, ForceMode2D.Force);
        else      
            PlayerRigidbody.velocity = PlayerRigidbody.velocity.normalized * MaxSpeed;

    }

    protected void RotateLeft() => transform.Rotate(0, 0, 1 * RotationSpeed);

    protected void RotateRight() => transform.Rotate(0, 0, -1 * RotationSpeed);

    protected abstract void OnUpdate();

    protected abstract void OnFixedUpdate();   
}
