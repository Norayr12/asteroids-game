using UnityEngine;

public class KeyboardController : PlayerController
{
    protected override void Start()
    {
        base.Start();

        Subscribe();
    }

    protected override void OnUpdate()
    {
        if (Input.GetKeyDown(GameData.ControlConfig.KeyboardShoot))
            Shoot();
    }

    protected override void OnFixedUpdate()
    {
        if (Input.GetKey(GameData.ControlConfig.KeyboardForward))
        {         
            AddForce();
        }

        if (Input.GetKey(GameData.ControlConfig.RotateRight))
            RotateRight();        
        else if (Input.GetKey(GameData.ControlConfig.RotateLeft))
            RotateLeft();
    }
}
