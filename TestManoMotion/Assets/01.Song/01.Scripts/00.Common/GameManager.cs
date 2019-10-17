using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public Transform camPos;


	public Book_v2 masterBook;

	public PrimeHand hand;
	public ManoVisualization mano;
	public FadeChange fadeCanvas;
	
	private void Awake()
	{
		hand = FindObjectOfType<PrimeHand>();
		mano = FindObjectOfType<ManoVisualization>();
		fadeCanvas = FindObjectOfType<FadeChange>();

		if (instance == null) instance = GetComponent<GameManager>();

		else Destroy(this);

		camPos = Camera.main.transform;
	}
	
	public void BackGroundOn(bool temp)
	{
		GameManager.instance.mano.Show_background_layer = temp;
		GameManager.instance.mano._layer_background.gameObject.SetActive(temp);
		GameManager.instance.mano._layer_background.enabled = temp;
	}

	//World 나가는 메소드
	public IEnumerator ExitWorld()
	{
		fadeCanvas.FadeOut();
		masterBook.gameObject.SetActive(true);
		BackGroundOn(true);
		yield return new WaitForSeconds(3.0f);
		fadeCanvas.FadeIn();
	}

	//World 들어가는 메소드
	public IEnumerator EnterWorld()
	{
		fadeCanvas.FadeOut();
		masterBook.gameObject.SetActive(false);
		BackGroundOn(false);
		yield return new WaitForSeconds(3.0f);
		fadeCanvas.FadeIn();
	}
}
