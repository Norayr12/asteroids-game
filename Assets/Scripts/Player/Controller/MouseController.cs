using UnityEngine;

public class MouseController : PlayerController
{
    protected override void Start()
    {
        base.Start();       
    }

    protected override void OnUpdate()
    {
        if (Input.GetKeyDown(GameData.ControlConfig.MouseShoot) || Input.GetKeyDown(GameData.ControlConfig.KeyboardShoot))
            Shoot();
    }

    protected override void OnFixedUpdate()
    {
        if (Input.GetKey(GameData.ControlConfig.MouseForward) || Input.GetKey(GameData.ControlConfig.KeyboardForward))
            AddForce();

        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 targetDirection = worldMousePos - transform.position;
        float angle = -Mathf.Atan2(targetDirection.x, targetDirection.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(Mathf.LerpAngle(transform.eulerAngles.z, angle, RotationSpeed * Time.fixedDeltaTime) , Vector3.forward);
    }
}
