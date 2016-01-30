using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {
	
	public float Speed;
	public float Damage;

	public bool isBoosted;
	public bool isGrounded;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isGrounded == false)
		transform.localPosition -= transform.up * Speed * Time.deltaTime;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			DamagePlayer();
			Destroy(gameObject);
		}
		else if (other.gameObject.tag == "Ground" && isBoosted == false)
		{
			Destroy(gameObject);
		}
		else if (other.gameObject.tag=="Ground" && isBoosted == true)
		{
			isGrounded = true;
		}
	}
	
	void DamagePlayer()
	{
	}
}
