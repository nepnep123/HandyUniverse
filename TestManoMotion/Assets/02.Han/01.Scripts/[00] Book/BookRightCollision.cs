using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookRightCollision : MonoBehaviour, ICollidable
{
    Book book;
    public void InitCollData(Book book)
    {
        this.book = book;
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
}
