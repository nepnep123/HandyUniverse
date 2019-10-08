using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasing : MonoBehaviour
{
	private Transform campos;
	public GameObject potal;

	private void Awake()
	{
		campos = Camera.main.transform;
	}

	// Start is called before the first frame update
	void Start()
    {
		StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
		//timer += Time.deltaTime;

		//if (timer <= 5.0f) return;

	}
	IEnumerator Timer()
	{
		yield return new WaitForSeconds(5.0f);
		float dis = Vector3.Distance(transform.position, campos.position);

		while (dis >= 0.1)
		{
			dis = Vector3.Distance(transform.position, campos.position);

			//Lerp 특성상 0으로 정확하게 맞춰지진 않는다. 
			transform.position = Vector3.Lerp(transform.position, campos.position - new Vector3(0, 0, 0.2f), Time.deltaTime * 2f);

			yield return null;

		}
		
		potal.SetActive(false);
		TestManager.azone.SetActive(false);
		TestManager.abook.gameObject.SetActive(false);
	}
}
