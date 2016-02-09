using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public Text Play;
    public Text Credits;

	public bool onPlay = true;

	// Use this for initialization
	void Start () {
	
	}
		
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			Play.color = Color.yellow;
			Credits.color = Color.white;

			onPlay = true;
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			Credits.color = Color.yellow;
			Play.color = Color.white;

			onPlay = false;
		}

		if (onPlay == true)
		{
			Credits.color = Color.white;
			Play.color = Color.yellow;
		}
		else if (onPlay == false)
		{
			Credits.color = Color.yellow;
			Play.color = Color.white;
		}

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E))
		{
			if (onPlay == true){
				Debug.Log("LoadScene(\"Room01\")");
				// Application.LoadLevel("Room01");
				SceneManager.LoadScene("Room00");
			} else if (onPlay == false){
				Debug.Log("LoadScene(\"Credits\")");
				// Application.LoadLevel("Credits");
				SceneManager.LoadScene("Credits");
			}
		}

	}
}
