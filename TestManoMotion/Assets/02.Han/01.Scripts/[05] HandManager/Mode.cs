using UnityEngine;

//현재 Mode 클래스를 상속받는 클래스들 :
//EntryMode : 현실에서 책과의 상호작용
//MoonMode : 달에서의 상호작용
//VenusMode : 금성에서의 상호작용
public class Mode
{
    public PrimeHand hand;

    public Mode(PrimeHand hand) => this.hand = hand;
    public virtual void ModeChange(Mode mode) => hand.mode = mode;

    public virtual void OnTriggeredGrab() { }       //그랩
    public virtual void OnTriggeredRelease() { }    //릴리즈
    public virtual void OnTriggeredPick() { }       //픽
    public virtual void OnTriggeredDrop() { }       //드랍
    public virtual void OnTriggeredClick() { }      //클릭
}

public class EntryMode : Mode
{
	
    public EntryMode(PrimeHand hand):base(hand)
    {
        this.hand = hand;
    }

    public override void ModeChange(Mode mode)
    {
        base.ModeChange(mode);
    }

    public override void OnTriggeredGrab()
    {
		if (hand.curObj != null)
			hand.curObj.ProcessGrab();
	}
	public override void OnTriggeredPick()
	{
		if (hand.curObj != null)
			hand.curObj.ProcessPick();
	}
	public override void OnTriggeredDrop()
	{
		if (hand.curObj != null)
			hand.curObj.ProcessDrop();
	}
	public override void OnTriggeredRelease()
	{
		if (hand.curObj != null)
			hand.curObj.ProcessRelease();
	}
	public override void OnTriggeredClick()
	{
        if (hand.curObj != null)
            hand.curObj.ProcessClick();
	}
}

