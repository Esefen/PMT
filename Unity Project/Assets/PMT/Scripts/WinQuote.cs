using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinQuote : MonoBehaviour {

	public Text text;

	// Use this for initialization
	void Start () {
		
		text = this.GetComponent<Text>();

		if (Boss.bossLevel == 1)
		{
			text.text = "Are you even trying? N00B!";
		}
		else if (Boss.bossLevel >= 5)
		{
			text.text = "You could try harder!";
		}
		else if (Boss.bossLevel == 6)
		{
			text.text = "Perfect balance! Are you a Game Designer?";
		}
		else if (Boss.bossLevel <= 10)
		{
			text.text = "That's pretty damn good!";
		}
		else if (Boss.bossLevel == 11)
		{
			text.text = "Godlike performance!";
		}

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
