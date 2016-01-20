using UnityEngine;
using System.Collections;

public class BarRealTime : MonoBehaviour {
	
	private int step = 0;
	public GameObject Bar;
	private GameObject eventObject;
	private float startX;
	private GameControl gameControl;
	//private CharacterControl character;
	public GameObject Flow;
	
	// Use this for initialization
	void Start () {
		gameControl = GameObject.Find("GameControl").GetComponent("GameControl") as GameControl; 
		//character = GameObject.Find("CharacterControl").GetComponent("CharacterControl") as CharacterControl;
		startX = gameControl.stopCharacterX;
	}
	
	// Update is called once per frame
	void Update () {
		if (step == 1){
			Step1();
		}else if (step == 2)
			Step2();
		else if (step == 3)
			EndEvent();
	}
	
	public void StartEvent() {
		gameControl.eventRun = true;
		Instantiate(Flow,new Vector3(18,0,-3),Flow.transform.rotation);
		eventObject = Instantiate(Bar,new Vector3(27F, -2.8F, -2F),Quaternion.Euler(0f,0f,0f)) as GameObject;
		step = 1;
		settled = 0;
		lr = 1;
		down = false;
		bonus = false;
		stage2 = false;
	}
	private bool bonus;
	private bool stage2;
	private int endPoint = 30;
	private int settled = 0,lr = 1;
	private void Step1() {
		
		if(gameControl.EndTrue){
			step = 0;
			Destroy(eventObject);
		}else if(settled > endPoint || settled < endPoint * -1){
			gameControl.EndTrue = true;
		}else {
		
			if(eventObject.transform.position.x < startX - eventObject.transform.localScale.x/2 - 2) {
				step = 2;
			}else if(eventObject.transform.position.x < 9 && !stage2){
				Instantiate(Flow,new Vector3(18,0,-3),Flow.transform.rotation);
				stage2 = true;
			}else if(eventObject.transform.position.x < -9 && !bonus) {
				GamePlay gp = GameObject.Find("GameControl").GetComponent<GamePlay>();
				gp.Bonus = true;
				bonus = true;
			}else if(eventObject.transform.position.x < startX + eventObject.transform.localScale.x/2 + 2) {
				while(settled == 0)
					settled = (int)Random.Range((endPoint/4*-1), endPoint/4);
				TimeLR();
				ClickObject();
				TouchObject();
			}
			
			eventObject.transform.Translate(Vector3.left * (gameControl.SpeedFlow() * Time.deltaTime),Space.World);
		
			if(num == 20 && eventObject.transform.position.x < startX - eventObject.transform.localScale.x/4)
				num--;
			else if(num == 19 && eventObject.transform.position.x < startX - eventObject.transform.localScale.x/2)
				num-=2;
			else if(num == 18 && eventObject.transform.position.x < startX + eventObject.transform.localScale.x/4)
				num-=2;
			else if(num == 17 && eventObject.transform.position.x < startX + eventObject.transform.localScale.x/2)
				num-=2;
		}
	}
	private int i = 0;
	private int num = 10;
	private void TimeLR() {
		if(i > num - (int)(gameControl.speedControl * 5)) {
			if(settled > 0)
				settled += lr;
			else if(settled < 0)
				settled -= lr;
			i = 0;
		}
		i++;
	}
	
	public GUISkin _GUISkin;
	void OnGUI() {
		if(step == 1)
			if(eventObject.transform.position.x < startX + eventObject.transform.localScale.x/2){
				SetUpGUIMatrix.Resolution(800,480);
				GUI.skin = _GUISkin;
				GUI.HorizontalSlider(new Rect(340, 220, 120, 40), settled, endPoint*-1, endPoint);
		}
    }
	
	private void Step2() {
		if(eventObject.transform.position.x > -22f)
			eventObject.transform.Translate(Vector3.left * (gameControl.SpeedFlow() * Time.deltaTime),Space.World);
		else{
			step = 4;
			Destroy(eventObject);
		}
	}
	
	private void EndEvent() {
		step = 0;
		settled = 0;
		gameControl.eventRun = false;
	}
	
	public void TouchObject() {
		if (Input.touchCount > 0)
			if(i > num - (int)(gameControl.speedControl * 10)) {
				if(Input.GetTouch(0).deltaPosition.x > 0)
					settled += lr+1;
				else if(Input.GetTouch(0).deltaPosition.x < 0)
					settled -= lr+1;
			}
	}
	
	private bool down = false;
	public void ClickObject() {
		if (Input.GetMouseButtonDown(0)) 
			down = true;
		if (Input.GetMouseButtonUp(0))
			down = false;
		if(down)
			if(i > num - (int)(gameControl.speedControl * 10)) {
				Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				if(mouse.x > 0)
					settled += lr;
				else if(mouse.x < 0)
					settled -= lr;
			}
	}
}
