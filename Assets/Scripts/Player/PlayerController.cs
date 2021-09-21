using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player constants")]
    [SerializeField] private float _forcePower;
    [SerializeField] private float _rotationSpeed;

    [Space]

    [Header("Input values")]
    [SerializeField] private KeyCode _forwardAcceleration;
    [SerializeField] private KeyCode _leftRotation;
    [SerializeField] private KeyCode _rightRotation;
    [SerializeField] private KeyCode _shooting;

    private Rigidbody2D _playerRigidbody;
    private ShootingController _shootingController;

    void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _shootingController = GetComponent<ShootingController>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(_forwardAcceleration))
            AddForce();

        if (Input.GetKey(_leftRotation))
            RotateLeft();
        else if (Input.GetKey(_rightRotation))
            RotateRight();
        else if (Input.GetKeyDown(_shooting))
            _shootingController.Shoot();

    }

    private void AddForce() => _playerRigidbody.AddForce(transform.up * _forcePower, ForceMode2D.Force);

    private void RotateLeft() => transform.Rotate(0, 0, 1 * _rotationSpeed);

    private void RotateRight() => transform.Rotate(0, 0, -1 * _rotationSpeed);

}
