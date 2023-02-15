using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour,IDamagetbl
{
    [SerializeField]private float _maxHp;
    [SerializeField] private Image helthBar;
    [SerializeField] private Transform pos;
    private float _currentHp;
    private static EnemyBulletPool enemyPool;
    private float _speed = 1f;
    private Vector3 leftTargetPosition = new Vector3(-3, 3, 0);
    private Vector3 rightTargetPosition = new Vector3(3,3,0);
    private Vector3 _targetPosition = new Vector3(0,3,0);
    private bool isLeft = true;
    [SerializeField] private EnemyState enemyState = EnemyState.Vertical;

    private void Start()
    {
        
    }
    public void ChangeHealth(float change)
    {
        _currentHp += change;
        if (_currentHp<=0)
        {
            Destroy(gameObject);
        }
        helthBar.fillAmount = _currentHp / _maxHp;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
        /*   if (transform.position == _targetPosition&&isLeft)
           {
               _targetPosition=leftTargetPosition;
               isLeft = false;
           }
           if (transform.position == _targetPosition &&!isLeft)
           {
               _targetPosition = rightTargetPosition;
               isLeft = true;
           }*/

        if (transform.position != _targetPosition) return;
        switch (enemyState)
        {
            case EnemyState.Horizontal:
                    _targetPosition = _targetPosition == leftTargetPosition ? rightTargetPosition : leftTargetPosition;
                break;
            case EnemyState.Vertical:
                    enemyState = EnemyState.Horizontal;
                    _targetPosition = leftTargetPosition;
                break;
            
        }
    }

    private void OnEnable()
    {
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
        yield return new WaitForSeconds(1);
            enemyPool.Pool.Get().transform.position = pos.position;
        }
    }


}

public enum EnemyState
{
    Horizontal,
    Vertical
       

}