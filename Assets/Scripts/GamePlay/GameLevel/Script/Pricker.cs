using UnityEngine;
using System.Collections;

public class Pricker : MonoBehaviour {
	
	private int step = 0;
	private float startPosition = 3;
	private float height, posY;
	private GameControl gameControl;
	
	void Start() {
		posY = transform.position.y;
		if(transform.parent.position.y > 0)
			posY -= transform.parent.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if(step == 0){
			StartEvent();
		}else if (step == 1){
			Step1();
		}else if (step == 2)
			Step2();
	}
	
	public void StartEvent() {
		gameControl = GameObject.Find("GameControl").GetComponent("GameControl") as GameControl;
		height = posY + transform.parent.position.y;
		transform.position = new Vector3(transform.position.x, transform.position.y - transform.localScale.y, -2F);
		step = 1;
	}
	
	private void Step1() {
		if(gameControl.EndTrue){
			step = 0;
			//gameObject.SetActive(false);
		}else if(transform.position.y < height && gameControl.SpeedFlow() > 0) {
			if(transform.position.x < startPosition)
				transform.Translate(Vector3.up * (gameControl.SpeedFlow() * Time.deltaTime),Space.World);
		}
		else
		{
			step = 2;
			transform.position = new Vector3(transform.position.x, height, transform.position.z);
		}
	}
	
	private void Step2() {
		if(transform.position.x > 10f){
			step = 0;
			//StartEvent();
		}
	}
}
