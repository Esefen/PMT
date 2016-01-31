using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {

    CharacterController cController;
    CameraBehavior cam;
    SpriteRenderer shield;
    public Image hpBar;

	public GameObject TextBox1;
	public GameObject TextBox2;
	public GameObject TextBox3;
	public GameObject TextBox4;

    public float BASE_SPEED = 10f;
    public float BASE_JUMP = 10.0f;
    public float JUMP_INPUT_MAX_LENGTH = 0.4f;
    public float JUMP_COOLDOWN_LENGTH = 0.3f;
    public float GRAVITY = 10.0f;
    public float SLIDE_LENGTH = 1.5f;
    public float DASH_POWER = 3f;
    float DASH_LENGTH = 0.3f;
    public float DASH_COOLDOWN_LENGTH = 2.0f;
    public float INVULNERABILITY_LENGTH = 0.5f;
    float speed, jumpPower;
    bool inAir = true;
    bool apexReached = true;
    bool jumpCooldown = true;
    bool doubleJumpCooldown = true;
    bool upKeyReleased = true;
    Vector3 forwardMovement, verticalMovement = Vector3.zero;
    float timerJumpPower, timerJumpCooldown, timerGravityKick;
    bool sliding = false;
    float slideTimer;
    float timerInvulnerability;
    bool dashing = false, canDash = true;
    float timerDash, timerDashCooldown;
    float shieldPoints = 0;

	public bool isPoisoned;
	public float poisonDamage;

    public float hitPoints = 100;
    float MAX_HIT_POINTS = 100;

    public int playerLevel = 1;
    public bool slideUnlocked = true;
    public bool doubleJumpUnlocked = true;
    public bool dashUnlocked = true;
    public bool shieldUnlocked = true;

	// Use this for initialization
	void Start () {
        cController = GetComponent<CharacterController>();
        cam = Camera.main.GetComponent<CameraBehavior>();
        speed = BASE_SPEED;
        jumpPower = BASE_JUMP;
        GetComponent<MeshRenderer>().sortingLayerName = "Player";
        shield = transform.Find("Shield").GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            inflictDamage(5);
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            if (Input.GetAxis("Horizontal") < 0) speed -= BASE_SPEED / 30.0f;
            else speed += BASE_SPEED / 30.0f;
            speed = Mathf.Clamp(speed, BASE_SPEED / 2.0f, BASE_SPEED * 2.0f);
            cam.changeXOffset((speed - BASE_SPEED) / BASE_SPEED);
        }
        else speed = Mathf.Lerp(speed, BASE_SPEED, 0.1f);
        if (Input.GetAxis("Vertical") <= 0) upKeyReleased = true;

        if (dashUnlocked && canDash && Input.GetKeyDown(KeyCode.E))
        {
            dashing = true;
            canDash = false;
            timerDash = Time.time;
        }

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

                if (apexReached) timerGravityKick = Time.time;
            }
            else
            {
                verticalMovement = Vector3.down * Mathf.Clamp(Mathf.Lerp(0, GRAVITY, Time.time - timerGravityKick), 0, GRAVITY);
            }
            if (upKeyReleased && doubleJumpUnlocked && !doubleJumpCooldown && Input.GetAxis("Vertical") > 0)
            {
                doubleJumpCooldown = true;
                apexReached = false;
                timerJumpPower = Time.time;
            }
        }
        else
        {
            // Reset jump cooldown
            if (Time.time - timerJumpCooldown > JUMP_COOLDOWN_LENGTH && Input.GetAxis("Vertical") <= 0)
            {
                jumpCooldown = false;
                doubleJumpCooldown = false;
                jumpPower = BASE_JUMP;
            }
            // Jump
            if (!jumpCooldown && Input.GetAxis("Vertical") > 0)
            {
                if (sliding)
                {
                    sliding = false;
                    cController.enabled = true;
                }
                inAir = true;
                jumpCooldown = true;
                upKeyReleased = false;
                timerJumpPower = Time.time;
                jumpPower = BASE_JUMP;
                verticalMovement = Vector3.up * jumpPower;
            }
            // Slide
            else if (!sliding)
            {
                if (slideUnlocked && Input.GetAxis("Vertical") < 0) slide();
            }
            else if (Time.time - slideTimer > SLIDE_LENGTH)
            {
                sliding = false;
                cController.enabled = true;
                cController.height = 2;
                cController.center = Vector3.zero;
            }
        }
        forwardMovement = Vector3.right * speed;
        //transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.right * speed, 1);
        //transform.Translate(forwardMovement / 100.0f + verticalMovement);
        if (!dashing)
        {
            transform.Translate(new Vector3(forwardMovement.x / 100.0f, verticalMovement.y, 0));
            if (Time.time - timerDashCooldown > DASH_COOLDOWN_LENGTH) canDash = true;
        }
        else
        {
            transform.Translate(new Vector3(BASE_SPEED * DASH_POWER / 100.0f, 0, 0));
            if (Time.time - timerDash > DASH_LENGTH)
            {
                dashing = false;
                timerDashCooldown = Time.time;
            }
        }
    }

    #region movement

    void slide()
    {
        sliding = true;
        slideTimer = Time.time;
        cController.height = 1;
        cController.center = new Vector3(0, -0.5f, 0);
    }

    void OnCollisionEnter(Collision coll)
    {
        Debug.Log("collision");
        if (coll.gameObject.tag == "Ground") groundTouched();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        if (other.CompareTag("Ground")) groundTouched();
    }

    void groundTouched()
    {
        inAir = false;
        apexReached = false;
        timerJumpCooldown = Time.time;
        verticalMovement = Vector3.zero;
        cController.SimpleMove(Vector3.down);
    }

    #endregion

    #region damage

    public void inflictDamage(float damageValue)
    {
        if (Time.time - timerInvulnerability > INVULNERABILITY_LENGTH)
        {
            timerInvulnerability = Time.time;
            if (shieldUnlocked && shieldPoints > 0)
            {
                shieldPoints--;
            }
            else
            {
                hitPoints = Mathf.Clamp(hitPoints - damageValue, 0, MAX_HIT_POINTS);
                refreshHitPoints();
            }
			if (damageValue >= MAX_HIT_POINTS/100) cam.launchShake();
        }
    }

    void refreshHitPoints()
    {
        hpBar.fillAmount = hitPoints / MAX_HIT_POINTS;
    }

	void Poisoned()
	{
		inflictDamage(poisonDamage);
	}

    #endregion

    public void levelUp()
    {
        playerLevel++;
        if (playerLevel > 9) shieldPoints++;
        else 
        switch (playerLevel)
        {
            case 2: BASE_SPEED *= 1.5f;
                break;
            case 3: slideUnlocked = true;
			TextBox1.SetActive(true);
                break;
            case 4: BASE_JUMP *= 1.5f;
                break;
            case 5: doubleJumpUnlocked = true;
			TextBox2.SetActive(true);
                break;
            case 6: BASE_JUMP *= 1.5f;
                break;
            case 7: dashUnlocked = true;
			TextBox3.SetActive(true);
                break;
            case 8: DASH_POWER *= 1.5f;
                break;
            case 9: shieldUnlocked = true;
			TextBox4.SetActive(true);
                    shieldPoints = 1;
                break;
            default: Debug.LogError("PlayerBehavior, levelUp: Incorrect level increase");
                break;
        }
    }
}
