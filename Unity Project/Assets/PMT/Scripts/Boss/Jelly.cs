using UnityEngine;
using System.Collections;

public class Jelly : MonoBehaviour {
	
	public float Speed;
	public float Damage;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition -= transform.right * Speed * Time.deltaTime;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			DamagePlayer();
		}
	}
	
	void DamagePlayer()
	{
	}
}

