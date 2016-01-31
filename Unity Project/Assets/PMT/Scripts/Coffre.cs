using UnityEngine;
using System.Collections;

public class Coffre : MonoBehaviour {

	public GameObject leGlitter;

	PlayerBehavior lePlayerBehavior;
	bool isTriggered = false;

	// Use this for initialization
	void Start () {
		lePlayerBehavior = FindObjectOfType<PlayerBehavior>();
		leGlitter.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		Fading.blackening = 2;
		Debug.Log("Coffre");
		if(isTriggered || !other.CompareTag("Player") ){ return; }

		leGlitter.SetActive(true);
		//on chope le trésor => un level de plus pour le joueur
		lePlayerBehavior.levelUp();
		isTriggered = true ;
	}
}
