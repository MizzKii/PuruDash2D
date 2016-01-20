using UnityEngine;
using System.Collections;

public class PrickerRealTime : MonoBehaviour {

	private int step = 0;
	public GameObject pricker;
	private GameObject eventObject;
	private float startPosition = 3;
	private float height;
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
	
	public void StartEvent(float x, float y) {
		gameControl = GameObject.Find("GameControl").GetComponent("GameControl") as GameControl; 
		gameControl.eventRun = true;
		eventObject = Instantiate(pricker,new Vector3(x, y - pricker.transform.localScale.y/2-0.5F, -2F),Quaternion.Euler(90f,180f,0f)) as GameObject;
		height = y + pricker.transform.localScale.y/2 -0.5F;
		step = 1;
	}
	
	private void Step1() {
		if(gameControl.EndTrue){
			step = 0;
			Destroy(eventObject);
		}else if(eventObject.transform.position.x > startPosition) {
			eventObject.transform.Translate(Vector3.right * -(gameControl.SpeedFlow() * Time.deltaTime),Space.World);
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
		}else if(eventObject.transform.position.y < height) {
			eventObject.transform.Translate(Vector3.left * (gameControl.SpeedFlow() * Time.deltaTime),Space.World);
			eventObject.transform.Translate(Vector3.up * (gameControl.SpeedFlow() * Time.deltaTime),Space.World);
		}
		else
		{
			step = 3;
			eventObject.transform.position = new Vector3(eventObject.transform.position.x, height, eventObject.transform.position.z);
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
