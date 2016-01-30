using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {
	
	public GameObject bullet;
	public GameObject rock;
	public GameObject jelly;
	public GameObject shootingPoint;
	public GameObject Player;
	
	public float patternCD;
	public float distanceToPlayer;
	public float rockFallHeight;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void patternSelection()
	{
	}
	
	private void bulletShot()
	{
		Instantiate(bullet, shootingPoint.transform.position, Quaternion.identity);
	}
	
	private void rockFall()
	{
		Vector2 fallPosition = new Vector2 (Player.transform.position.x + distanceToPlayer, rockFallHeight);
		Instantiate(rock, fallPosition, Quaternion.identity);
	}
	
	private void jellyThrow()
	{
	}
	
	private void death()
	{
	}
}