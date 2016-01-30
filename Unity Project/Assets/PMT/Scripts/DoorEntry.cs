using UnityEngine;
using System.Collections;

public class DoorEntry : MonoBehaviour {


	void OnTriggerEnter2D(Collider2D collider)
	{
			Door porte = collider.gameObject.GetComponent<Door>();

	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
