using UnityEngine;
using System.Collections;


public class DoorCrossed : MonoBehaviour {


	public static int NombreDoor = 1;


	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.name == "door+")
		{
            if (PlayerBehavior.player.isPoisoned)
                PlayerBehavior.player.isPoisoned = false;

			GameManager.roomsDone += 1;
			Fading.blackening = 2;
		Door porte = collider.gameObject.GetComponent<Door>();
		if (porte != false) 
		{
				if (GameManager.roomsDone == 10)
				{
					Application.LoadLevel("Win");
				}
				Boss.bossLevel += 1;
			NombreDoor ++;
			string NameRoom = "Room0"+DoorCrossed.NombreDoor;
			Application.LoadLevel (NameRoom);
		}
		}
		else if (collider.gameObject.name == "door-")
		{
            if (PlayerBehavior.player.isPoisoned)
                PlayerBehavior.player.isPoisoned = false;

			GameManager.roomsDone += 1;
			Fading.blackening = 2;
				Door porte = collider.gameObject.GetComponent<Door>();
				if (porte != false) 
				{
				if (GameManager.roomsDone == 10)
				{
					Application.LoadLevel("Win");
				}
					string NameRoom = "Room0"+DoorCrossed.NombreDoor;
					Application.LoadLevel (NameRoom);
				}
			}
	}

}
