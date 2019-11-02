using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    // Use this for initialization
    public float RotationSpeed = 1;


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime);
	}
}
