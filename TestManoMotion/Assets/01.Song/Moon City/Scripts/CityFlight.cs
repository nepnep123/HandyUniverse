using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityFlight : MonoBehaviour {

	

	void FixedUpdate()
	{
		
		if (Input.GetKey ("w"))
		{
			transform.position += transform.right;
		}
		if (Input.GetKey ("s"))
		{
			transform.position -= transform.right;
		}
		if (Input.GetKey ("a"))
		{
			transform.Rotate (Vector3.down * Time.deltaTime*10);
		}
		if (Input.GetKey ("d"))
		{
			transform.Rotate (Vector3.up * Time.deltaTime*10);
		}
		if (Input.GetKey ("up"))
		{
			transform.position += Vector3.up;
		}
		if (Input.GetKey ("down"))
		{
			transform.position += Vector3.down;
		}
	}
}
