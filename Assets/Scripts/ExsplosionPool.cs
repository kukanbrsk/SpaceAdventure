using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ExsplosionPool : MonoBehaviour
{
    [SerializeField] private ParticleSystem dust;
    private ObjectPool<ParticleSystem> _poolExsplosion;
    public ObjectPool<ParticleSystem> PoolExsplosion => _poolExsplosion;
    private WaitForSeconds _wait = new WaitForSeconds(1);

    private void Awake()
    {
        _poolExsplosion = new ObjectPool<ParticleSystem>(Create, OnGet, OnRealise, OnDestroyDust, false, 5, 10);
    }

    private ParticleSystem Create()
    {
        return Instantiate(dust);
    }

    private void OnGet(ParticleSystem obj)
    {
        obj.gameObject.SetActive(true);
        StartCoroutine(ReturnExsplosion(obj));
    }
    private IEnumerator ReturnExsplosion(ParticleSystem obj)
    {
        yield return _wait;
        PoolExsplosion.Release(obj);
    }

    private void OnRealise(ParticleSystem obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnDestroyDust(ParticleSystem obj)
    {
        Destroy(gameObject);
    }

   
}
