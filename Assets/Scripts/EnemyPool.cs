using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] Enemy enemyPrefab;
    private ObjectPool<Enemy> _enemyPool;
    public ObjectPool<Enemy> enemyPool => _enemyPool;

    private void Awake()
    {
        _enemyPool = new ObjectPool<Enemy>(Create, OnGet, OnRealise, DestroyEnemy, false, 5, 15);
    }

    private Enemy Create()
    {
        return Instantiate(enemyPrefab);
    }

    private void OnGet(Enemy obj)
    {
        obj.gameObject.SetActive(true);
    }

    private void OnRealise(Enemy obj)
    {
        obj.gameObject.SetActive(false);
        StartCoroutine(RespiteFromTheEnemy());
    }

    IEnumerator RespiteFromTheEnemy()
    {
        yield return new WaitForSeconds(7);
        enemyPool.Get();
    }

    private void DestroyEnemy(Enemy obj)
    {
        Destroy(obj);
    }


}
