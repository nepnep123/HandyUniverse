using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableZone : InteractableObject
{
	Book_v2 book;

	public bool isInit = false;
	private bool isBookZone = false;

	public enum TriggerObj
	{
		CreateBook
	}
	public TriggerObj triobj = TriggerObj.CreateBook;

	public override void ProcessInit<T>(T book)
	{
		if (book is Book_v2)
			this.book = book as Book_v2;
		isInit = true;
	}

	//BOOKZONE을 생성하고 RELEASE 제스처로 마스터 책을 생성한다. 
	public override void ProcessRelease()
	{
		if (isBookZone == true && GameManager.instance.isCanCreateBook == true)
		{
			GameManager.instance.master.CreateMasterBook();
			Destroy(GameManager.instance.hand.curObj.gameObject.GetComponent<BoxCollider>());
			GameManager.instance.isCanCreateBook = false;
		}
	}

	public override void ProcessCollisionEnter()
	{
		switch (triobj)
		{
			case TriggerObj.CreateBook:
				isBookZone = true;
				break;
		}
	}

	public override void ProcessCollisionExit()
	{
		isBookZone = false;
	}

}
