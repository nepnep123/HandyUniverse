using UnityEngine;

public class Mode
{
    public PrimeHand hand;

    public Mode(PrimeHand hand)
    {
        this.hand = hand;
    }

    public virtual void ModeChange(Mode mode)
    {
        hand.mode = mode;
    }

    public virtual void OnHandCollideEnter(ICollidable col) { }      //충돌
    public virtual void OnHandCollideExit(ICollidable col) { }      //충돌
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

    public override void OnHandCollideEnter(ICollidable col)
    {
        col.ProcessCollision();
    }

    public override void OnTriggeredRelease()
    {
        Debug.Log("포탈 생성!!");
        hand.curObj.GetComponent<ICollidable>().ProcessMethod();
        Debug.Log("포탈생성 완료!");
    }

    public override void OnTriggeredGrab()
    {
        Debug.Log("EntryMode : Grab!@!");
    }
}
