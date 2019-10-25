using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterBookCreator : MonoBehaviour
{
    public Book_v2 masterBookPrefab;

    public Book bookPrefab;
	
    public PageInfo_Scriptable[] pageInfos;

    private void Start()
    {
        CreateMasterBook();
    }

    private void CreateMasterBook()
    {
        Quaternion rot = Quaternion.AngleAxis(-30f, Vector3.right);

        var booker = Instantiate(masterBookPrefab, transform.position, rot);
		GameManager.instance.masterBook = booker;

		//Quaternion rot2 = Quaternion.AngleAxis(-90f, Vector3.right);
		//var bookBtn = Instantiate(bookBottom, transform.position, rot2);
		//GameManager.instance.bookBottom = bookBtn;

		booker.InitBook("The Worlds");

        booker.GetComponent<BookPageSetter>().InitBookSetter(pageInfos);
    }

	//사용 X
    void CreateBook()
    {
        int num = pageInfos.Length;
        for (int i = 0; i < num; i++)
        {
            PhotoUtils.MakeFolder(pageInfos[i].world.name);
            List<Texture2D> textures = PhotoUtils.ReadTexturesInFolder(pageInfos[i].world.name);
            float randin = Random.Range(0f, 2f);
            float randus = Random.Range(0f, 2f);
            Quaternion rot = Quaternion.AngleAxis(-30f, Vector3.right);
            var a = Instantiate(bookPrefab, transform.position + new Vector3(randin, -0.5f, randus), rot);
            a.InitBook(new FolderInfo(textures, pageInfos[i].world.name));
        }
    }
}
