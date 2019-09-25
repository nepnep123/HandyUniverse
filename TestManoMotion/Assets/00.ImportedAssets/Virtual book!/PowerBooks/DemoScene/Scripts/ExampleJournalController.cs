using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TLGFPowerBooks;

public class ExampleJournalController : MonoBehaviour {


	public string overlayLayer;
	public Camera overlayCam;
	public float raycastDistance = 3.0f;
	[Range(0f,15f)]
	public float perspectiveAngle = 0f;
	public bool autoOpenBook = false;
	public KeyCode openCloseKey;
	public KeyCode nextPageKey;
	public KeyCode prevPageKey;
	public GameObject[] disabledGameObjectsWhileReading;
	public Camera useThisCameraInsteadOfMain = null;

	private Camera cam;
	private PBook activePowerBook = null;
	private Vector3 activePowerBookOriginalPos;
	private Quaternion activePowerBookOriginalRot;
	private LayerMask activePowerBookOriginalLayer;
	private bool bookIsOpen = false;


	// Use this for initialization
	void Start () {
		cam = Camera.main;
		activePowerBook = null;
		if (useThisCameraInsteadOfMain != null) {
			cam = useThisCameraInsteadOfMain;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!bookIsOpen) {
			if (Input.GetMouseButtonDown (0)) {
				RaycastHit hit;
				Ray ray = cam.ScreenPointToRay (Input.mousePosition);
				if (Physics.Raycast (ray, out hit, raycastDistance)) {
					if (hit.transform.parent != null && hit.transform.parent.GetComponent<PBook> () != null) {
						activePowerBook = hit.transform.parent.GetComponent<PBook> ();
						activePowerBookOriginalLayer = activePowerBook.gameObject.layer;
						SetLayer (activePowerBook.gameObject, LayerMask.NameToLayer (overlayLayer));
						BoxCollider bookCollider = (BoxCollider)hit.collider;
						activePowerBookOriginalPos = hit.transform.position;
						activePowerBookOriginalRot = hit.transform.rotation;
						activePowerBook.transform.position = new Vector3 ((bookCollider.size.y / 2) - 0.005f, 0, -bookCollider.size.z / 2);
						activePowerBook.transform.rotation = Quaternion.Euler (perspectiveAngle, 0, 0);
						overlayCam.transform.position = new Vector3 (0, ((bookCollider.size.z * 1.4f) - (bookCollider.size.y / 2)) - perspectiveAngle * 0.003f, -perspectiveAngle * 0.001f);
						overlayCam.gameObject.SetActive (true);
						if (autoOpenBook) {
							activePowerBook.OpenBook ();
						}
						bookIsOpen = true;

						foreach (GameObject go in disabledGameObjectsWhileReading) {
							go.SetActive (false);
						}
					}
				}
			}
		} else {
			if (Input.GetKey (openCloseKey)) {
				if (activePowerBook.GetBookState () == PBook.BookState.CLOSED) {
					activePowerBook.OpenBook ();
				}
				else if(activePowerBook.GetBookState () == PBook.BookState.OPEN) {
					CloseOverlay ();
				}
			}
			if (Input.GetKey (nextPageKey)) {
				activePowerBook.NextPage ();
			}
			if (Input.GetKey (prevPageKey)) {
				activePowerBook.PrevPage ();
			}
		}
	}

	public void CloseOverlay () {
		if (activePowerBook != null && bookIsOpen) {
			StartCoroutine (CloseOverlayAnim ());
		}
	}

	IEnumerator CloseOverlayAnim () {
		activePowerBook.CloseBook ();
		yield return new WaitUntil(() => activePowerBook.GetBookState() == PBook.BookState.CLOSED);
		activePowerBook.transform.position = activePowerBookOriginalPos;
		activePowerBook.transform.rotation = activePowerBookOriginalRot;
		SetLayer(activePowerBook.gameObject, activePowerBookOriginalLayer);
		overlayCam.gameObject.SetActive (false);
		bookIsOpen = false;
		foreach (GameObject go in disabledGameObjectsWhileReading) {
			go.SetActive (true);
		}
		yield return true;
	}

	public void SetLayer(GameObject parent, int layer) {
		parent.layer = layer;
		foreach (Transform t in parent.transform.GetComponentsInChildren<Transform>(true)) {
			t.gameObject.layer = layer;
		}
	}
}
