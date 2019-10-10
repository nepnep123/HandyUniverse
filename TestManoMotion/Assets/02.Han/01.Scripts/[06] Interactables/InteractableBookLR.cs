using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBookLR : InteractableObject
{
    Book book;
    public LR leftRight = LR.Left;

    #region 베이스클래스 오버라이드
    public override void ProcessInit<T>(T book)
    {
        if (book is Book)
            this.book = book as Book;
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
    #endregion
}
