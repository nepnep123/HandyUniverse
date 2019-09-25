using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TLGFPowerBooks;

public class ExampleDragController : MonoBehaviour {

	public PBook pBook;
	public LayerMask bookLayer;
	public float raycastDistance = 3.0f;
	public Camera useThisCamera;

	private enum DragState {NODRAG,OPENDRAG,CLOSEDRAG,NEXTDRAG,PREVDRAG};
	private DragState dragstate = DragState.NODRAG;
	private Vector3 dragStartPosition;
	private Vector3 dragTargetPosition;
	private float maxDragDistance;
	private float dragValue;
	private Collider bookCollider;
	private float minDragDistance;
	private Camera cam;


	// Use this for initialization
	void Start () {
		if (useThisCamera != null) {
			cam = useThisCamera;
		} else {
			cam = Camera.main;
		}
		ResetMaxDragStartDistance ();
	}

	public void SetPBook (PBook book) {
		pBook = book;
		ResetMaxDragStartDistance ();
	}

	public void ResetMaxDragStartDistance () {
		bookCollider = pBook.transform.Find ("Book").GetComponent<Collider>();
		minDragDistance = bookCollider.bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (pBook != null) {
			RaycastHit hit;
			Ray ray = cam.ScreenPointToRay (Input.mousePosition);

			// Left Mouse Btn (Open Book / Next Page / Prev Page)
			if (Input.GetMouseButtonDown (0) && dragstate == DragState.NODRAG) {
				if (Physics.Raycast (ray, out hit, raycastDistance, bookLayer)) {
					// Open Book Start
					if (hit.transform.parent != null && hit.transform.parent.gameObject == pBook.gameObject) {
						dragStartPosition = hit.point;
						if (pBook.GetBookState () == PBook.BookState.CLOSED) {
							float distance = hit.transform.InverseTransformPoint (hit.point).x * 2.25f;
							distance = Mathf.Clamp (distance, minDragDistance, distance);
							dragTargetPosition = dragStartPosition - pBook.transform.right * distance;
							maxDragDistance = Mathf.Abs ((Input.mousePosition - cam.WorldToScreenPoint (dragTargetPosition)).x);
							dragstate = DragState.OPENDRAG;
						} 
					}
					// Next Page Start
					if (hit.transform.parent.parent != null && hit.transform.parent.parent.gameObject == pBook.gameObject && hit.transform.name == "PageCenterRight" && !pBook.IsLastPage ()) {
						dragStartPosition = hit.point;
						if (pBook.GetBookState () == PBook.BookState.OPEN) {
							float distance = Mathf.Abs (hit.transform.parent.parent.InverseTransformPoint (hit.point).x * 2.5f);
							distance = Mathf.Clamp (distance, minDragDistance, distance);
							dragTargetPosition = dragStartPosition - pBook.transform.right * distance;
							maxDragDistance = Mathf.Abs ((Input.mousePosition - cam.WorldToScreenPoint (dragTargetPosition)).x);
							dragstate = DragState.NEXTDRAG;
						} 
					}

					// Prev Page Start
					if (hit.transform.parent.parent != null && hit.transform.parent.parent.gameObject == pBook.gameObject && hit.transform.name == "PageCenterLeft" && !pBook.IsFirstPage ()) {
						dragStartPosition = hit.point;
						if (pBook.GetBookState () == PBook.BookState.OPEN) {
							float distance = Mathf.Abs (hit.transform.parent.parent.InverseTransformPoint (hit.point).x * 1.9f);
							distance = Mathf.Clamp (distance, minDragDistance, distance);
							dragTargetPosition = dragStartPosition + pBook.transform.right * distance;
							maxDragDistance = Mathf.Abs ((Input.mousePosition - cam.WorldToScreenPoint (dragTargetPosition)).x);
							dragstate = DragState.PREVDRAG;
						} 
					}
				}
			}

			// Right Mouse Btn (Close Book)
			if (Input.GetMouseButtonDown (1) && dragstate == DragState.NODRAG) {
				if (Physics.Raycast (ray, out hit, raycastDistance, bookLayer)) {
					// Close Book Start
					if (hit.transform.parent.parent != null && hit.transform.parent.parent.gameObject == pBook.gameObject && hit.transform.name == "PageCenterLeft") {
						dragStartPosition = hit.point;
						if (pBook.GetBookState () == PBook.BookState.OPEN) {
							float distance = Mathf.Abs (hit.transform.parent.parent.InverseTransformPoint (hit.point).x * 1.9f);
							distance = Mathf.Clamp (distance, minDragDistance, distance);
							dragTargetPosition = dragStartPosition + pBook.transform.right * distance;
							maxDragDistance = Mathf.Abs ((Input.mousePosition - cam.WorldToScreenPoint (dragTargetPosition)).x);
							dragstate = DragState.CLOSEDRAG;
						} 
					}
				}
			}

			// Open Book Drag / Next Page Drag / Prev Page Drag
			if (Input.GetMouseButton (0) && (dragstate == DragState.OPENDRAG || dragstate == DragState.NEXTDRAG || dragstate == DragState.PREVDRAG)) {
				float absDistance = Mathf.Abs ((Input.mousePosition - cam.WorldToScreenPoint (dragStartPosition)).x);
				float inputDistance = Mathf.Abs ((Input.mousePosition - cam.WorldToScreenPoint (dragTargetPosition)).x);
				dragValue = 1 - (1 / maxDragDistance * inputDistance);
				dragValue = Mathf.Clamp (dragValue, 0, 1);
				if (absDistance <= maxDragDistance && dragstate == DragState.OPENDRAG) {
					pBook.DragOpenBook (dragValue);
				}
				if (absDistance <= maxDragDistance && dragstate == DragState.NEXTDRAG) {
					pBook.DragNextPage (dragValue);
				}
				if (absDistance <= maxDragDistance && dragstate == DragState.PREVDRAG) {
					pBook.DragPrevPage (dragValue);
				}
			}

			// Close Book Drag
			if (Input.GetMouseButton (1) && dragstate == DragState.CLOSEDRAG) {
				float absDistance = Mathf.Abs ((Input.mousePosition - cam.WorldToScreenPoint (dragStartPosition)).x);
				float inputDistance = Mathf.Abs ((Input.mousePosition - cam.WorldToScreenPoint (dragTargetPosition)).x);
				dragValue = 1 - (1 / maxDragDistance * inputDistance);
				dragValue = Mathf.Clamp (dragValue, 0, 1);
				if (absDistance <= maxDragDistance) {
					pBook.DragCloseBook (dragValue);
				}
			}

			// Cancel Drag
			if (Input.GetMouseButtonUp (0) || Input.GetMouseButtonUp (1)) {
				CancelDrag ();
			}
			if (!Input.GetMouseButton (0) && !Input.GetMouseButton (1) && dragstate != DragState.NODRAG) {
				CancelDrag ();
			}
		}
	}

	// Cancel Drag
	private void CancelDrag () {
		if (pBook.GetBookState() != PBook.BookState.OPEN && pBook.GetBookState() != PBook.BookState.CLOSED) {
			if (dragstate == DragState.OPENDRAG || dragstate == DragState.CLOSEDRAG) {
				pBook.CancelDragOpenCloseBook ();
				dragstate = DragState.NODRAG;
			} else if (dragstate == DragState.NEXTDRAG || dragstate == DragState.PREVDRAG) {
				pBook.CancelDragNextPrevPage ();
				dragstate = DragState.NODRAG;
			}
		}
	}
}
