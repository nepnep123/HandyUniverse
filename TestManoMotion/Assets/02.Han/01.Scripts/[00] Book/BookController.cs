using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookController : MonoBehaviour
{
    Book book;
    public bool isPicked = false;
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
        if(isPicked == true)
        {
            Lookplease();
        }

    }

    void Lookplease()
    {
        /*
        transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));
        Vector3 dir = -(Camera.main.transform.position - transform.position);
        Debug.DrawLine(transform.position, transform.position - dir * 2f);
        transform.rotation = Quaternion.LookRotation(dir, transform.up);
        Debug.Log(transform.rotation);
        Debug.Log(transform.rotation + "대체 왜");*/
        transform.LookAt(Camera.main.transform.position);
        transform.Rotate(-90f, 0, 180f);
    }
}
