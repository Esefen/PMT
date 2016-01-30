using UnityEngine;
using System.Collections;

public class BouncyBullet : MonoBehaviour {

	public float force;

	// Use this for initialization
	void Start () {
		this.gameObject.GetComponent<Rigidbody>().AddForce(-transform.up * force);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
