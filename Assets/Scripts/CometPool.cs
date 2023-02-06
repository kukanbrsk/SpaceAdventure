using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CometPool : MonoBehaviour
{
    [SerializeField] private CometSmall comet;
    private ObjectPool<CometSmall> objectPool;
    public ObjectPool<CometSmall> PoolForComet => objectPool;

    private void Awake()
    {
        objectPool = new ObjectPool<CometSmall>(Create, OnGet, OnRelease, OnDestroyObj, false, 10, 50);
    }

    private CometSmall Create()
    {
        return Instantiate(comet);
    }

    private void OnGet(CometSmall obj)
    {
        obj.gameObject.SetActive(true);
    }

    private void OnRelease(CometSmall obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnDestroyObj(CometSmall obj)
    {
        Destroy(obj.gameObject);
    }

    
}
