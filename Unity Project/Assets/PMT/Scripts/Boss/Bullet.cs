using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	public float Speed;
	public float Damage;
	public float lifeTime;
	
	// Use this for initialization
	void Start () {
		Invoke("lifeEnd", lifeTime);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position -= transform.right * Speed * Time.deltaTime;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			Debug.Log("collide");
			other.gameObject.GetComponent<PlayerBehavior>().inflictDamage(Damage);
			Destroy(gameObject);
		}
	}

	void lifeEnd()
	{
		Destroy(gameObject);
	}
}

