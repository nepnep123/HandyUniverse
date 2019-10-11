using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;


	public Book_v2 masterBook;
	public GameObject bookBottom;


	
	private void Awake()
	{
		if (instance == null) instance = GetComponent<GameManager>();

		else Destroy(this);
	}

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
