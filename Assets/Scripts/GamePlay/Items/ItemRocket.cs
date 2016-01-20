using UnityEngine;
using System.Collections;

public class ItemRocket : MonoBehaviour {
	
	public GameObject Rocket;
	
	void OnTriggerEnter2D (Collider2D collision) {
		if(collision.transform.name == "CharacterControl" || collision.transform.name == "CC"){
			Instantiate(Rocket);
			Destroy(transform.gameObject);
		}
	}
	
	void Update () {
		GameControl gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
		transform.Translate(Vector3.left * (gameControl.SpeedFlow() * Time.deltaTime),Space.World);
	}
}
