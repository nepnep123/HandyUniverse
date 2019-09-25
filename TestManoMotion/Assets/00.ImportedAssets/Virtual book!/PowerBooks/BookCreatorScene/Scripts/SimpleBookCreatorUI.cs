using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
using TLGFPowerBooks;

public class SimpleBookCreatorUI : MonoBehaviour {

	public SimpleBookCreator sbc;
	public GameObject loadingBar;
	public Image loadingBarPercent;
	public GameObject controlPanel;
	private bool loadingComplete;


	void Start () {
		if (sbc.convertMultipleFiles) {
			CreateMultipleBookPrefabs ();
		}
	}

	void Update () {
		loadingBarPercent.fillAmount = sbc.GetPercentComplete () / 100f;
		if (sbc.GetBookState () == SimpleBookCreator.BookState.OPEN && !loadingComplete) {
			loadingComplete = true;
			ShowControlUI ();
		}

		if (Input.GetAxis ("Horizontal") > 0) {
			sbc.NextPage ();
		}

		if (Input.GetAxis ("Horizontal") < 0) {
			sbc.PrevPage();
		}
	}

	private void ShowControlUI () {
		loadingBar.SetActive (false);
		controlPanel.SetActive (true);
	}

	public void CreateBookPrefab () {
		#if UNITY_EDITOR
		string prefabName = sbc.prefabName;
		if (prefabName != "") {
			GameObject go = sbc.CreateTextBookContent ();
			PrefabUtility.CreatePrefab ("Assets/PowerBooks/BookCreatorScene/SavedBookContent/" + prefabName + ".prefab", go);
			sbc.PrefabCreatedMessage ();
		} else {
			sbc.PrefabNotCreatedMessage();
		}
		#endif
	}

	public void CreateMultipleBookPrefabs () {
		StartCoroutine (CreateMultipleBooks());
	}

	private IEnumerator CreateMultipleBooks () {
		#if UNITY_EDITOR
		string path = "Assets/PowerBooks/BookCreatorScene/ConvertMultipleFiles/";
		foreach (string file in System.IO.Directory.GetFiles(path)) {
			if (!file.Contains (".meta")) {
				string text = System.IO.File.ReadAllText(file);
				sbc.CreateMultipleTextBooks(text);
				yield return new WaitUntil (() => sbc.convertState == SimpleBookCreator.ConvertState.READY);
				GameObject go = sbc.CreateTextBookContent ();
				string[] fileNameArray = file.Split("/"[0]);
				PrefabUtility.CreatePrefab ("Assets/PowerBooks/BookCreatorScene/SavedBookContent/" + fileNameArray[fileNameArray.Length-1].Split("."[0])[0] + ".prefab", go);
			}
		}
		yield return null;
		#endif
		yield return null;
	}
}
