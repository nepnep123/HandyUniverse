using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
    public static PlanetGravity instance;

    private SphereCollider planetSphereCollider;

    private float gravity = -0.02f;

    void Awake()
    {
        instance = this;
        planetSphereCollider = GetComponent<SphereCollider>();
    }

    public void Attract(Rigidbody rigidbody)
    {
        Vector3 gravityUp = (rigidbody.position - transform.position).normalized;
        rigidbody.AddForce(gravityUp * gravity);

        RotateBody(rigidbody);
    }

    public void PlaceOnSurface(Rigidbody rigidbody)
    {
        rigidbody.MovePosition((rigidbody.position - transform.position).normalized * (transform.localScale.x * planetSphereCollider.radius));

        RotateBody(rigidbody);
    }

    void RotateBody(Rigidbody rigidbody)
    {
        Vector3 gravityUp = (rigidbody.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.FromToRotation(rigidbody.transform.up, gravityUp) * rigidbody.rotation;
        rigidbody.MoveRotation(Quaternion.Slerp(rigidbody.rotation, targetRotation, 50f * Time.deltaTime));
    }
}
