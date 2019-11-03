using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KBullet : MonoBehaviour
{
    //public GameObject destroyParticle;

    [HideInInspector]
    public float getSpeed;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * getSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "DIEPLANET")
        {
            //Instantiate(destroyParticle, transform.position, transform.rotation);
            KWorld.instance.dieRealCount++;
            Destroy(gameObject);
        }
    }
}
