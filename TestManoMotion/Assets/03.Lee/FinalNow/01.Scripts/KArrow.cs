using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KArrow : MonoBehaviour
{
    public Transform targetPlanet;

    void LateUpdate()
    {
        transform.position = targetPlanet.position + new Vector3(0f, 0.03f, 0f);
    }
}
