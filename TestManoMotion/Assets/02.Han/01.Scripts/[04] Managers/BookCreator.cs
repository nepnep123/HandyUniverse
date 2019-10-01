using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookCreator : MonoBehaviour
{
    public static BookCreator instance;
    public Book bookPrefab;
    bool isInited = false;

    public IPortalFolder hello;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = GetComponent<BookCreator>();
        else
            Destroy(this);
    }
    private void LateUpdate()
    {
        if(isInited == false)
        {
            CreateBook();
            Destroy(this);
            isInited = true;
        }
    }

    void CreateBook()
    {
        string[] folderPathes = PhotoUtils.GetFolders();
        for(int i = 0; i < folderPathes.Length; i++)
        {
            var a = Instantiate(bookPrefab, transform.position + new Vector3(Random.Range(0f, 4f), 0, Random.Range(0f, 4f)), Quaternion.identity);
            Texture2D[] textures = PhotoUtils.ReadTexturesInFolder(folderPathes[i]);
            a.InitBook(new FolderInfo(textures, "Fuck"));
        }
    }
}
