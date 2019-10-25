using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableMasterBook : InteractableObject
{
    Book_v2 book;

    public bool isInit = false;
    public override void ProcessInit<T>(T book)
    {
        if (book is Book_v2)
            this.book = book as Book_v2;
        isInit = true;
    }
}
