using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKEnemyCol : MonoBehaviour
{
    public GameObject exp;
    private GameObject planet;

    //private PlanetGravity planetGravity;
    private Rigidbody rb;

    void Start()
    {
        //planetGravity = PlanetGravity.instance;

        rb = GetComponent<Rigidbody>();
        planet = GameObject.Find("SpherePlanetOriginal");
    }

    void FixedUpdate()
    {
        PlanetGravity.instance.Attract(rb);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "SpherePlanetOriginal")
        {
            Quaternion rot = Quaternion.LookRotation(transform.position.normalized);
            rot *= Quaternion.Euler(90f, 0f, 0f);
            var particle = Instantiate(exp, col.contacts[0].point, rot);
            particle.transform.SetParent(planet.transform);
            Destroy(particle, 3f);

            Destroy(gameObject, 0.2f);
        }
    }
}
