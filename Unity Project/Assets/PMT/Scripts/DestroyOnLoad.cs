using UnityEngine;
using System.Collections;

public class DestroyOnLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		if (GameObject.FindGameObjectsWithTag("Canvas").GetLength(0) > 1)
		{
			this.Destroy(gameObject);
		}
	}
		
	
	// Update is called once per frame
	void Update () {
	
	}
}
