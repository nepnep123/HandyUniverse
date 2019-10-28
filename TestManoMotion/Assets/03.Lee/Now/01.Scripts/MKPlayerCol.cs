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
            MKScoreManager.instance.enabled = false;
            MKManager.instance.EndGame();

            //Instantiate(deathEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }

    private void OnParticleCollision()
    {
        MKScoreManager.instance.enabled = false;
        MKManager.instance.EndGame();

        //Instantiate(deathEffect, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
