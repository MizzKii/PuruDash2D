using UnityEngine;
using System.Collections;

public class Stalactite : MonoBehaviour {

	private int step = 0;
	private float startPosition = 3;
	private float down, posY;
	private GameControl gameControl;
	private GameObject cc;
	
	void Start() {
		posY = transform.position.y;
		if(transform.parent.position.y > 0)
			posY -= transform.parent.position.y;

		cc = GameObject.Find("Main Camera");
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
		down = posY + transform.parent.position.y;
		transform.position = new Vector3(transform.position.x, 6F, -2F);
		step = 1;
	}
	
	private void Step1() {
		/*if(gameControl.EndTrue){
			step = 0;
			//gameObject.SetActive(false);
		}else*/
		if(transform.position.y > down) {
			if(transform.position.x < cc.transform.position.x+startPosition)
				transform.Translate(Vector3.down * (gameControl.SpeedFlow()*3 * Time.deltaTime),Space.World);
		}
		else
		{
			step = 2;
			transform.position = new Vector3(transform.position.x, down, transform.position.z);
		}
	}
	
	private void Step2() {
		if(transform.position.x > cc.transform.position.x+startPosition){
			step = 0;
			//StartEvent();
		}
	}
}
