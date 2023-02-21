using UnityEngine;
using UnityEngine.Pool;

public class EnemyBulletPool : MonoBehaviour
{
    private ObjectPool<Bullet> bulletPool;

    public ObjectPool<Bullet> Pool => bulletPool;

    [SerializeField] private Bullet bullet;


    private void Awake()
    {
        bulletPool = new ObjectPool<Bullet>(Create, OnGet, OnRelease, OnDestroyObj, false, 10, 50);
    }

    private Bullet Create()
    {
        var temp = Instantiate(bullet);
        temp.GoHome += bulletPool.Release;
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
