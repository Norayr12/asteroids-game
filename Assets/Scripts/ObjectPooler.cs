using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    [Header("List of pooled objects")]
    [SerializeField] private List<Pool> _pools;

    private Dictionary<PoolType, Queue<GameObject>> _poolDictionary;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        _poolDictionary = new Dictionary<PoolType, Queue<GameObject>>();

        foreach (Pool pool in _pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.Size; i++)
            {
                GameObject pooledObject = Instantiate(pool.Prefabs[Random.Range(0, pool.Prefabs.Length)], pool.Parent);
                pooledObject.SetActive(false);

                objectPool.Enqueue(pooledObject);
            }

            _poolDictionary.Add(pool.Type, objectPool);
        }
    }

    public GameObject GetFromPool(PoolType type, Vector2 position, Quaternion rotation)
    {
        GameObject objectFromPool = _poolDictionary[type].Dequeue();

        objectFromPool.transform.position = position;
        objectFromPool.transform.rotation = rotation;
        objectFromPool.transform.parent = null;
        objectFromPool.SetActive(true);

        return objectFromPool;      
    }

    public void ReturnToPool(PoolType poolType, GameObject pooledObject)
    {
        if (poolType == PoolType.None)
            return;

        Pool current = _pools[0];
        foreach (var pool in _pools)
        {
            if (pool.Type == poolType)
            {
                current = pool;
                break;
            }
        }

        pooledObject.SetActive(false);
        pooledObject.transform.SetParent(current.Parent.transform);
        _poolDictionary[poolType].Enqueue(pooledObject);        
    }
}

[System.Serializable]
public class Pool
{
    public PoolType Type;
    public GameObject[] Prefabs;
    public Transform Parent;
    public int Size;    
}

public enum PoolType
{
    None,
    Projectile,
    Asteroid,
    UFO
}


