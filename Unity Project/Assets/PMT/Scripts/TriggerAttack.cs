using UnityEngine;
using System.Collections;

public class TriggerAttack : MonoBehaviour {
	[Tooltip("Soit mettre les bullets en fils du trigger, soit les mettre dans cette liste")]
	public GameObject[] listBullet;
	bool isTriggered = false;

	// Use this for initialization
	void Start () {
		if ( listBullet == null || listBullet.Length == 0){
			Component[] mesEnfants = gameObject.GetComponentsInChildren<Transform>(true);
			listBullet = new GameObject[mesEnfants.Length];
			for ( int i =0  ; i < mesEnfants.Length ; i++) {
				listBullet[i] = mesEnfants[i].gameObject;
			}
		}
		// DesActiver les Bullets
		foreach ( GameObject bullet in listBullet ) {
			if ( this.gameObject != bullet ){ // pour eviter de s'appeller soit meme
				bullet.SetActive(false);
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {

		Debug.Log("TriggerAttack");
		if(isTriggered){ return; }

		// Activer les Bullets
		foreach ( GameObject bullet in listBullet ) {
			if ( this.gameObject != bullet ){ // pour eviter de s'appeller soit meme
				bullet.SetActive(true);
			}
		}
		isTriggered = true ;
	}



}
