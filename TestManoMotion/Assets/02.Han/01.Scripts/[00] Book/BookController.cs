using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookController : MonoBehaviour
{
    Book book;
    // Start is called before the first frame update
    void Awake()
    {
        book = GetComponent<Book>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            book.OpenBook();
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            book.CloseBook();
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            book.NextPage();
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            book.PrePage();
        }
    }
}
