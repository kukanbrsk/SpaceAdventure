using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Singleton;
    [SerializeField] private GameObject comet;
    [SerializeField] private CometPool cometPool;
    [SerializeField] private ExsplosionPool exsplosionPool;
    public float LeftBorder { get; private set; }
    public float RightBorder{get; private set;}
    public float Upper { get; private set; }
    

    private void Awake()
    {
        Upper = Camera.main.transform.position.y + Camera.main.orthographicSize;
        LeftBorder = Camera.main.transform.position.x - Camera.main.aspect * Camera.main.orthographicSize;
        RightBorder = Camera.main.transform.position.x + Camera.main.aspect * Camera.main.orthographicSize;

        Singleton = this;
    }
    void Start()
    {
        StartCoroutine(SpawnComet());
    }

    public CometSmall GetComet() => cometPool.PoolForComet.Get();
    public void RealiseComet(CometSmall obj) => cometPool.PoolForComet.Release(obj);
    public ParticleSystem GetExsplosion() => exsplosionPool.PoolExsplosion.Get();

    private void SpawnSpaceObject()
    {
       GetComet();
    }

    IEnumerator SpawnComet()
    {
        while (true)
        {
        yield return new WaitForSeconds(2);
            SpawnSpaceObject();
        }
    }
}
