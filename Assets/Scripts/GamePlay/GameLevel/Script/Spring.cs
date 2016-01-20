using UnityEngine;
using System.Collections;

public class Spring : MonoBehaviour {

	public int hight = 3;

	void OnTriggerEnter2D (Collider2D collision) {
		if(collision.gameObject.name == "CharacterControl" || collision.gameObject.name == "CC")
			collision.gameObject.SendMessage("AddJump",hight);
		Debug.Log(collision.gameObject.name);
	}
}
