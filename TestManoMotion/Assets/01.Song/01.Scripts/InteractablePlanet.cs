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

	//클릭했을때 페이지 행성으로 이동. 
	public override void ProcessPick()
    {
        transform.SetParent(GameManager.instance.hand.transform);
    }

    public override void ProcessDrop()
    {
        transform.SetParent(null);
    }

    public override void ProcessClick()
	{
        transform.SetParent(null);
        if(book.isPlanetGrowing == false)
        {
            book.isPlanetGrowing = true;
            StartCoroutine(GrowPlanet());
        }
	}

	//충돌시 아웃라인 처리 
	public override void ProcessCollisionEnter()
	{
		GetComponent<Outline>().OutlineWidth = 10;
	}
	private void OnDisable()
	{
		GetComponent<Outline>().OutlineWidth = 0;
	}

	IEnumerator GrowPlanet()
	{
		Vector3 targetPos = GameManager.instance.camPos.position + new Vector3(0, 0, 0.005f);
		float dis = Vector3.Distance(transform.position, targetPos);

		while (dis >= 0.2f)
		{
			dis = Vector3.Distance(transform.position, targetPos);

			//Lerp 특성상 0으로 정확하게 맞춰지진 않는다. 
			transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 0.2f);
			yield return null;
		}
        book.isPlanetGrowing = false;
		book.OpenPortal();
	}
}
