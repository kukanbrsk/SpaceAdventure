using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Enemy : MonoBehaviour, IDamagetbl
{
    [SerializeField] private Image helthBar;
    [SerializeField] private float _maxHp;
    private float _currentHp;

    [SerializeField] private Transform pos;
    [SerializeField] private float speed = 1f;
    private static EnemyBulletPool enemyPool;

    private Vector3 _leftTargetPosition;
    private Vector3 _rightTargetPosition;
    private Vector3 _targetPosition;
    private float _indent = 3;

    private Action _moveState;
    private float _angle = 0;
    private float _angleSpeed = 0.02f;
    private int _radiusFactor = -1;
    [SerializeField] private float radiusMove = 2;
    [SerializeField] private EnemyState enemyState = EnemyState.Vertical;


    private void Start()
    {
        _leftTargetPosition = new Vector3(PoolManager.Singleton.LeftBorder, _indent);
        _rightTargetPosition = new Vector3(PoolManager.Singleton.RightBorder, _indent);
        _targetPosition = new Vector3(0, _indent + radiusMove);

    }

    private void Update()
    {
        _moveState?.Invoke();
    }

    public void ChangeHealth(float change)
    {
        _currentHp += change;
        if (_currentHp <= 0)
        {
            PoolManager.Singleton.RealiseEnemy(this);
            PoolManager.Singleton.GetExsplosion().transform.position = transform.position;
            PoolManager.Singleton.onScene = false;
        }
        helthBar.fillAmount = _currentHp / _maxHp;
    }


    private void OnEnable()
    {
        StartCoroutine(PauseMove());
        StartCoroutine(SwitchState());

        transform.position = new Vector3(0, 6, 0);
        _moveState = LinearMove;
        _currentHp = _maxHp;
        helthBar.fillAmount = 1;
        if (enemyPool == null)
        {
            enemyPool = FindObjectOfType<EnemyBulletPool>();
        }
        StartCoroutine(SpawnBullet());

    }
    IEnumerator SpawnBullet()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            enemyPool.Pool.Get().transform.position = pos.position;
        }
    }
    private void LinearMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, speed * Time.deltaTime);

        if (transform.position != _targetPosition) return;
        switch (enemyState)
        {
            case EnemyState.Horizontal:
                _targetPosition = _targetPosition == _leftTargetPosition ? _rightTargetPosition : _leftTargetPosition;
                break;
            case EnemyState.Vertical:
                // enemyState = EnemyState.Horizontal;
                //  _targetPosition = leftTargetPosition;
                _moveState = CircleMove;
                break;
        }
    }


    IEnumerator PauseMove()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(6, 12));
            _angleSpeed = 0;
            speed = 0;
            yield return new WaitForSeconds(UnityEngine.Random.Range(1, 1.5f));
            _angleSpeed = 0.02f;
            speed = 2;
        }
    }
    IEnumerator SwitchState()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(5, 10));

            if (_moveState == LinearMove)
            {
                enemyState = EnemyState.Vertical;
                radiusMove = Vector3.Distance(transform.position, new Vector3(0, _indent));
                _angle = -Vector2.SignedAngle(Vector2.up, transform.position - new Vector3(0, _indent)) - 90;
                _moveState = CircleMove;
            }
            else
            {
                enemyState = EnemyState.Horizontal;
                _moveState = LinearMove;
                _targetPosition = _leftTargetPosition;
            }
        }
    }

    private void CircleMove()
    {
        transform.position = new Vector3(Mathf.Sin(_angle) * 2, Mathf.Cos(-_angle) * radiusMove + _indent);
        radiusMove += 0.0005f * _radiusFactor;
        if (radiusMove >= 2)
        {
            _radiusFactor = -1;
        }
        else if (radiusMove <= 0.5f)
        {
            _radiusFactor = 1;
        }
        _angle += _angleSpeed;
    }
}


public enum EnemyState
{
    Horizontal,
    Vertical,
    Circle


}