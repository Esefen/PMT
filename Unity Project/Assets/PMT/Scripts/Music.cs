using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {

	public AudioClip leSonDemon;
	public AudioClip leSonHero;

	AudioSource leHautparleur;
	static int currentSong;

	bool isDone = false;

	// Use this for initialization
	void Start () {
		leHautparleur = GameObject.Find("GameManager").GetComponent<AudioSource>() ;
			 

	}
	
	// Update is called once per frame
	void Update () {
		
		if (isDone) return ;
		Debug.Log("Music");
		if ( Boss.bossLevel > DoorCrossed.NombreDoor ){
			//if(currentSong = 1)

			leHautparleur.PlayOneShot (leSonDemon);
			currentSong = 1;
		} else if ( Boss.bossLevel < DoorCrossed.NombreDoor ){
			leHautparleur.PlayOneShot (leSonHero);
			currentSong = 2;
		} else {
			leHautparleur.PlayOneShot (leSonDemon);
			currentSong = 1;
		}
		isDone = true;

	}

}
