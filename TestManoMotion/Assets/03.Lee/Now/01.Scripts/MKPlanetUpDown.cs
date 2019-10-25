using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKPlanetUpDown : MonoBehaviour
{
     void Update()
    {
        PlanetRotDown();
    }

    private void PlanetRotDown()
    {
        float plnaetRotDownSpeed = 150 * Time.deltaTime;
        transform.Rotate(Vector3.right * plnaetRotDownSpeed);
    }
}
