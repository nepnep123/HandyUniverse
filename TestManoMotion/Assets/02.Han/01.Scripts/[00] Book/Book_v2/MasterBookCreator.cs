using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterBookCreator : MonoBehaviour
{
    public Book_v2 masterBookPrefab;
    public PageInfo_Scriptable[] pageInfos;

    private void Awake()
    {
        Quaternion rot = Quaternion.AngleAxis(-30f, Vector3.right);
        var booker = Instantiate(masterBookPrefab, transform.position, rot);
        booker.InitBook("The Worlds");
        booker.GetComponent<BookPageSetter>().InitBookSetter(pageInfos);
    }
}
