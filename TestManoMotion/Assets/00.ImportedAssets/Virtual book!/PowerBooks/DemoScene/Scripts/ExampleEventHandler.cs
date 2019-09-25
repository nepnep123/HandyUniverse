using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TLGFPowerBooks;

public class ExampleEventHandler : MonoBehaviour {

	public PBook pbook;
	public Color openBookColor;
	public Color willOpenBookColor;
	public Color closeBookColor;
	public Color willCloseBookColor;
	public Color lastPageColor;
	public Color firstPageColor;
	public Color enterLastPageColor;
	public Color enterFirstPageColor;


	void OnEnable () {
		PBook.OnBookOpened += OpenBookChangeColor;
		PBook.OnBookWillOpen += BookWillOpenChangeColor;
		PBook.OnBookClosed += CloseBookChangeColor;
		PBook.OnBookWillClose += BookWillCloseChangeColor;
		PBook.OnBookLastPage += LastPageChangeColor;
		PBook.OnBookFirstPage += FirstPageChangeColor;
		PBook.OnBookTurnToLastPage += TurnToLastPageChangeColor;
		PBook.OnBookTurnToFirstPage += TurnToFirstPageChangeColor;
	}

	void OnDisable () {
		PBook.OnBookOpened -= OpenBookChangeColor;
		PBook.OnBookWillOpen -= BookWillOpenChangeColor;
		PBook.OnBookClosed -= CloseBookChangeColor;
		PBook.OnBookWillClose -= BookWillCloseChangeColor;
		PBook.OnBookLastPage -= LastPageChangeColor;
		PBook.OnBookFirstPage -= FirstPageChangeColor;
		PBook.OnBookTurnToLastPage -= TurnToLastPageChangeColor;
		PBook.OnBookTurnToFirstPage -= TurnToFirstPageChangeColor;
	}

	void OpenBookChangeColor (GameObject sender) {
		if (pbook != null && sender == pbook.gameObject) {
			transform.GetComponent<Renderer> ().material.SetColor ("_Color", openBookColor);
		}
	}
	void BookWillOpenChangeColor (GameObject sender) {
		if (pbook != null && sender == pbook.gameObject) {
			transform.GetComponent<Renderer> ().material.SetColor ("_Color", willOpenBookColor);
		}
	}
	public void CloseBookChangeColor (GameObject sender) {
		if (pbook != null && sender == pbook.gameObject) {
			transform.GetComponent<Renderer> ().material.SetColor ("_Color", closeBookColor);
		}
	}
	void BookWillCloseChangeColor (GameObject sender) {
		if (pbook != null && sender == pbook.gameObject) {
			transform.GetComponent<Renderer> ().material.SetColor ("_Color", willCloseBookColor);
		}
	}
	void LastPageChangeColor (GameObject sender) {
		if (pbook != null && sender == pbook.gameObject) {
			transform.GetComponent<Renderer> ().material.SetColor ("_Color", lastPageColor);
		}
	}
	void FirstPageChangeColor (GameObject sender) {
		if (pbook != null && sender == pbook.gameObject) {
			transform.GetComponent<Renderer> ().material.SetColor ("_Color", firstPageColor);
		}
	}
	void TurnToLastPageChangeColor (GameObject sender) {
		if (pbook != null && sender == pbook.gameObject) {
			transform.GetComponent<Renderer> ().material.SetColor ("_Color", enterLastPageColor);
		}
	}
	void TurnToFirstPageChangeColor (GameObject sender) {
		if (pbook != null && sender == pbook.gameObject) {
			transform.GetComponent<Renderer> ().material.SetColor ("_Color", enterFirstPageColor);
		}
	}
}
