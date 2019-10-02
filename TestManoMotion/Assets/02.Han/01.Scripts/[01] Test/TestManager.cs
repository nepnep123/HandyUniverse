using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    static public TestManager instance;
    public Texture2D[] textures;
    public Book book;

    bool isInited = false;

    private void Awake()
    {
        instance = GetComponent<TestManager>();
		book.InitBook(new FolderInfo(textures, "Proto"));
	}
    /*
    private void LateUpdate()
    {
        if(!isInited)
        {
            string[] folders = PhotoUtils.GetFolders();
            for(int i = 0;)
            var a = PhotoUtils.GetFolders();
            foreach (string b in a)
            {
                Debug.Log(b);
            }
            //book = Instantiate();
            book.InitBook(new FolderInfo(textures, "Proto"));
            isInited = true;
        }
    }*/
}
