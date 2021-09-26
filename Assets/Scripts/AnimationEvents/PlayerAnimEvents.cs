using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerAnimEvents : MonoBehaviour
{
    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    public void InvulnerabilityOn () => _collider.enabled = false;

    public void InvulnerabilityOff() => _collider.enabled = true;
}
