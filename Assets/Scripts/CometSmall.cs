using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CometSmall : SpaceObject
{
    [SerializeField] private CircleCollider2D colComet;
    private float startRadius;
    private float factor;
    private const int UpperOffset = 2;

    private void Awake()
    {
        startRadius = colComet.radius;
        factor = 1 / startRadius;
    }

    private void OnEnable()
    {
        transform.position = new Vector3(UnityEngine.Random.Range(PoolManager.Singleton.LeftBorder, PoolManager.Singleton.RightBorder), PoolManager.Singleton.Upper + UpperOffset, 0);
        var rand = UnityEngine.Random.Range(1f, 2f);
        transform.localScale =Vector3.one * rand;
        colComet.radius = transform.localScale.x / factor;
        HpComet = (int)Mathf.Floor(rand * 2);
        rotationSpeed = UnityEngine.Random.Range(-50, 50f);
    }

    public override void ChangeHealth(float change)
    {
        base.ChangeHealth(change);
        if (HpComet == 0)
        {
             PoolManager.Singleton.RealiseComet(this);
             PoolManager.Singleton.GetExsplosion().transform.position = transform.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamagetbl damagetbl))
        {
            damagetbl.ChangeHealth(-1);
            ChangeHealth(-1);
        }
    }
}
