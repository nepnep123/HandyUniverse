using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TLGFPowerBooks;

public class ExampleFPSBookController : MonoBehaviour {

	public LayerMask bookLayer;
	public float raycastDistance = 3.0f;
	public float raycastStartDistance = 0.0f;
	public KeyCode openCloseBookKey;
	public KeyCode nextPageKey;
	public KeyCode prevPageKey;
	public  Image pointer;
	private Transform camTr;
	private PBook activePowerBook = null;


	// Use this for initialization
	void Start () {
		camTr = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;

		pointer.color = Color.white;
		if (Physics.Raycast (camTr.position + (camTr.forward * raycastStartDistance), camTr.forward, out hit, raycastDistance, bookLayer.value)) {
			if ((hit.transform.parent != null && hit.transform.parent.GetComponent<PBook> () != null) || (hit.transform.parent.parent != null && hit.transform.parent.parent.GetComponent<PBook> () != null)) {
				pointer.color = Color.red;
			} 
		}

		if (Input.GetKeyDown (openCloseBookKey) && activePowerBook == null) {
			if (Physics.Raycast (camTr.position + (camTr.forward * raycastStartDistance), camTr.forward, out hit, raycastDistance, bookLayer.value)) {
				if (hit.transform.parent != null && hit.transform.parent.GetComponent<PBook> () != null) {
					activePowerBook = hit.transform.parent.GetComponent<PBook> ();
					if (activePowerBook.GetBookState () == PBook.BookState.CLOSED) {
						activePowerBook.OpenBook ();
					}
					activePowerBook = null;
				} 
			}

			if (Physics.Raycast (camTr.position + (camTr.forward * raycastStartDistance), camTr.forward, out hit, raycastDistance, bookLayer.value)) {
				if (hit.transform.parent.parent != null && hit.transform.parent.parent.GetComponent<PBook> () != null) {
					activePowerBook = hit.transform.parent.parent.GetComponent<PBook> ();
					if (activePowerBook.GetBookState () == PBook.BookState.OPEN) {
						activePowerBook.CloseBook ();
					}
					activePowerBook = null;
				} 
			}
		}

		if (Input.GetKeyDown (prevPageKey) && activePowerBook == null) {
			if (Physics.Raycast (camTr.position + (camTr.forward * raycastStartDistance), camTr.forward, out hit, raycastDistance, bookLayer.value)) {
				if (hit.transform.parent.parent != null && hit.transform.parent.parent.GetComponent<PBook> () != null) {
					activePowerBook = hit.transform.parent.parent.GetComponent<PBook> ();
					if (activePowerBook.GetBookState () == PBook.BookState.OPEN) {
						activePowerBook.PrevPage ();
					}
					activePowerBook = null;
				} 
			}
		}

		if (Input.GetKeyDown (nextPageKey) && activePowerBook == null) {
			if (Physics.Raycast (camTr.position + camTr.forward * raycastStartDistance, camTr.forward, out hit, raycastDistance, bookLayer.value)) {
				if (hit.transform.parent.parent != null && hit.transform.parent.parent.GetComponent<PBook> () != null) {
					activePowerBook = hit.transform.parent.parent.GetComponent<PBook> ();
					if (activePowerBook.GetBookState () == PBook.BookState.OPEN) {
						activePowerBook.NextPage ();
					}
					activePowerBook = null;
				} 
			} 
		}
	}
}
