using UnityEngine;
using System.Collections;

public class StoneRealTime : MonoBehaviour {
	
	private int step = 0;
	public GameObject stone;
	private GameObject eventObject;
	private GameControl gameControl;
	
	// Use this for initialization
	/*void Start () {
		gameControl = GameObject.Find("GameControl").GetComponent("GameControl") as GameControl; 
	}*/
	
	// Update is called once per frame
	void Update () {
		if (step == 1){
			Step1();
		}else if (step == 2)
			EndEvent();
		
	}
	
	public void StartEvent(float x, float y, float z) {
		gameControl = GameObject.Find("GameControl").GetComponent("GameControl") as GameControl; 
		gameControl.eventRun = true;
		eventObject = Instantiate(stone,new Vector3(x, y, z),Quaternion.Euler(90f,180f,0f)) as GameObject;
		step = 1;
	}

	private void Step1() {
		if(gameControl.EndTrue){
			step = 0;
			Destroy(eventObject);
		}else if(eventObject.transform.position.x > -10f)
			eventObject.transform.Translate(Vector3.left * (gameControl.SpeedFlow() * Time.deltaTime),Space.World);
		else{
			step = 2;
			Destroy(eventObject);
		}
	}
	
	private void EndEvent() {
		step = 0;
		if(eventObject != null)
			Destroy(eventObject);
		gameControl.eventRun = false;
		//gameControl.CountNow++;
	}
}
