﻿using UnityEngine;
using System.Collections;

public class PoisonJelly : MonoBehaviour {

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
			other.gameObject.GetComponent<PlayerBehavior>().isPoisoned = true;
		}
	}
}
