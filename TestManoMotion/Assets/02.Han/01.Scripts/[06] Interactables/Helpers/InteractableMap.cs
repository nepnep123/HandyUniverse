using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableMap : InteractableTrinity
{
    [SerializeField] InteractableIndex[] indexes;
    [SerializeField] VenusPosition[] destis;
    CanvasGroup cg;


    #region 오버라이드 메서드
    public override void ProcessInit<T>(T obj)
    {
        base.ProcessInit(obj);
        //인덱스 초기화
        var a = GetComponentsInChildren<InteractableIndex>();
        indexes = new InteractableIndex[a.Length];
        for (int i = 0; i < a.Length; i++)
        {
            for (int j = 0; j < a.Length; j++)
            {
                if (i == (int)a[j].venusPos) indexes[i] = a[j];
            }
        }

        var b = helper.GetComponentInParent<VenusWorld>().transform.GetComponentsInChildren<VenusPosition>();
        if (b.Length != a.Length) Debug.LogError("인덱스 갯수와 비너스포지션 갯수가 맞지 않음");
        destis = new VenusPosition[b.Length];
        for (int i = 0; i < b.Length; i++)
        {
            for (int j = 0; j < b.Length; j++)
            {
                if (i == (int)b[j].venusPos) destis[i] = b[j];
            }
        }
        //인덱스들 일단 끄기
        cg = GetComponentInChildren<CanvasGroup>(true);
        IndexesOnOff(false);
    }
    //인덱스를 열기
    public override void ProcessPick()
    {
        if (helper.drone.releaseStack.Count > 1)
            helper.drone.ReturnBack();
        IndexesOnOff(true);
        helper.drone.releaseStack.Push(IndexesOff);
    }
    #endregion

    #region 인덱스들 관련 메서드들
    //인덱스들 끄기
    public void IndexesOff() => IndexesOnOff(false);
    //인덱스들 끄기/켜기
    void IndexesOnOff(bool booleana)
    {
        for (int i = 0; i < indexes.Length; i++)
        {
            indexes[i].gameObject.SetActive(booleana);
        }
    }

    //아웃라인 끄기
    public void OffAllOutline()
    {
        for (int i = 0; i < indexes.Length; i++)
        {
            indexes[i].OutlineOnOff(false);
        }
    }
    #endregion

    #region 날아가는 메서드들
    public void RequestFly(VenusPos venusPos) => StartCoroutine(SoarPlayer(venusPos));
    IEnumerator SoarPlayer(VenusPos venusPos)
    {
        float y = helper.arDevice.transform.position.y;
        while (y < 10f)
        {
            Debug.Log("Soaring...@@@@@@@@@@@");
            y += Time.deltaTime * 3f;
            Vector3 vect = new Vector3(helper.arDevice.transform.position.x, y, helper.arDevice.transform.position.z);
            helper.arDevice.transform.position = vect;
            yield return null;
        }
        Vector3 travelPos = new Vector3(destis[(int)venusPos].transform.position.x, 10f, destis[(int)venusPos].transform.position.z);
        float dist = Vector3.Distance(travelPos, helper.arDevice.transform.position);
        while (dist > 0.5f)
        {
            Debug.Log("Charging...@@@@@@@@@@@");
            Vector3 desti = Vector3.Lerp(helper.arDevice.transform.position, travelPos, 1.2f * Time.deltaTime);
            helper.arDevice.transform.position = desti;
            dist = Vector3.Distance(helper.arDevice.transform.position, travelPos);
            yield return null;
        }
        float finalY = 10f;
        float y2 = helper.arDevice.transform.position.y;
        while (finalY > 0.8f)
        {
            Debug.Log("Descending....@@@@@@@@@@@");
            y2 -= Time.deltaTime * 3f;
            Debug.Log(y2);
            Vector3 vect = new Vector3(helper.arDevice.transform.position.x, y2, helper.arDevice.transform.position.z);
            Vector3 vect2 = Vector3.Lerp(helper.arDevice.transform.position, vect, 1.2f * Time.deltaTime);
            helper.arDevice.transform.position = vect2;
            finalY = vect2.y;
            yield return null;
        }
        Debug.Log("끝났당");
    }

    #endregion
}
