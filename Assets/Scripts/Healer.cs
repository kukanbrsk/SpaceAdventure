using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : BonusObject
{
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamagetbl>(out var damagetbl)&& damagetbl is Ship)
        {
            damagetbl.ChangeHealth(3);
            StartCoroutine(SpawnHeal());
        }
    }
 
}
