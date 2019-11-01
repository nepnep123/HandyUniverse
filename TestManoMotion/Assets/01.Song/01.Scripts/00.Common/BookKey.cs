using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookKey : MonoBehaviour
{
	public static bool isBookKeyIn = false;

	public GameObject particle;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("KEYZONE"))
		{
			isBookKeyIn = true;
		}
	}


	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("KEYZONE"))
		{
			isBookKeyIn = false;
		}
	}

}
