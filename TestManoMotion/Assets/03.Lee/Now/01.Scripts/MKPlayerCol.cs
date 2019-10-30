using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKPlayerCol : MonoBehaviour
{
    public GameObject deathEffect;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "SphereEnemy(Clone)")
        {
            MKManager.instance.EndGame();

            //Instantiate(deathEffect, transform.position, transform.rotation);
        }
    }

    private void OnParticleCollision()
    {
        MKManager.instance.EndGame();

        //Instantiate(deathEffect, transform.position, transform.rotation);
    }
}
