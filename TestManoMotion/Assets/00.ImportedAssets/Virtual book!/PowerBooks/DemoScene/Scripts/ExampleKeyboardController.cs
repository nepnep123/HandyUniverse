using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TLGFPowerBooks;

public class ExampleKeyboardController : MonoBehaviour {

	public PBook pBook;
	public KeyCode openCloseKey = KeyCode.Space;
	public KeyCode nextPageKey = KeyCode.D;
	public KeyCode prevPageKey = KeyCode.A;
	public KeyCode gotoLastPageKey = KeyCode.E;
	public KeyCode gotoFirstPageKey = KeyCode.Q;
	public KeyCode jumpToLastPageKey = KeyCode.C;
	public KeyCode jumpToFirstPageKey = KeyCode.Y;
	public float gotoSpeed = 40f;
	public bool playSoundOnJump = true;


	void Update () {
		if (openCloseKey != KeyCode.None && Input.GetKeyDown (openCloseKey)) {
			if (pBook.GetBookState () == PBook.BookState.CLOSED) {
				pBook.OpenBook ();
			}
			if (pBook.GetBookState () == PBook.BookState.OPEN) {
				pBook.CloseBook ();
			}
		}

		if (nextPageKey != KeyCode.None && Input.GetKeyDown (nextPageKey)) {
			pBook.NextPage ();
		}

		if (prevPageKey != KeyCode.None && Input.GetKeyDown (prevPageKey)) {
			pBook.PrevPage ();
		}

		if (gotoLastPageKey != KeyCode.None && Input.GetKeyDown (gotoLastPageKey)) {
			pBook.GoToLastPage (gotoSpeed);
		}

		if (gotoFirstPageKey != KeyCode.None && Input.GetKeyDown (gotoFirstPageKey)) {
			pBook.GoToFirstPage (gotoSpeed);
		}

		if (jumpToLastPageKey != KeyCode.None && Input.GetKeyDown (jumpToLastPageKey)) {
			pBook.JumpToLastPage (playSoundOnJump);
		}

		if (gotoFirstPageKey != KeyCode.None && Input.GetKeyDown (jumpToFirstPageKey)) {
			pBook.JumpToFirstPage (playSoundOnJump);
		}
	}
}
