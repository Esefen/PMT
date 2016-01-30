using UnityEngine;
using System.Collections;

public class ScalingJelly : MonoBehaviour {

	public float scalingDuration;
	public bool isGrowing;
	public float sizeMax;
	public float sizeMin;
	public float scalingSpeed;
	private Vector3 scaleMax;
	private Vector3 scaleMin;

	// Use this for initialization
	void Start () {
		scaleMax = new Vector3(transform.localScale.x * sizeMax, 0, 0);
		scaleMin = new Vector3(transform.localScale.x * sizeMin, 0, 0);

		InvokeRepeating("changeScale", 0, scalingDuration);
	}
	
	// Update is called once per frame
	void Update () {
		if (isGrowing == true)
		{
			this.transform.localScale += Vector3.Lerp(scaleMin, scaleMax, Time.deltaTime * scalingSpeed);
		}
		else if (isGrowing == false)
		{
			this.transform.localScale -= Vector3.Lerp(scaleMin, scaleMax, Time.deltaTime * scalingSpeed);
		}
	}

	void changeScale()
	{
		if(isGrowing){isGrowing=false;}else{isGrowing=true;};
	}
}
