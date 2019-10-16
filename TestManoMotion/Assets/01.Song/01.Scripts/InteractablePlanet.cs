using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePlanet : InteractableObject
{
	Book_v2 book;

	public bool isInit = false;
	public override void ProcessInit<T>(T book)
	{
		if (book is Book_v2)
			this.book = book as Book_v2;
		isInit = true;
	}
    /*
	//TEST
	private void OnEnable()
	{
		StartCoroutine(GrowPlanet());
	}*/

	//클릭했을때 페이지 행성으로 이동. 
	public override void ProcessClick()
	{
		GameManager.instance.hand.curObj.transform.SetParent(null);
		StartCoroutine(GrowPlanet());
	}

	IEnumerator GrowPlanet()
	{
		Vector3 targetPos = GameManager.instance.camPos.position + new Vector3(0.1f, -0.15f, 0.005f);
		float dis = Vector3.Distance(transform.position, targetPos);

		while (dis >= 0.2f)
		{
			dis = Vector3.Distance(transform.position, targetPos);

			//Lerp 특성상 0으로 정확하게 맞춰지진 않는다. 
			transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 0.2f);
			yield return null;
		}
		GameManager.instance.fadeCanvas.FadeOut();

		yield return new WaitForSeconds(3.0f);
		book.OpenPortal();
	}
}
