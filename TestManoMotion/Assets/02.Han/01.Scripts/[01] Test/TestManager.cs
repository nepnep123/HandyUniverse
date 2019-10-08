using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TestManager : MonoBehaviour
{
    static public TestManager instance;
    public Texture2D[] textures;
    public Book book;

    public Text testinas;
    public Text testlin;
    public Text testan;

    public Text testels;

    bool isInited = false;

    private void Awake()
    {
        //book.InitBook(new FolderInfo(textures, "Proto"));
        instance = GetComponent<TestManager>();
        var a = PhotoUtils.GetFolders();
        for(int i =0; i < a.Length; i++)
        {
            Debug.Log(Path.GetFileName(a[i]));
        }
        
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
