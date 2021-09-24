using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundChecker : MonoBehaviour
{
    [SerializeField] private OutOfBoundsObjectType _objectType;
    [SerializeField] private PoolType _poolType;

    private Vector2 _sceneSize;
    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _sceneSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (transform.position.x - _spriteRenderer.bounds.size.x / 2 > _sceneSize.x)
        {

            if (_objectType == OutOfBoundsObjectType.Teleportable)
                Teleport(BoundType.Right);
            else
                Destroy();
        }
        else if(transform.position.x + _spriteRenderer.bounds.size.x / 2 < -_sceneSize.x)
        {

            if (_objectType == OutOfBoundsObjectType.Teleportable)
                Teleport(BoundType.Left);
            else
                Destroy();
        }
        else if (transform.position.y - _spriteRenderer.bounds.size.y / 2 > _sceneSize.y)
        {

            if (_objectType == OutOfBoundsObjectType.Teleportable)
                Teleport(BoundType.Top);
            else
                Destroy();
        }
        else if (transform.position.y + _spriteRenderer.bounds.size.y / 2 < -_sceneSize.y)
        {

            if (_objectType == OutOfBoundsObjectType.Teleportable)
                Teleport(BoundType.Bottom);
            else
                Destroy();
        }

    }

    private void Teleport(BoundType boundType)
    {
        switch (boundType)
        {
            case BoundType.Top:
                transform.position -= new Vector3(0, _sceneSize.y * 2 + _spriteRenderer.bounds.size.y);
                break;

            case BoundType.Bottom:
                transform.position += new Vector3(0, _sceneSize.y * 2 + _spriteRenderer.bounds.size.y);
                break;

            case BoundType.Left:
                transform.position += new Vector3(_sceneSize.x * 2 + _spriteRenderer.bounds.size.x, 0);
                break;

            case BoundType.Right:
                transform.position -= new Vector3(_sceneSize.x * 2 + _spriteRenderer.bounds.size.x, 0);
                break;
        }
    }

    private void Destroy()
    {
        ObjectPooler.Instance.ReturnToPool(_poolType, gameObject);
    }
}

public enum OutOfBoundsObjectType
{ 
    Teleportable,
    Destroyable
}

public enum BoundType
{
    Top,
    Bottom,
    Left,
    Right
}

