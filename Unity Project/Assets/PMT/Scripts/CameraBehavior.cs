using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour {

    Transform player;
    float currentXOffset, baseXOffset;
    bool offsetChanged = false;
    bool screenShaking = false;

    Vector3 originalPosition;
    float shakeAmount = 2.0f;
    float screenSize;

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
        if (offsetChanged) currentXOffset = Mathf.Lerp(currentXOffset, baseXOffset, 0.3f);
        if (!screenShaking) transform.position = new Vector3(player.position.x + currentXOffset, transform.position.y, transform.position.z);
	}

    public void changeXOffset(float offsetModifier)
    {
        currentXOffset = Mathf.Clamp(currentXOffset - offsetModifier, baseXOffset * 0.95f, baseXOffset * 1.05f);
        offsetChanged = true;
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
