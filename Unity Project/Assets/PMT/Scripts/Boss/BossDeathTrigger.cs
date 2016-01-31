using UnityEngine;
using System.Collections;

public class BossDeathTrigger : MonoBehaviour {

	public GameObject boss;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			boss.GetComponent<Boss>().Death();
		}
	}
}
