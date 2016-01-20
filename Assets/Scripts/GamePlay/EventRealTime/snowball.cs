using UnityEngine;
using System.Collections;

public class snowball : MonoBehaviour {
	private GameControl gameControl;
	private Transform _Character;
	void Start () {
		gameControl = GameObject.Find("GameControl").GetComponent("GameControl") as GameControl; 
	}
	
	
	void Update(){
		if(transform.position.y < -5)
			Destroy(transform.gameObject);
	}
	
	void OnTriggerEnter2D (Collider2D collision) {
		Debug.Log(collision.name);
		if(collision.transform.name == "CharacterControl"){
			gameControl.EndTrue = true;
			Destroy(transform.gameObject);
		}
	}
	
}
