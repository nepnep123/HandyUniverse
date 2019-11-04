using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableZone : InteractableObject
{
	Book_v2 book;

	public bool isInit = false;
	private bool isBookZone = false;
	private bool canPick = false;
	private bool isGrab = false;

	private BookKey bookkeyScript;

	private void Awake()
	{
		bookkeyScript = FindObjectOfType<BookKey>();
		
	}
	private void OnEnable()
	{
		
	}


	public enum TriggerObj
	{
		CreateBook,
		BookKey
	}
	public TriggerObj triobj = TriggerObj.CreateBook;

	public override void ProcessInit<T>(T book)
	{
		if (book is Book_v2)
			this.book = book as Book_v2;
		isInit = true;
	}
	public override void ProcessCollisionEnter()
	{
		switch (triobj)
		{
			case TriggerObj.CreateBook:
				isBookZone = true;
				break;
			case TriggerObj.BookKey:
				canPick = true;
				bookkeyScript.outline.OutlineWidth = 10;
				this.gameObject.GetComponent<AutoRotate>().enabled = false;
				break;
		}
	}
	public override void ProcessCollisionExit()
	{
		switch (triobj)
		{
			case TriggerObj.CreateBook:
				isBookZone = false;
				break;
			case TriggerObj.BookKey:
				canPick = false;
				bookkeyScript.outline.OutlineWidth = 0;
				this.gameObject.GetComponent<AutoRotate>().enabled = true;
				break;
		}
	}

	public override void ProcessPick()
	{
		switch (triobj)
		{
			case TriggerObj.BookKey:
				if(canPick == true)
				{
					this.transform.SetParent(GameManager.instance.hand.transform);
				}
				break;
		}
	}



	public override void ProcessDrop()
	{
		switch (triobj)
		{
			case TriggerObj.BookKey:
				{ 
					this.transform.SetParent(null);
					canPick = false;
					//Heart와 MissionBook과 충돌해있는 상태에서 Drop 제스처 취할시 마스터책 발생
					if(BookKey.isBookKeyIn == true)
					{
						SoundManager.instance.soundPlayer.PlayOneShot(SoundManager.instance.keySound);
						Destroy(GameObject.FindWithTag("KEYZONE"));
						
						//미션 클리어(마스터 북 생성)
						GameManager.instance.master.CreateMasterBook();
						GameManager.instance.camPos.position = GameManager.instance.mainPos.position;

						Destroy(this.gameObject);
					}
				}
				break;
		}
	}


	//BOOKZONE을 생성하고 RELEASE 제스처로 마스터 책을 생성한다. 
	public override void ProcessGrab()
	{
		if (isBookZone == true)
		{
			isGrab = true;
		}
	}

	public override void ProcessRelease()
	{
		if(isGrab == true)
		{
			isGrab = false;
			isBookZone = false;
			var msg = ">> 봉인된 공간을 해제 하십시요 <<" + "\n" + "\n"
				+ "마스터북을 생성하기 위해서는 봉인된 공간을 " + "\n" 
				+ "열쇠를 사용해 해제 해야 합니다. " + "\n" + "\n" 
				+ "Pick & Drop 제스처를 사용해 미션을 완수합니다. ";
			StartCoroutine(UIManager.instance.ShowMissionUI(msg));

			Destroy(GameManager.instance.zone.GetComponent<BoxCollider>());

			GameManager.instance.camPos.position = GameManager.instance.mainPos.position;

			Instantiate(GameManager.instance.keyZone, GameManager.instance.zone.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
			Instantiate(GameManager.instance.bookKey, GameManager.instance.zone.transform.position
				+ new Vector3(0.8f, 0.25f, 0f), Quaternion.identity);
		}
	}
}
