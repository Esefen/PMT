using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour {

    Transform player;
    float OFFSET_PERCENTAGE_BACKWARD = 0.3f, OFFSET_PERCENTAGE_FORWARD = 0.5f;
    float currentXOffset, baseXOffset;
    bool offsetChanged = false;
    bool screenShaking = false;

    Vector3 originalPosition;
    float shakeAmount = 2.0f;
    float screenSize;
    float lastValue, timerlastValue, DELAY_BEFORE_OFFSET_RESTORATION = 0.5f;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        baseXOffset = transform.position.x - player.position.x;
        currentXOffset = baseXOffset;
        originalPosition = transform.position;
        screenSize = Screen.height / 10000.0f;
	}
	
	// Update is called once per frame
    void Update()
    {
        if (offsetChanged && Time.time - timerlastValue > DELAY_BEFORE_OFFSET_RESTORATION)
        {
            currentXOffset = Mathf.Lerp(lastValue, baseXOffset, Mathf.Clamp((Time.time - (timerlastValue + DELAY_BEFORE_OFFSET_RESTORATION)) * 2.0f, 0, 1));
        }
        if (!screenShaking) transform.position = new Vector3(player.position.x + currentXOffset, transform.position.y, transform.position.z);
	}

    public void changeXOffset(float offsetModifier)
    {
        offsetModifier /= 2.0f;
        lastValue = Mathf.Clamp(currentXOffset - offsetModifier, baseXOffset * (1f - OFFSET_PERCENTAGE_FORWARD), baseXOffset * (1f + OFFSET_PERCENTAGE_BACKWARD));
        currentXOffset = lastValue;
        offsetChanged = true;
        timerlastValue = Time.time;
    }

    public void launchShake(float tempshakeAmount)
    {
        shakeAmount = tempshakeAmount;
        launchShake();
    }

    public void launchShake()
    {
        InvokeRepeating("shakeScreen", 0, .01f);
        Invoke("stopShaking", 0.3f);
    }

    void shakeScreen()
    {
        if (shakeAmount > 0)
        {
            float quakeAmtX = (Random.value * shakeAmount * 2 - shakeAmount) * screenSize;
            float quakeAmtY = (Random.value * shakeAmount * 2 - shakeAmount) * screenSize;
            Vector3 pp = transform.position;
            pp.x += quakeAmtX;
            pp.y += quakeAmtY;
            transform.position = pp;
        }
    }

    void stopShaking()
    {
        CancelInvoke("shakeScreen");
        shakeAmount = 2.0f;
        screenShaking = false;
        transform.position = new Vector3(transform.position.x, originalPosition.y, originalPosition.z);
    }
}
