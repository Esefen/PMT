using UnityEngine;
using System.Collections;


public class DoorCrossed : MonoBehaviour {


	public static int NombreDoor = 1;

	void OnTriggerEnter2D(Collider2D collider)
	{

		Door porte = collider.gameObject.GetComponent<Door>();
		if (porte != false) 
		{
			
			//transform.position = new Vector3 (-10, transform.position.y, transform.position.z);
			NombreDoor ++;
			string NameRoom = "Room0"+DoorCrossed.NombreDoor;
			Application.LoadLevel (NameRoom);

			

		}
	}
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
