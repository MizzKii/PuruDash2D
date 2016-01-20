using UnityEngine;
using System.Collections;

public class HitTrigger2D : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.name == "CharacterControl") {
			CharacterControl cc = GameObject.Find("CharacterControl").GetComponent<CharacterControl>();
			cc.Hit();
			GameControl gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
			gameControl.EndTrue = true;
		}   
		
	}
}
