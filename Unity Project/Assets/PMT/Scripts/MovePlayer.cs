using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {

	public Vector2 speed = new Vector2(1, 0);

	// 2 - Stockage du mouvement
	private Vector2 movement;

	void Update()
	{
		// 3 - Récupérer les informations du clavier/manette
		float inputX = Input.GetAxis("Horizontal");

		// 4 - Calcul du mouvement
		movement = new Vector2(
			speed.x * inputX,0);

	}

	void FixedUpdate()
	{
		// 5 - Déplacement
		GetComponent<Rigidbody2D>().velocity = movement;
	}
}
