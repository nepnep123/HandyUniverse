using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour {


	private Transform tr;
	private float y;
	private float speed = 20.0f;

	void Start () {
		tr = GetComponent<Transform> ();
	}
		
	void Update () {
		y += Time.deltaTime * speed;
		tr.rotation = Quaternion.Euler(0, y, 0);
	}
}
