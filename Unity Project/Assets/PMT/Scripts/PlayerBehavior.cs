using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {

    CharacterController cController;
    public float BASE_SPEED = 0.1f;
    public float BASE_JUMP = 10.0f;
    public float JUMP_INPUT_MAX_LENGTH = 0.4f;
    public float JUMP_COOLDOWN_LENGTH = 1.0f;
    public float GRAVITY = 10.0f;
    float speed, jumpPower;
    bool inAir = true;
    bool apexReached = true;
    bool jumpCooldown = true;
    bool doubleJumpCooldown = true;
    Vector3 forwardMovement, verticalMovement;
    float timerJumpPower, timerJumpCooldown;

	// Use this for initialization
	void Start () {
        cController = GetComponent<CharacterController>();
        speed = BASE_SPEED;
        jumpPower = BASE_JUMP;
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
            if (!apexReached)
            {
                if (Input.GetAxis("Vertical") > 0)
                {
                    jumpPower = Mathf.Lerp(BASE_JUMP, 0, Time.time - timerJumpPower);
                }
                else apexReached = true;
                verticalMovement = Vector3.up * jumpPower;

                if (Time.time - timerJumpPower > JUMP_INPUT_MAX_LENGTH)
                {
                    verticalMovement = Vector3.zero;
                    apexReached = true;
                }
            }
            else
            {
                verticalMovement = Vector3.down * GRAVITY;
            }
            if (!doubleJumpCooldown && Input.GetAxis("Vertical") > 0) jump(); //  double jump
        }
        else
        {
            if (Time.time - timerJumpCooldown > JUMP_COOLDOWN_LENGTH) jumpCooldown = false;
            if (!jumpCooldown && Input.GetAxis("Vertical") > 0) jump();
        }
        forwardMovement = Vector3.right * speed;
        //transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.right * speed, 1);
        transform.Translate(forwardMovement / 100.0f + verticalMovement);
	}

    void jump()
    {
        Debug.Log("jump");
        if (!inAir)
        {
            inAir = true;
            jumpCooldown = true;
            timerJumpPower = Time.time;
        }
        verticalMovement = Vector3.up * jumpPower;
    }

    void OnCollisionEnter(Collision coll)
    {
        Debug.Log("collision");
        if (coll.gameObject.tag == "Ground")
        {
            inAir = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        if (other.CompareTag("Ground"))
        {
            inAir = false;
            apexReached = false;
            timerJumpCooldown = Time.time;
            verticalMovement = Vector3.zero;
            cController.SimpleMove(Vector3.down);
        }
    }
}
