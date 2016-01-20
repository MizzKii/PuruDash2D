using UnityEngine;
using System.Collections;

public class HitCollider : MonoBehaviour {

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.name == "CharacterControl" || collision.gameObject.name == "CC") {
			CharacterControl cc = GameObject.Find("CharacterControl").GetComponent<CharacterControl>();
			cc.Hit();
			GameControl gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
			gameControl.EndTrue = true;
		}		
	}
}
