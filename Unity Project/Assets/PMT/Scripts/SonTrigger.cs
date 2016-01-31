using UnityEngine;
using System.Collections;

public class SonTrigger : MonoBehaviour {

	public AudioClip leSon;
	AudioSource leHautparleur;

	// Use this for initialization
	void Start () {
		// leSon = gameObject.GetComponent<AudioSource>() ;
		leHautparleur = GameObject.Find("GameManager").GetComponent<AudioSource>() ;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void PlaySon(){
		//touched.

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			leHautparleur.PlayOneShot (leSon);


		}
	}

}
