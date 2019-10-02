using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookCreator : MonoBehaviour
{
    public World[] worlds;
    public Book bookPrefab;
    bool isInited = false;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    private void LateUpdate()
    {
        if(isInited == false)
        {
            CreateBook();
            isInited = true;
            Destroy(this);
        }
    }

    void CreateBook()
    {
        int num = worlds.Length;
        for (int i = 0; i < num; i++)
        {
            WorldInfo worldinfo = new WorldInfo(worlds[i]);
            PhotoUtils.MakeFolder(worldinfo.GetWorldName());
            List<Texture2D> textures = PhotoUtils.ReadTexturesInFolder(worldinfo.GetWorldName());
            float randin = Random.Range(0f, 2f);
            float randus = Random.Range(0f, 2f);
            Quaternion rot = Quaternion.AngleAxis(-30f, Vector3.right);
            var a = Instantiate(bookPrefab, transform.position + new Vector3(randin, -0.5f, randus), rot);
            a.InitBook(new FolderInfo(textures, worldinfo.GetWorldName()), worldinfo);
        }
    }
}
