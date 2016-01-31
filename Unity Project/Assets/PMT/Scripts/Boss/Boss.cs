using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {

	public static int bossLevel = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Death()
	{
		// METTRE L'ANIM DE MORT DU BOSS SI ON L'A
		Scoring.score += 5000 + ((GameObject.Find("Player").GetComponent<PlayerBehavior>().playerLevel - Boss.bossLevel) * 1000);
		Destroy(gameObject);
	}
	

}