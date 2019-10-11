using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterBookCreator : MonoBehaviour
{
    public Book_v2 masterBookPrefab;
	public GameObject bookBottom;
    public PageInfo_Scriptable[] pageInfos;

    private void Start()
    {
        Quaternion rot = Quaternion.AngleAxis(-30f, Vector3.right);
        var booker = Instantiate(masterBookPrefab, transform.position, rot);
		GameManager.instance.masterBook = booker;

		Quaternion rot2 = Quaternion.AngleAxis(-90f, Vector3.right);
		var bookBtn = Instantiate(bookBottom, transform.position, rot2);
		GameManager.instance.bookBottom = bookBtn;

		booker.InitBook("The Worlds");
        booker.GetComponent<BookPageSetter>().InitBookSetter(pageInfos);
    }
}
