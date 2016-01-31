using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fading : MonoBehaviour {

	public GameObject blackScreen;
	public Image image;
	public static int blackening = 0;

	private float alpha = 1;

	// Use this for initialization
	void Start () {
		image = blackScreen.GetComponent<Image>();
	}
		
	// Update is called once per frame
	void Update () {
		if (blackening == 0)
		{
			alpha -= 0.05f;
			image.color = new Color(0,0,0,alpha);

			if (alpha <= 0)
			{
				blackening = 1;
			}
		}
		if (blackening == 2)
		{
			alpha += 0.05f;
			image.color = new Color(0,0,0,alpha);
			if (alpha >= 1)
				blackening = 0;
		}	
	}
}
