using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ShootingController))]
public class UFO : MonoBehaviour
{
    [Header("UFO settings")]
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _cooldown;

    private ShootingController _shootingController;
    private Coroutine _shootTimer;
    
    private void Awake()
    {
        _shootingController = GetComponent<ShootingController>();
    }

    private void Start()
    {
        GameController.Instance.OnFixedUpdate += OnFixedUpdate;
        GameController.Instance.OnGameOver += OnGameOverRestart;
        GameController.Instance.OnGameRestart += OnGameOverRestart;        
    }

    public void ShootingOn()
    {
        _shootTimer = StartCoroutine(Shoot());
    }

    private void OnFixedUpdate()
    {
        Vector3 targetPosition = GameController.Instance.Player.gameObject.transform.position;

        Vector3 targetDirection = targetPosition - transform.position;
        float angle = -Mathf.Atan2(targetDirection.x, targetDirection.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(Mathf.LerpAngle(transform.eulerAngles.z, angle, _rotationSpeed * Time.fixedDeltaTime), Vector3.forward);
    }

    private void OnGameOverRestart()
    {
        if(!ObjectPooler.Instance.Exist(PoolType.UFO, gameObject))
            ObjectPooler.Instance.ReturnToPool(PoolType.UFO, gameObject);
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(_cooldown);
        _shootingController.Shoot();
        StartCoroutine(Shoot());
    }

    private void Cooldown()
    {
        StopCoroutine(_shootTimer);
        _shootTimer = StartCoroutine(Shoot());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameController.Instance.OnDestroyUFO(gameObject, collision.gameObject.tag);
    }
}
