using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterBookCreator : MonoBehaviour
{
    public Book_v2 masterBookPrefab;

    public PageInfo_Scriptable[] pageInfos;


    public void CreateMasterBook()
    {
        Quaternion rot = Quaternion.AngleAxis(-30f, Vector3.right);

		//GameManager.instance.camPos.position + new Vector3(0, -0.5f, 1)
        var booker = Instantiate(masterBookPrefab, GameManager.instance.keyZone.transform.position, rot);

		GameManager.instance.masterBook = booker;
		booker.InitBook("Handy Universe");
        booker.GetComponent<BookPageSetter>().InitBookSetter(pageInfos);

		var msg = ">> 봉인된 공간이 해제되었습니다. <<" + "\n" + "\n" 
			+ "행성 여행을 하기 위한 마스터북이 활성화되었습니다.  " + "\n"
			+ "책을 터치하여 행성 페이지를 살펴 봅시다.";
		StartCoroutine(UIManager.instance.ShowMissionUI(msg));
	}
}
