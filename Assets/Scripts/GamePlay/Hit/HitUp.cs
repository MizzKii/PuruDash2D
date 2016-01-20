using UnityEngine;
using System.Collections;

public class HitUp : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D collision) {
		if(collision.transform.name == "CharacterControl"){
			//CharacterControl chracter = GameObject.Find("CharacterControl").GetComponent<CharacterControl>();
			//chracter.HitUp();
		}
	}
}
