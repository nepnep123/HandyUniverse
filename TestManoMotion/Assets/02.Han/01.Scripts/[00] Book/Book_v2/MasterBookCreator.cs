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

        var booker = Instantiate(masterBookPrefab, GameManager.instance.camPos.position + new Vector3(0, -0.5f, 1), rot);
		GameManager.instance.masterBook = booker;

		booker.InitBook("The Worlds");
        booker.GetComponent<BookPageSetter>().InitBookSetter(pageInfos);
    }
}
