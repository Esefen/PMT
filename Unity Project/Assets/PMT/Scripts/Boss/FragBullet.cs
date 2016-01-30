using UnityEngine;
using System.Collections;

public class FragBullet : MonoBehaviour {

	public float timer;
	public int bulletNumber;
	public GameObject bullet;

	// Use this for initialization
	void Start () {
		
		Invoke("Frag", timer);
	}

	public void Frag()
	{
		Quaternion rotation = Quaternion.identity;
		float angle = this.bulletNumber == 0 ? 0 : 360.0f / this.bulletNumber;

		for (int index = 0; index < this.bulletNumber; ++index)
		{
			GameObject item = Instantiate(bullet, this.transform.position, Quaternion.identity) as GameObject;

			rotation.eulerAngles = new Vector3(0.0f, 0.0f, angle * index);
			item.transform.rotation = rotation;
		}


		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
