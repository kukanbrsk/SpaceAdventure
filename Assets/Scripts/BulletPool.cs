using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    private ObjectPool<Bullet> pool;
    public ObjectPool<Bullet> Pool => pool;
    [SerializeField] private Transform pos;


    private void Awake()
    {
        pool = new ObjectPool<Bullet>(Create, OnGet, OnRelease, OnDestroyObj, false, 10, 50);
    }

    private Bullet Create()
    {
      var temp = Instantiate(bullet);
      temp.GoHome += pool.Release;
      return temp;
    }

    private void OnGet(Bullet obj)
    {
        obj.gameObject.SetActive(true);
    }

    private void OnRelease(Bullet obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnDestroyObj(Bullet obj)
    {
        Destroy(obj.gameObject);
    }
}
