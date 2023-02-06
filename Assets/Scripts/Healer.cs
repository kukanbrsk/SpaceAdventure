using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamagetbl>(out var damagetbl)&& damagetbl is Ship)
        {
            damagetbl.ChangeHealth(3);
        }
    }
}
