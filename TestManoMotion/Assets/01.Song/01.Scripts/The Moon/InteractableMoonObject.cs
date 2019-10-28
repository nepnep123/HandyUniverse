using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableMoonObject : InteractableObject
{
	private bool isTouched = false;
	private bool canPrinted = false;
	private bool isGrab = false;

	public enum MoonSymbol
	{
		FirstHint,
		SecondHint,
		FirstPosMove,
		SecondPosMove,
		HandPrintZone,
		ExitPotal

	}

	public MoonSymbol symbol = MoonSymbol.FirstHint;

	public override void ProcessCollisionEnter()
	{
		switch (symbol)
		{
			case MoonSymbol.FirstHint:
				isTouched = true;
				//빌드전에 제거 필요
				//this.gameObject.transform.Translate(new Vector3(0, 0.3f, 0));
				//Destroy(this.gameObject);
				//MoonUICtrl.instance.ShowPicture(1);
				break;
			case MoonSymbol.SecondHint:
				isTouched = true;
				//빌드전에 제거 필요
				//this.gameObject.transform.Translate(new Vector3(0, 0.3f, 0));
				//Destroy(this.gameObject);
				//MoonUICtrl.instance.ShowPicture(2);
				break;
				///////////////////////////////////////////////////
			case MoonSymbol.FirstPosMove:
				MoonWorld.instance.GoFirstPos();
				break;
			case MoonSymbol.SecondPosMove:
				MoonWorld.instance.GoSecondPos();
				break;
				//충돌되는순간 제스처를 통해서 이벤트 처리
			case MoonSymbol.HandPrintZone:
				canPrinted = true;
				break;

				//손과 포탈과 충돌시 포탈을 타게된다. 
			case MoonSymbol.ExitPotal:
				//Book_v2에 구독하고있는 ClosePortal 실행. 
				GameManager.instance.masterBook.ClosePortal();

				Destroy(MoonWorld.instance.potalPrefab, 3.0f);
				break;
		}
	}
	public override void ProcessCollisionExit()
	{
		switch (symbol)
		{
			//이벤트 존에서만 가능하다. 
			case MoonSymbol.HandPrintZone:
				canPrinted = false;
				break;
		}
	}

	public override void ProcessClick()
	{
		switch (symbol)
		{
			case MoonSymbol.FirstHint:
				if (isTouched == true)
				{
					isTouched = false;
					Destroy(this.gameObject);
					MoonUICtrl.instance.ShowPicture(1);
				}
				break;
			case MoonSymbol.SecondHint:
				if (isTouched == true)
				{	
					isTouched = false;
					Destroy(this.gameObject);
					MoonUICtrl.instance.ShowPicture(2);
				}
				break;
		}
	}

	//손 프린팅 
	public override void ProcessGrab()
	{
		switch (symbol)
		{
			case MoonSymbol.HandPrintZone:
				if(canPrinted == true)
				{
					isGrab = true;
				}
				break;
		}
	}

	public override void ProcessRelease()
	{
		switch (symbol)
		{
			case MoonSymbol.HandPrintZone:
				if (isGrab == true)
				{
					//로딩 후 핸드프린팅
					StartCoroutine(MoonUICtrl.instance.LoadingAndPrint());
					isGrab = false;
				}
				break;
		}
	}

}
