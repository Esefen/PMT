using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	public GameObject Play;
	public GameObject Credits;

	public bool onPlay = true;

	// Use this for initialization
	void Start () {
	
	}
		
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			Play.GetComponent<Image>().color = Color.yellow;
			Credits.GetComponent<Image>().color = Color.white;

			onPlay = true;
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			Credits.GetComponent<Image>().color = Color.yellow;
			Play.GetComponent<Image>().color = Color.white;

			onPlay = false;
		}

		if (onPlay == true)
		{
			Credits.GetComponent<Image>().color = Color.white;
			Play.GetComponent<Image>().color = Color.yellow;
		}
		else if (onPlay == false)
		{
			Credits.GetComponent<Image>().color = Color.yellow;
			Play.GetComponent<Image>().color = Color.white;
		}
	
		if (Input.GetKeyDown(KeyCode.Return))
		{
			if (onPlay == true)
				Application.LoadLevel("Room01");
			else if (onPlay == false)
				Application.LoadLevel("Credits");
		}

	}
}
