using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BonusObject : MonoBehaviour
{

    [SerializeField] private float speed;
    protected Coroutine reuseObject;
    private void FixedUpdate()
    {
        if (transform.position.y < PoolManager.Singleton.Down - 2)
        {
            StartCoroutine(SpawnHeal());
        }
    }

    void Start()
    {

    }
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime, 0);

    }
     protected IEnumerator SpawnHeal()
    {
        transform.position = new Vector3(Random.Range(PoolManager.Singleton.LeftBorder, PoolManager.Singleton.RightBorder), PoolManager.Singleton.Upper + 2, 0);
        var lastSpeed = speed;
        speed = 0;
        yield return new WaitForSeconds(Random.Range(12, 27));
        speed = lastSpeed;
    }
}
