using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    [SerializeField]private float speed = 5f;
    private Rigidbody2D _rb;
    public Action<Bullet> GoHome;

    private BulletPool _bulletPool;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        
    }

    private void OnEnable()
    {
      _rb.AddForce(Vector2.up * speed, ForceMode2D.Impulse);
        Invoke(nameof(OnGoHome), 2f);
        
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnGoHome()
    {
        GoHome.Invoke(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IDamagetbl>(out var spaceObject))
        {
            spaceObject.ChangeHealth(-1);
            OnGoHome();
        }
    }
}
