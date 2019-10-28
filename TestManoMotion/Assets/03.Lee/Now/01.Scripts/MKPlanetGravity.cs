using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKPlanetGravity : MonoBehaviour
{
    public static MKPlanetGravity instance;

    public SphereCollider planetSphereCollider;

    void Awake()
    {
        //
        if (instance == null) instance = GetComponent<MKPlanetGravity>();
        else Destroy(instance);

        planetSphereCollider = GetComponent<SphereCollider>();
    }
}
