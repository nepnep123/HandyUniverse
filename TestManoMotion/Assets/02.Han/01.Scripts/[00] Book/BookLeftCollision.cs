using UnityEngine;

public class BookLeftCollision : MonoBehaviour, ICollidable
{
    public Book book { get; set; }
    public void InitCollData(Book book)
    {
        this.book = book;
    }
    
    public void ProcessCollision()
    {
        
    }
}
