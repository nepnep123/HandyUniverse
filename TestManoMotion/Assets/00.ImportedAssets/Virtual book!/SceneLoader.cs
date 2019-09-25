using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SceneLoader : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	//Vi laver nogle variable nogle private og nogle public så vi kan ændre den fra unity
	private bool gazedAt = false;
	public float gazeTime = 2f;
	private float timer = 0f;

	public bool skiftScene;
    // Start is called before the first frame update
    void Start()
    {
		skiftScene = false;
		//SetGazedAt = false;
    }

    // Update is called once per frame
    void Update()
    {
		//Vi laver et if statement der spørger om hvor lang tid man har kigget på objektet. Hvis man har kigget nok skifter man scene
		timer += Time.deltaTime;
		if (timer >= gazeTime)
		{
			if (gazedAt)
			{
			SceneManager.LoadScene(1);
				timer = 0f;
			}
    }
	}

	public void ss(string scenetochangeto)
	{
		gameObject.SetActive (true);
	}

	//tjekker om gazed at er true
	public void OnPointerEnter(PointerEventData eventData)
	{
		Debug.Log("pointer enter");
		gazedAt = true;
	}

	//tjekker om gazed at er false
	public void OnPointerExit(PointerEventData eventData)
	{
		Debug.Log("pointer exit");
		gazedAt = false;
	}
	/*public void SetGazedAt (bool gazedAt) {
		if(gazedAt)
		{
			SceneManager.LoadScene(1);
		}
	}*/
}
