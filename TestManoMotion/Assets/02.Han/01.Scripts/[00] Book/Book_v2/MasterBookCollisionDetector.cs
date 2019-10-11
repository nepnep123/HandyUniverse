using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterBookCollisionDetector : MonoBehaviour, ICollidable
{
    public enum MbookLR { Left, Right }
    public MbookLR mbook = MbookLR.Left;
    Book_v2 book;
    public void InitCollData<T>(T book)
    {
        if(book is Book_v2)
        {
            this.book = book as Book_v2;
        }
    }

    public void ProcessCollision()
    {
        if(mbook == MbookLR.Left)
        {
            if(book.IsBookOpened == false)
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

    public void ProcessMethod()
    {
        //포탈 열기
        book.GetComponent<BookPageSetter>().OpenPortal();
    }
}
