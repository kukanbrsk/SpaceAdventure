using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpaceObject : MonoBehaviour,IDamagetbl
{
    [SerializeField] private float speed;
     protected float hpComet = 1;
     protected float rotationSpeed;

    void Update()
    {
        transform.Translate(Vector3.down * 2 * Time.deltaTime,Space.World);
        transform.Rotate(0,0,rotationSpeed * Time.deltaTime);
    }
    
    public virtual void ChangeHealth(float change)
    {
        hpComet += change;
    }
}