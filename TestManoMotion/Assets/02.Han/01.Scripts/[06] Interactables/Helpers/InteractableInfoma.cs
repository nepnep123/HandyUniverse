using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableInfoma : InteractableTrinity
{
    [SerializeField] Image lakshmi;
    [SerializeField] Image maxwell;
    public Image curInfo;
    public override void ProcessInit<T>(T obj)
    {
        base.ProcessInit(obj);
        lakshmi.gameObject.SetActive(false);
        maxwell.gameObject.SetActive(false);
        CloseInfos();
    }
    public override void ProcessPick()
    {
        isPicked = true;
        //오픈
        curInfo.gameObject.SetActive(true);
        Quaternion look = Quaternion.LookRotation(GameManager.instance.camPos.position - transform.position);
        transform.rotation = look;
        //StartCoroutine(Activer());
        helper.drone.releaseStack.Push(CloseInfos);
    }

    public void CloseInfos()
    {
        isPicked = false;
        curInfo.gameObject.SetActive(false);
    }

    public void ChangeInfos(LR lr)
    {
        if(lr == LR.Left)
            curInfo = lakshmi;
        else
            curInfo = maxwell;
    }

    void Update()
    {
        if (isPicked == false)
        {
            transform.Rotate(Vector3.up, 45f * Time.deltaTime, Space.World);
        }
        else
        {
            //Todo: 진ㅉ ㅏ플레이어를 바라보게
            //transform.LookAt(GameManager.instance.camPos);
            transform.LookAt(helper.target.position);
        }
    }
}
