using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
	//독수리가 목표지점에 도착했을때 생성되는 이벤트
	public GameObject particle;

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.CompareTag("POTAL01_EVENT"))
		{
			var a = Instantiate(particle, other.transform.position, other.transform.rotation);
			Destroy(a, 5.0f);
		}
	}
}
