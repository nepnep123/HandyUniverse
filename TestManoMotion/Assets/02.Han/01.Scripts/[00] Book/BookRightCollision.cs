using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookRightCollision : MonoBehaviour, ICollidable
{
    public Book book { get; set; }
    public void InitCollData<T>(T book)
    {
        if(book is Book)
        {
            this.book = book as Book;
        }
    }

    public void ProcessCollision()
    {
        bool isOpened = book.IsBookOpened;
        //책이 열려있다면
        if (isOpened == true)
        {
            book.NextPage();
        }
        //책이 닫혀있다면
        else
        {
            book.OpenBook();
        }
    }

    public void ProcessMethod()
    {
        //내용 없음
    }
}
