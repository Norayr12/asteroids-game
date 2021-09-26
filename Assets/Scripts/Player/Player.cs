using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Player : MonoBehaviour
{    
    private Rigidbody2D _playerRigidbody;
    private Animator _animator;

    private Vector2 _startPos;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _startPos = transform.position;

        GameController.Instance.OnGameStarted += () =>
        {
            _animator.SetTrigger("Invulnerability");
        };

        GameController.Instance.OnGameOver += PlayerReset;
    }


    public void PlayerRespawn()
    {
        PlayerReset();
        _animator.SetTrigger("Invulnerability");
    }

    private void PlayerReset()
    {
        transform.rotation = Quaternion.identity;
        transform.position = _startPos;
        _playerRigidbody.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameController.Instance.ContactPlayer(collision.gameObject.tag);
    }

}
