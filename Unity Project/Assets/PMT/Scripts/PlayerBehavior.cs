using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {

    public float BASE_SPEED = 0.1f;
    float speed;
    public float gravity = 10.0f;
    bool inAir = true;
    float weight = 0.1f;

	// Use this for initialization
	void Start () {
        speed = BASE_SPEED;
        GetComponent<MeshRenderer>().sortingLayerName = "Player";
        //GetComponent<MeshRenderer>().sortingLayerID = 1;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Horizontal") != 0)
        {
            if (Input.GetAxis("Horizontal") < 0) speed -= BASE_SPEED / 30.0f;
            else speed += BASE_SPEED / 30.0f;
            speed = Mathf.Clamp(speed, BASE_SPEED / 2.0f, BASE_SPEED * 2.0f);
        }
        else speed = Mathf.Lerp(speed, BASE_SPEED, 0.1f);
        if (inAir)
        {
            //transform.Translate(Vector3.down * weight);
            transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.down * BASE_SPEED, 1);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.right * speed, 1);
        }
	}

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            inAir = false;
        }
    }
}
