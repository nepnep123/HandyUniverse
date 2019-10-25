using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableIndex : InteractableObject
{
    Image image;
    InteractableMap map;
    Helper helper;
    public VenusPos venusPos = VenusPos.Venus;
    public VenusPosition thisLand;
    public override void ProcessInit<T>(T obj)
    {
        image = GetComponent<Image>();
        if (obj is Helper)
        {
            helper = obj as Helper;
            map = helper.GetComponentInChildren<InteractableMap>(true);
            var b = helper.GetComponentInParent<VenusWorld>().transform.GetComponentsInChildren<VenusPosition>();
            for(int i = 0; i < b.Length; i++)
            {
                if(venusPos == b[i].venusPos)
                {
                    thisLand = b[i];
                }
            }
        }
    }
    //충돌, 아웃라인 켜기
    public override void ProcessCollisionEnter()
    {
        map.OffAllOutline();
        OutlineOnOff(true);
    }

    //픽, 해당 인덱스로 이동
    public override void ProcessPick()
    {
        Debug.Log("ProcessPicked from Index! " + this.name);
        helper.curPos = this.venusPos;          //헬퍼에게 우리가 여깄다고 알림
        //안에디터에서만
#if !UNITY_EDITOR
        GameManager.instance.hand.curObj = null;
#endif
        map.RequestFly(this.venusPos);
        helper.drone.ReturnBack();
    }

    public void OutlineOnOff(bool booleana)
    {
        if (booleana == true)
            image.color = Color.green;
        else
            image.color = Color.blue;
    }
}
