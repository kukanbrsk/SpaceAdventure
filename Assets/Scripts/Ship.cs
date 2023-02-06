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
    [SerializeField] private TextMeshProUGUI textHp;
    [SerializeField] Image healthBar;
    [SerializeField] ParticleSystem[] sparks;
    private int _maxHp = 6;
    private float _hp;

    void Start()
    {
        _hp = _maxHp;
        textHp.text = _hp.ToString();
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
        yield return new WaitForSeconds(0.5f);
        }
    }

    public void ChangeHealth(float change)
    {
        _hp += change;
        healthBar.fillAmount = _hp/_maxHp;
        textHp.text = _hp.ToString();
        if (_hp<_maxHp/2)
        {
            StartCoroutine(LowHp());
        }
        if (_hp<=0)
        {
            SceneManager.LoadScene(0);
        }
    }

    IEnumerator LowHp()
    {
        while (true)
        {
        yield return new WaitForSeconds(Random.Range(1,4));
        sparks[Random.Range(0, sparks.Length)].Play();

        }
    }
}
