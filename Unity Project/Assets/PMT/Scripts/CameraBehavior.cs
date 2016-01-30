using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour {

    Transform player;
    float xOffset;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        xOffset = transform.position.x - player.position.x;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(player.position.x + xOffset, transform.position.y, transform.position.z);
	}
}
