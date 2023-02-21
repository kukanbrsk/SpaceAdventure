using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBonus : BonusObject
{
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Ship ship))
        {
            ship.BonusBullet(0.25f,5);
            StartCoroutine(SpawnHeal());
        }
    }
}
