using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    static public TestManager instance;
    public Texture2D[] textures;
    public Book book;

    private void Awake()
    {
        instance = GetComponent<TestManager>();
        //book = Instantiate();
        book.InitBook(new FolderInfo(textures, "Proto"));
    }
}
