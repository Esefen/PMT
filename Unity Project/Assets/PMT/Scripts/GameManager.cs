using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static int roomsDone = 0;

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}
}
