using UnityEngine;
using System.Collections;

public class FragBullet : MonoBehaviour {

	public int bulletNumber;
	public GameObject bullet;

	// Use this for initialization
	void Start () {
		Quaternion rotation = Quaternion.identity;
		float angle = this.bulletNumber == 0 ? 0 : 360.0f / this.bulletNumber;

		for (int index = 0; index < this.bulletNumber; ++index)
		{
			GameObject item = Instantiate(bullet, this.transform.position, Quaternion.identity) as GameObject;

			rotation.eulerAngles = new Vector3(0.0f, 0.0f, angle * index);
			item.transform.rotation = rotation;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
