using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour {


	private Transform tr;
	public Vector3 rotation;

	void Start () {
		tr = GetComponent<Transform> ();
	}
		
	void Update () {
		tr.Rotate (rotation * Time.deltaTime);	
	}
}
