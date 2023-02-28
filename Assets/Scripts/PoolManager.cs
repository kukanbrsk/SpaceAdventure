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
    [SerializeField] private EnemyPool enemyPool;
    public bool onScene = false;
    public float LeftBorder { get; private set; }
    public float RightBorder { get; private set; }
    public float Upper { get; private set; }
    public float Down { get; private set; }



    private void Awake()
    {
        onScene = false;
        Upper = Camera.main.transform.position.y + Camera.main.orthographicSize;
        LeftBorder = Camera.main.transform.position.x - Camera.main.aspect * Camera.main.orthographicSize;
        RightBorder = Camera.main.transform.position.x + Camera.main.aspect * Camera.main.orthographicSize;
        Down = Camera.main.transform.position.y - Camera.main.orthographicSize;

        Singleton = this;
    }
    void Start()
    {
        StartCoroutine(SpawnComet());
        GetEnemy();
    }

    public CometSmall GetComet() => cometPool.PoolForComet.Get();

    public void RealiseComet(CometSmall obj) => cometPool.PoolForComet.Release(obj);

    public ParticleSystem GetExsplosion() => exsplosionPool.PoolExsplosion.Get();

    public Enemy GetEnemy() => enemyPool.enemyPool.Get();

    public void RealiseEnemy(Enemy obj) => enemyPool.enemyPool.Release(obj);

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
