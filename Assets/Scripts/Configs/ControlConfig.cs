using UnityEngine;


[CreateAssetMenu(fileName = "Controller", menuName = "Config/Controller")]
public class ControlConfig : ScriptableObject
{
    [Header("Player settings")]
    public float PlayerSpeed;
    public float PlayerMaxSpeed;
    public float PlayerRotationSpeed;

    [Header("Keyboard controller")]
    public KeyCode KeyboardForward;
    public KeyCode RotateRight;
    public KeyCode RotateLeft;
    public KeyCode KeyboardShoot;

    [Header("Mouse controller")]
    public KeyCode MouseForward;
    public KeyCode MouseShoot;
}
