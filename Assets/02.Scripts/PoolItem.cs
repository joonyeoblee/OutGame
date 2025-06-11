using System;
using Redcode.Pools;
using UnityEngine;
public class PoolItem : MonoBehaviour, IPoolObject
{ 
    public PoolManager PoolManager;
    private void Start()
    {
        
    }
    public void ReturnToPoolAs<T>() where T : Component
    {
        PoolManager.TakeToPool<T>(GetComponent<T>());

        gameObject.SetActive(false);
    }
    public void OnCreatedInPool()
    {

    }
    public void OnGettingFromPool()
    {

    }
}
