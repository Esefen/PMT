using UnityEngine;
using System.Collections;


public class DoorCrossed : MonoBehaviour {


	public static int NombreDoor = 1;

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.name == "porte+")
		{
			Fading.blackening = 2;
		Door porte = collider.gameObject.GetComponent<Door>();
		if (porte != false) 
		{
			NombreDoor ++;
			string NameRoom = "Room0"+DoorCrossed.NombreDoor;
			Application.LoadLevel (NameRoom);
		}
		}
		else if (collider.gameObject.name == "porte-")
		{
				Door porte = collider.gameObject.GetComponent<Door>();
				if (porte != false) 
				{
					string NameRoom = "Room0"+DoorCrossed.NombreDoor;
					Application.LoadLevel (NameRoom);
				}
			}
		}
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
