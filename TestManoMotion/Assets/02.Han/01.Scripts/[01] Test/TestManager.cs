using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TestManager : MonoBehaviour
{
	public World[] worlds;
	public Book bookPrefab;
	public GameObject bookZone;

	public static GameObject azone;
	public static Book abook;

	private Transform camPos; 
	bool isInited = false;

	// Start is called before the first frame update
	void Awake()
	{
		camPos = Camera.main.transform;
	}

	private void LateUpdate()
	{
		if (Input.GetKeyDown(KeyCode.Z))
		{
			FindObjectOfType<PrimeHand>().mode.OnTriggeredRelease();
		}
		//if (isInited == false)
		//{
		//	CreateBook();
		//	isInited = true;
		//	Destroy(this);
		//}
	}

	void CreateBook()
	{
		int num = worlds.Length;
		for (int i = 0; i < num; i++)
		{
			WorldInfo worldinfo = new WorldInfo(worlds[i]);
			PhotoUtils.MakeFolder(worldinfo.GetWorldName());
			List<Texture2D> textures = PhotoUtils.ReadTexturesInFolder(worldinfo.GetWorldName());
			float randin = Random.Range(0f, 1f);
			float randus = Random.Range(0f, 1f);

			Quaternion rot = Quaternion.AngleAxis(-30f, Vector3.right);
			Quaternion zonrot = Quaternion.AngleAxis(-90f, Vector3.right);
			abook = Instantiate(bookPrefab, camPos.position + new Vector3(0, 0.05f, 0.3f), rot);
			azone = Instantiate(bookZone, camPos.position + new Vector3(0, -0.05f, 0), zonrot) ;

			abook.InitBook(new FolderInfo(textures, worldinfo.GetWorldName()), worldinfo);
		}
	}
}
