using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KBullet : MonoBehaviour
{
    public GameObject destroyParticle;

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

    //void OnCollisionEnter(Collision col)
    //{
    //    if(col.gameObject.tag == "DIEPLANET")
    //    {
    //        KWorld.instance.dieRealCount++;
    //        var a = Instantiate(destroyParticle, transform.position, transform.rotation);
    //        Destroy(a, 2f);
    //        Destroy(gameObject);
    //    }
    //}

    void OnParticleCollision(GameObject col2)
    {
        if (col2.gameObject.tag == "DIEPLANET")
        {
            KWorld.instance.dieRealCount++;
            var a = Instantiate(destroyParticle, transform.position, transform.rotation);
            SoundManager.instance.soundPlayer.PlayOneShot(SoundManager.instance.fired);
            Destroy(a, 2f);
            Destroy(gameObject);
        }
    }
}
