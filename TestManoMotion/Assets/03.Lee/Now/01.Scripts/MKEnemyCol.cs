using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKEnemyCol : MonoBehaviour
{
    public GameObject exp;

    private Rigidbody rb;

    private float gravity = -0.02f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Attract(rb);
    }

    private void Attract(Rigidbody rigidbody)
    {
        Vector3 gravityUp = (rigidbody.position - MKManager.instance.planet.transform.position).normalized;
        rigidbody.AddForce(gravityUp * gravity);

        RotateBody(rigidbody);
    }

    void RotateBody(Rigidbody rigidbody)
    {
        Vector3 gravityUp = (rigidbody.position - MKManager.instance.planet.transform.position).normalized;
        Quaternion targetRotation = Quaternion.FromToRotation(rigidbody.transform.up, gravityUp) * rigidbody.rotation;
        rigidbody.MoveRotation(Quaternion.Slerp(rigidbody.rotation, targetRotation, 50f * Time.deltaTime));
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Planet")
        {
            Quaternion rot = Quaternion.LookRotation(transform.position.normalized);
            rot *= Quaternion.Euler(90f, 0f, 0f);
            var particle = Instantiate(exp, col.contacts[0].point, rot);
            particle.transform.SetParent(col.transform);
            Destroy(particle, 3f);

            Destroy(gameObject, 0.2f);
        }
    }
}
