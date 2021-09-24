using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player constants")]
    [SerializeField] private float _acceleration;
    [SerializeField] private float _maxSpeed;
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

    private void Update()
    {
        if (Input.GetKeyDown(_shooting))
            _shootingController.Shoot();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(_forwardAcceleration))
            AddForce();

        if (Input.GetKey(_leftRotation))
            RotateLeft();
        else if (Input.GetKey(_rightRotation))
            RotateRight();       
    }

    private void AddForce()
    {
        if (_playerRigidbody.velocity.magnitude <= _maxSpeed)
            _playerRigidbody.AddForce(transform.up * _acceleration, ForceMode2D.Force);
        else
            _playerRigidbody.velocity = _playerRigidbody.velocity.normalized * _maxSpeed;
    }

    private void RotateLeft() => transform.Rotate(0, 0, 1 * _rotationSpeed);

    private void RotateRight() => transform.Rotate(0, 0, -1 * _rotationSpeed);

}
