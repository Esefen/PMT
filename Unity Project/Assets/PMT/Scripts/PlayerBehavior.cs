using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {

    CharacterController cController;
    CameraBehavior cam;
    SpriteRenderer shield;
    Animator anim;
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
    public float JUMP_MAX_HEIGHT = 4.0f;
    float DASH_LENGTH = 0.3f;
    public float DASH_COOLDOWN_LENGTH = 2.0f;
    public float INVULNERABILITY_LENGTH = 0.5f;
    float speed, jumpPower;
    bool inAir = true;
    bool apexReached = true;
    bool jumpImpulsionOver = true;
    bool jumpCooldown = true;
    bool doubleJumpCooldown = true;
    bool upKeyReleased = true;
    Vector3 forwardMovement, verticalMovement = Vector3.zero;
    float timerJump, timerJumpCooldown, timerJumpImpulsionCarry, timerGravityKick;
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
    bool dead = false;

    float yJumpReference = -1;
    bool shieldDisplayed = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void OnLevelWasLoaded()
    {
        findCameraReference();
        transform.position = new Vector3(-35, transform.position.y, transform.position.z);
        //cam.transform.position = new Vector3(-45, cam.transform.position.y, cam.transform.position.z);
    }

	// Use this for initialization
	void Start () {
        cController = GetComponent<CharacterController>();
        findCameraReference();
        speed = BASE_SPEED;
        jumpPower = BASE_JUMP;
        GetComponent<MeshRenderer>().sortingLayerName = "Player";
        shield = transform.Find("Shield").GetComponent<SpriteRenderer>();
        shield.color = new Color(1, 1, 1, 0);
        anim = transform.Find("Mesh").GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!dead)
        {
            if (Input.GetKeyDown(KeyCode.A)) inflictDamage(5);

            if (Input.GetAxis("Horizontal") != 0)
            {
                if (Input.GetAxis("Horizontal") < 0) speed -= BASE_SPEED / 30.0f;
                else speed += BASE_SPEED / 30.0f;
                speed = Mathf.Clamp(speed, BASE_SPEED / 2.0f, BASE_SPEED * 2.0f);
                float speedModifier = (speed - BASE_SPEED) / BASE_SPEED;
                cam.changeXOffset(speedModifier);
                anim.SetFloat("speed", Mathf.Clamp(1.0f + (speed - BASE_SPEED) / BASE_SPEED, 0.5f, 1.3f));
            }
            else
            {
                speed = Mathf.Lerp(speed, BASE_SPEED, 0.1f);
                anim.SetFloat("speed", Mathf.Clamp(1.0f + (speed - BASE_SPEED) / BASE_SPEED, 0.5f, 1.5f));
            }
            if (Input.GetAxis("Vertical") <= 0) upKeyReleased = true;

            if (dashUnlocked && canDash && Input.GetKeyDown(KeyCode.E))
            {
                dashing = true;
                canDash = false;
                timerDash = Time.time;
                anim.SetBool("dashing", true);
            }
        }

        if (inAir)
        {
            if (!apexReached)
            {
                /*
                if (!jumpImpulsionOver)
                {
                    if (Input.GetAxis("Vertical") <= 0)
                    {
                        jumpImpulsionOver = true;
                        //timerJumpImpulsionCarry = Time.time;
                    }
                    else if (Time.time - timerJump > JUMP_INPUT_MAX_LENGTH)
                    {
                        jumpImpulsionOver = true;
                        timerJumpImpulsionCarry = Time.time;
                        Debug.Log("long touch");
                    }
                    //if (jumpImpulsionOver) timerJumpImpulsionCarry = Time.time;
                }
                else
                {
                    //Debug.Log(timerJumpImpulsionCarry + " ; " + timerJump);
                    //float jumpLengthReducer = 1.0f;
                    //if (timerJumpImpulsionCarry > timerJump) jumpLengthReducer *= (timerJumpImpulsionCarry - timerJump);
                    //jumpPower = Mathf.Lerp(BASE_JUMP, 0, (Time.time - timerJump) * jumpLengthReducer);
                    if (timerJumpImpulsionCarry > timerJump) jumpPower = Mathf.Lerp(BASE_JUMP, 0, Time.time - timerJumpImpulsionCarry);
                    else jumpPower = Mathf.Lerp(BASE_JUMP, 0, Time.time - timerJump);
                    if (jumpPower <= 0) apexReached = true;
                }
                */
                verticalMovement = Vector3.up * jumpPower;

                if (apexReached)
                {
                    timerGravityKick = Time.time;
                    jumpImpulsionOver = false;
                }
            }
            else
            {
                verticalMovement = Vector3.down * Mathf.Clamp(Mathf.Lerp(0, GRAVITY, Time.time - timerGravityKick), 0, GRAVITY);
            }

            if (upKeyReleased && doubleJumpUnlocked && !doubleJumpCooldown && Input.GetAxis("Vertical") > 0)
            {
                doubleJumpCooldown = true;
                apexReached = false;
                timerJump = Time.time;
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
                if (sliding) endSliding();
                inAir = true;
                jumpCooldown = true;
                upKeyReleased = false;
                timerJump = Time.time;
                jumpPower = BASE_JUMP;
                verticalMovement = Vector3.up * jumpPower;
                anim.SetBool("jumping", true);
            }
            // Slide
            else if (!sliding)
            {
                if (slideUnlocked && Input.GetAxis("Vertical") < 0) slide();
            }
            else if (Time.time - slideTimer > SLIDE_LENGTH) endSliding();
        }
        forwardMovement = Vector3.right * speed;

        if (!dashing)
        {
            transform.Translate(new Vector3(forwardMovement.x / 100.0f, verticalMovement.y, 0));
            if (yJumpReference != -1)
            {
                transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, yJumpReference - 1.0f, yJumpReference + JUMP_MAX_HEIGHT), transform.position.z);
                if (Mathf.Approximately(transform.position.y, yJumpReference + JUMP_MAX_HEIGHT)) apexReached = true;
            }
            if (Time.time - timerDashCooldown > DASH_COOLDOWN_LENGTH) canDash = true;
        }
        else
        {
            transform.Translate(new Vector3(BASE_SPEED * DASH_POWER / 100.0f, 0, 0));
            if (Time.time - timerDash > DASH_LENGTH)
            {
                dashing = false;
                timerDashCooldown = Time.time;
                anim.SetBool("dashing", false);
            }
        }
    }

    public void findCameraReference()
    {
        cam = Camera.main.GetComponent<CameraBehavior>();
    }

    #region movement

    void slide()
    {
        Debug.Log("slide");
        sliding = true;
        slideTimer = Time.time;
        cController.height = 1;
        cController.center = new Vector3(0, -0.5f, 0);
        anim.SetBool("sliding", true);
    }

    void endSliding()
    {
        sliding = false;
        cController.height = 2;
        cController.center = Vector3.zero;
        anim.SetBool("sliding", false);
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
        yJumpReference = transform.position.y;
        inAir = false;
        apexReached = false;
        timerJumpCooldown = Time.time;
        verticalMovement = Vector3.zero;
        cController.SimpleMove(Vector3.down);
        anim.SetBool("jumping", false);
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
                if (!shieldDisplayed)
                {
                    shieldDisplayed = true;
                    shield.enabled = true;
                    StartCoroutine(displayShieldFeedback(0, 0.1f));
                }
            }
            else
            {
                speed /= 10.0f;
                hitPoints = Mathf.Clamp(hitPoints - damageValue, 0, MAX_HIT_POINTS);
                refreshHitPoints();
                anim.SetBool("tookDamage", true);
                Invoke("resetDamageFeedbackAnimation", 0.1f);
            }
            if (hitPoints == 0) death();
            else if (damageValue >= MAX_HIT_POINTS / 100) cam.launchShake();
        }
    }

    IEnumerator displayShieldFeedback(float alpha, float alphaModifier)
    {
        alpha += alphaModifier;
        shield.color = new Color(1, 1, 1, alpha);
        yield return new WaitForSeconds(0.05f);
        if (alpha < 1) StartCoroutine(displayShieldFeedback(alpha, alphaModifier));
        else Invoke("hideShield", 0.5f);
    }

    void hideShield()
    {
        shieldDisplayed = false;
        StartCoroutine(hideShieldCoroutine(1, -0.15f));
    }

    IEnumerator hideShieldCoroutine(float alpha, float alphaModifier)
    {
        if (!shieldDisplayed)
        {
            alpha += alphaModifier;
            shield.color = new Color(1, 1, 1, alpha);
            yield return new WaitForSeconds(0.05f);
            if (alpha < 1) StartCoroutine(hideShieldCoroutine(alpha, alphaModifier));
            else shield.enabled = false;
        }
    }

    void resetDamageFeedbackAnimation()
    {
        anim.SetBool("tookDamage", false);
    }

    void death()
    {
        Debug.Log("death");
        dead = true;
        anim.SetBool("dead", true);
        StartCoroutine(deathSequence(1.0f));
    }

    IEnumerator deathSequence(float newTimeScale)
    {
        if (newTimeScale > 0)
        {
            Time.timeScale = newTimeScale;
            speed = newTimeScale;
            yield return new WaitForSeconds(0.05f);
            StartCoroutine(deathSequence(newTimeScale - 0.1f));
        }
        else Application.LoadLevel("GameOver");
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
