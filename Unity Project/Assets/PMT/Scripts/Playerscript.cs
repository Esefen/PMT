using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	/// <summary>
	/// 1 - La vitesse de déplacement
	/// </summary>
	public Vector2 speed = new Vector2(50, 50);
	public int NombreDoor;
	// 2 - Stockage du mouvement
	private Vector2 movement;

	void Update()
	{
		// 3 - Récupérer les informations du clavier/manette
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");

		// 4 - Calcul du mouvement
		movement = new Vector2(
			speed.x * inputX,
			speed.y * inputY);

	}

	void FixedUpdate()
	{
		// 5 - Déplacement
		GetComponent<Rigidbody2D>().velocity = movement;
	}

}