using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableMasterBookLR : InteractableObject
{
    Book_v2 book;
    public LR leftRight = LR.Left;

    #region 베이스클래스 오버라이드
    public override void ProcessInit<T>(T book)
    {
        if(book is Book_v2)
            this.book =book as Book_v2;
    }

    public override void ProcessCollisionEnter()
    {
        if (leftRight == LR.Left)
        {
            if (book.IsBookOpened == false)
                book.OpenBook();
            else
                book.PrePage();
        }
        else
        {
            if (book.IsBookOpened == false)
                book.OpenBook();
            else
                book.NextPage();
        }
    }

    public override void ProcessRelease()
    {
        //book.OpenPortal();
    }
    #endregion
}
