using UnityEngine;

public class BookLeftCollision : MonoBehaviour, ICollidable
{
    public Book book { get; set; }

    public void InitCollData<T>(T book)
    {
        if (book is Book)
        {
            this.book = book as Book;
        }
    }

    public void ProcessCollision()
    {
        book.PrePage();
    }

    public void ProcessMethod()
    {
        //내용 없음
    }
}
