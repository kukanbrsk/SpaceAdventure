 using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Ship : MonoBehaviour,IDamagetbl
{
     private Camera cam;
    [SerializeField] private GameObject bulObj;
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private GameObject pos;

    [SerializeField] private Image healthBar;
    [SerializeField] private ParticleSystem[] sparks;
    private int _maxHp = 6;
    private float _hp;
    private Coroutine courutinEffects;
    private WaitForSeconds _rateOfFire = new WaitForSeconds(0.5f);

    void Start()
    {
        _hp = _maxHp;
        cam = Camera.main;
      StartCoroutine(Shot());

    }

    private void OnMouseDrag()
    {
        var mousePos = Input.mousePosition;
        var worldMousePos = cam.ScreenToWorldPoint(mousePos);
        worldMousePos.z = 0;
        transform.position = worldMousePos;
        
    }

    public Bullet GetBullet() => bulletPool.Pool.Get();

    public void Shooting()
    {
        GetBullet().transform.position = pos.transform.position ;
    }

    IEnumerator Shot()
    {
        while (true)
        {
        Shooting();
        yield return _rateOfFire;
        }
    }

    public void ChangeHealth(float change)
    {
        _hp += change;
        _hp = Mathf.Clamp(_hp, 0, _maxHp);
        healthBar.fillAmount = _hp/_maxHp;
        if (_hp<_maxHp/2&& courutinEffects == null)
        {
            courutinEffects = StartCoroutine(LowHp());
        }
        if (_hp<=0)
        {
            SceneManager.LoadScene(0);
        }
      
    }

    private IEnumerator LowHp()
    {
        while (_hp < _maxHp/2)
        {
        yield return new WaitForSeconds(Random.Range(1,4));
        sparks[Random.Range(0, sparks.Length)].Play();

        }
        courutinEffects = null;
    }
    public void BonusBullet (float newRate, float bonusTime)
    {
        StartCoroutine(BonusShooting(newRate,bonusTime));
    }

    private IEnumerator BonusShooting(float newRate, float bonusTime)
    {
       var currentRate = _rateOfFire;
        _rateOfFire = new WaitForSeconds(newRate);
        yield return new WaitForSeconds(bonusTime);
        _rateOfFire = currentRate;
    }
}
