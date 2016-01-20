using UnityEngine;
using System.Collections;

public class StalactiteRealTime : MonoBehaviour {

	private int step = 0;
	public GameObject Stalactite;
	private GameObject eventObject;
	private float startPosition = 4;
	private float downY;
	private GameControl gameControl;
	
	// Use this for initialization
	void Start () {
		gameControl = GameObject.Find("GameControl").GetComponent("GameControl") as GameControl; 
	}
	
	// Update is called once per frame
	void Update () {
		if (step == 1){
			Step1();
		}else if (step == 2)
			Step2();
		else if (step == 3)
			Step3();
		else if (step == 4)
			EndEvent();
	}
	
	public void StartEvent(float x, float y, float flowY) {
		gameControl = GameObject.Find("GameControl").GetComponent("GameControl") as GameControl; 
		gameControl.eventRun = true;;
		eventObject = Instantiate(Stalactite,new Vector3(x, y + Stalactite.transform.localScale.y/2, -2F),Quaternion.Euler(0f,0f,0f)) as GameObject;
		downY = -3 + flowY;
		step = 1;
	}
	
	private void Step1() {
		if(gameControl.EndTrue){
			step = 0;
			Destroy(eventObject);
		}else if(eventObject.transform.position.x > startPosition) {
			eventObject.transform.Translate(Vector3.left * (gameControl.SpeedFlow() * Time.deltaTime),Space.World);
		}
		else
		{
			step = 2;
		}
	}
	
	private void Step2() {
		if(gameControl.EndTrue){
			step = 0;
			Destroy(eventObject);
		}else if(eventObject.transform.position.y > downY) {
			eventObject.transform.Translate(Vector3.left * (gameControl.SpeedFlow() * Time.deltaTime),Space.World);
			eventObject.transform.Translate(Vector3.down * ((gameControl.SpeedFlow() * 3.5F) * Time.deltaTime),Space.World);
		}
		else
		{
			step = 3;
			eventObject.transform.position = new Vector3(eventObject.transform.position.x, downY, eventObject.transform.position.z);
		}
	}
	
	private void Step3() {
		if(gameControl.EndTrue){
			step = 0;
			Destroy(eventObject);
		}else if(eventObject.transform.position.x > -10f)
			eventObject.transform.Translate(Vector3.left * (gameControl.SpeedFlow() * Time.deltaTime),Space.World);
		else{
			step = 4;
			Destroy(eventObject);
			AddScore(100);
		}
	}
	
	private void EndEvent() {
		step = 0;
		if(eventObject != null)
			Destroy(eventObject);
		gameControl.eventRun = false;
		//gameControl.CountNow++;
	}
	
	private void AddScore(int x) {
		gameControl.score += x;
	}
}
