using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookController_v2 : MonoBehaviour
{
    Book_v2 book;
    public bool isPicked = false;
    // Start is called before the first frame update
    void Awake()
    {
        book = GetComponent<Book_v2>();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Q))
        {
            book.OpenBook();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            book.CloseBook();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            book.NextPage();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            book.PrePage();
        }
#endif
        if (isPicked == true)
        {
            Lookplease();
        }
    }

    void Lookplease()
    {
        transform.LookAt(Camera.main.transform.position);
        transform.Rotate(-90f, 0, 180f);
    }
}
