using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KRotateSelf : MonoBehaviour {

    public float RotationSpeed = 1;

	void Update ()
    {
        transform.Rotate(Vector3.forward * RotationSpeed * Time.deltaTime);
	}
}
