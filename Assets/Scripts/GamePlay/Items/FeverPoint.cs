using UnityEngine;
using System.Collections;

public class FeverPoint : MonoBehaviour {

	GameControl gameControl;
	// Use this for initialization
	void Start () {
		gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.left * (gameControl.SpeedFlow() * Time.deltaTime),Space.World);
		if(transform.position.x < -10)
			Destroy(transform.gameObject);
	}
	public GameObject _rocket;
	void OnTriggerEnter2D (Collider2D collision) {
		if(collision.transform.name == "CharacterControl" || collision.transform.name == "CC"){
			if(++gameControl.feverPoint >= 5) {
				Instantiate(_rocket);
				gameControl.fever = true;
				collision.transform.name = "CC";
				//if(gameControl.speedtime < 1.8F)
					//Time.timeScale = 1.8F;
			}
			Destroy(transform.gameObject);
		}
	}
}
