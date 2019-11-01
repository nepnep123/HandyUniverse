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
		bookkeyScript.particle.SetActive(false);
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
				bookkeyScript.particle.SetActive(true);
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
				bookkeyScript.particle.SetActive(false);
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
						//미션 클리어(마스터 북 생성)
						Destroy(GameObject.FindWithTag("KEYZONE"));

						GameManager.instance.master.CreateMasterBook();
						SoundManager.instance.soundPlayer.PlayOneShot(SoundManager.instance.closebookSound);
						GameManager.instance.camPos.position = GameManager.instance.mainPos.position;

						Destroy(this.gameObject);

					}
				}
				break;
		}
	}

	

	public override void ProcessGrab()
	{
		if (isBookZone == true)
		{
			isGrab = true;
		}
	}

	//public GameObject bookkey_pre_particle;

	//BOOKZONE을 생성하고 RELEASE 제스처로 마스터 책을 생성한다. 
	public override void ProcessRelease()
	{
		if (isGrab == true)
		{
			isGrab = false;
			var msg = "MasterBook을 생성하기 위해서는 " + "\n"
				+ "옆에 존재하는 열쇠를 집어서 책을 열어야합니다. " + "\n" + "\n"
				+ "Pick & Drop을 사용해 열쇠를 집어 책을 엽니다. ";
			StartCoroutine(UIManager.instance.ShowMissionUI(msg));

			Destroy(GameManager.instance.zone.GetComponent<BoxCollider>());


			Instantiate(GameManager.instance.keyZone, GameManager.instance.zone.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
			var bookkey_pre = Instantiate(GameManager.instance.bookKey, GameManager.instance.zone.transform.position
				+ new Vector3(0.8f, 0.25f, 0f), Quaternion.identity);
			//bookkey_pre_particle = bookkey_pre.GetComponentInChildren<ParticleSystem>().gameObject;
		}

	}
}
