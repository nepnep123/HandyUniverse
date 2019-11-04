using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KRotateSelf : MonoBehaviour {

    public float RotationSpeed = 1;

	void Update ()
    {
        transform.Rotate(Vector3.forward * RotationSpeed * Time.deltaTime);
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "BULLET")
        {
            //Instantiate(destroyParticle, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
