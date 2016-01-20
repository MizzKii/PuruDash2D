using UnityEngine;
using System.Collections;

public class IceRealTime : MonoBehaviour {

	private int step = 0;
	public GameObject Ice;
	private GameObject eventObject;
	private GameControl gameControl;
	private CharacterControl character;
	
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
	
	public void StartEvent(float x, float y) {
		character = GameObject.Find("CharacterControl").GetComponent("CharacterControl") as CharacterControl;
		gameControl = GameObject.Find("GameControl").GetComponent("GameControl") as GameControl; 
		gameControl.eventRun = true;
		eventObject = Instantiate(Ice,new Vector3(x, y, -2F),Quaternion.Euler(0f,0f,0f)) as GameObject;
		step = 1;
	}
	
	private void Step1() {
		if(gameControl.EndTrue){
			step = 0;
			Destroy(eventObject);
		}else {
			if(character.underObject && eventObject.transform.position.x < gameControl.stopCharacterX - 2F) 
				character.underObject = false;
			else if(eventObject.transform.position.x < gameControl.stopCharacterX+1)
				character.underObject = true;
		
			if(eventObject.transform.position.x > -10f)
				eventObject.transform.Translate(Vector3.left * (gameControl.SpeedFlow() * Time.deltaTime),Space.World);
			else {
				step = 2;
				Destroy(eventObject);
			}
		}
	}
	
	private void EndEvent() {
		step = 0;;
		gameControl.eventRun = false;
		//gameControl.CountNow++;
	}
}
