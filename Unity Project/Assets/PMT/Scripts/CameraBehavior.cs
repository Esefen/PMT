using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour {

    Transform player;
    float currentXOffset, baseXOffset;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        baseXOffset = transform.position.x - player.position.x;
        currentXOffset = baseXOffset;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(player.position.x + currentXOffset, transform.position.y, transform.position.z);
	}

    public void changeXOffset(float offsetModifier)
    {
        currentXOffset = Mathf.Clamp(currentXOffset - offsetModifier, baseXOffset * 0.95f, baseXOffset * 1.05f);
    }
}
