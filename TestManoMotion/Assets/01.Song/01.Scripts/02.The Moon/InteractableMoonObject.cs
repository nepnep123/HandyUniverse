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
		FinalPosMove,
		HandPrintZone,
	}

	public MoonSymbol symbol = MoonSymbol.FirstHint;

	public override void ProcessCollisionEnter()
	{
		switch (symbol)
		{
			case MoonSymbol.FirstHint:
				isTouched = true;
				break;
			case MoonSymbol.SecondHint:
				isTouched = true;
				break;


			case MoonSymbol.FirstPosMove:
				MoonSoundManager.instance.sfxPlayer.PlayOneShot(MoonSoundManager.instance.arrowTouchSound);
				MoonWorld.instance.GoFirstPos();
				break;
			case MoonSymbol.SecondPosMove:
				MoonSoundManager.instance.sfxPlayer.PlayOneShot(MoonSoundManager.instance.arrowTouchSound);
				MoonWorld.instance.GoSecondPos();
				break;


				//충돌되는순간 제스처를 통해서 이벤트 처리
			case MoonSymbol.HandPrintZone:
				SoundManager.instance.soundPlayer.PlayOneShot(SoundManager.instance.bookZone);
				canPrinted = true;
				break;


				//마지막 단계로 이동.
			case MoonSymbol.FinalPosMove:
				isTouched = true;
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
					MoonSoundManager.instance.sfxPlayer.PlayOneShot(MoonSoundManager.instance.clickSound);
					this.gameObject.SetActive(false);
					MoonUICtrl.instance.ShowPicture(1);
				}
				break;
			case MoonSymbol.SecondHint:
				if (isTouched == true)
				{	
					isTouched = false;
					MoonSoundManager.instance.sfxPlayer.PlayOneShot(MoonSoundManager.instance.clickSound);
					this.gameObject.SetActive(false);
					MoonUICtrl.instance.ShowPicture(2);
				}
				break;

			//포탈 나가는 마지막 단계 
			case MoonSymbol.FinalPosMove:
				if (isTouched == true)
				{
					isTouched = false;
					MoonSoundManager.instance.sfxPlayer.PlayOneShot(MoonSoundManager.instance.clickSound);
					this.gameObject.SetActive(false);
					//마지막 장소로 이동. 
					MoonWorld.instance.GoFinalPos();
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
