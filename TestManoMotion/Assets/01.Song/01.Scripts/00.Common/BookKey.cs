using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookKey : MonoBehaviour
{
	public static bool isBookKeyIn = false;

	public GameObject particle;
	public Outline outline;

	private void Start()
	{
		outline = gameObject.GetComponent<Outline>();
		particle.SetActive(false);
		outline.OutlineWidth = 0;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("KEYZONE"))
		{
			isBookKeyIn = true;
			particle.SetActive(true);
		}
	}


	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("KEYZONE"))
		{
			isBookKeyIn = false;
			particle.SetActive(false);
		}
	}

}
