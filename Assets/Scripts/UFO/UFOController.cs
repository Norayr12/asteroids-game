using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOController : MonoBehaviour
{
    [Header("UFO settings")]
    [SerializeField] private float _speed;
    [SerializeField] private float _respawnTime;
    [SerializeField, Range(0, 1)] private float _spawnOffset;

    private Coroutine _respawnTimer;

    private void Start()
    {
        GameController.Instance.OnGameStarted += OnGameStart;
        GameController.Instance.OnGameOver += OnGameOverRestart;
        GameController.Instance.OnGameRestart += OnGameOverRestart;
    }

    public void RespawnUFO() => _respawnTimer = StartCoroutine(Respawn()); 

    private void OnGameStart()
    {
        _respawnTimer = StartCoroutine(Respawn());
    }

    private void OnGameOverRestart()
    {
        StopCoroutine(_respawnTimer);
    }

    private void Spawn()
    {
        BoundType side = Random.Range(0, 10) > 5 ? BoundType.Right : BoundType.Left;
        GameObject ufoObject = ObjectPooler.Instance.GetFromPool(PoolType.UFO, CalculatePosition(side), CalculateRotation(side));
        UFO ufo = ufoObject.GetComponent<UFO>();
        ufo.ShootingOn();
        ufoObject.GetComponent<Rigidbody2D>().AddForce(side == BoundType.Left ? ufoObject.transform.right : -ufoObject.transform.right * _speed, ForceMode2D.Impulse);
    }

    private Vector2 CalculatePosition(BoundType side)
    {
        float topBound = Screen.height - Screen.height * _spawnOffset;
        float bottomBound = Screen.height * _spawnOffset;
        Vector2 rightSide = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Random.Range(bottomBound, topBound)));
        Vector2 leftSide = Camera.main.ScreenToWorldPoint(new Vector2(0, Random.Range(bottomBound, topBound)));

        return side == BoundType.Right ? rightSide : leftSide;
    }

    private Quaternion CalculateRotation(BoundType side)
    {
        return Quaternion.identity;        
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(_respawnTime);
        Spawn();
    }

    
}
