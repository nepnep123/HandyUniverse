using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TLGFPowerBooks;

public class ExampleUIController : MonoBehaviour {

	public PBook pBook;
	

	public void OpenBook () {
		pBook.OpenBook ();
	}

	public void CloseBook () {
		pBook.CloseBook ();
	}

	public void NextPage () {
		pBook.NextPage ();
	}

	public void PrevPage () {
		pBook.PrevPage ();
	}

	public void GoToLastPage () {
		pBook.GoToLastPage (50);
	}

	public void GoToFirstPage () {
		pBook.GoToFirstPage (50);
	}

	public void JumpToLastPage () {
		pBook.JumpToLastPage (true);
	}
    
    //public void JumptoPage (h) {
    //    pBook.JumptoPage (h,true);
    //}

	public void JumpToFirstPage () {
		pBook.JumpToFirstPage (true);
	}
}
