using UnityEngine;
using System.Collections;

public class WallRealTime : MonoBehaviour {
	
	private float endPoint;
	private int step = 0;
	public int count;
	public GameObject[] texture;
	private GameObject[] box;
	private GameControl gameControl;
	public GameObject Flow;
	//private float tokenspeed;
	private Film _flim;

	private GamePlay gp;
	
	// Use this for initialization
	void Start () {
		box = new GameObject[texture.Length];
		gameControl = GameObject.Find("GameControl").GetComponent("GameControl") as GameControl; 
		gp = GameObject.Find("GameControl").GetComponent<GamePlay>(); 

		endPoint = gameControl.stopCharacterX + 2.2F;
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
	public void StartEvent() {
		gameControl.eventRun = true;
		count = 0;
		int i = 0;
		float y = -3.5f;
		//Instantiate(Flow,new Vector3(18,Flow.transform.position.y,0),Flow.transform.rotation);
		while(i < texture.Length){
			box[i] = Instantiate(texture[i],new Vector3(gp.nextHop+ 20F ,y + (texture[i].transform.localScale.y/2),texture[i].transform.position.z),texture[i].transform.rotation) as GameObject;
			y += texture[i].transform.localScale.y;
			i++;
			count++;
		}
		
		bonus = false;
		step = 1;
	}
	private bool bonus;
	private void Step1() {
		BoxCheck();
	
		if(box[0].transform.position.x > endPoint){
			if(box[0].transform.position.x < 4F && !bonus) {
				GamePlay gp = GameObject.Find("GameControl").GetComponent<GamePlay>();
				gp.Bonus = true;
				bonus = true;
			}
			else if(box[0].transform.position.x < 5){
				Time.timeScale = gameControl.speedtime/2;
			}
			int i = 0;
			while(i < count){
				box[i].transform.Translate(Vector2.right * -(gameControl.SpeedFlow() * Time.deltaTime),Space.World);
				i++;
			}
		}else {
			Time.timeScale = gameControl.speedtime;
			step = 2;
		}
	}
	
	private void BoxCheck() {
		if (Input.touchCount > 0)
			if(count > 1)
				if(TouchObject())
					Destroy(box[--count]);
		if (Input.GetMouseButtonDown(0))
			if(count > 1)
				if(ClickObject())
					Destroy(box[--count]);
	}
	
	public GameObject character;
	
	private void Step2() {
		step = 3;
	}
	
	private void Step3() {
		BoxCheck();
		if(gameControl.EndTrue){
			step = 0;
			while(count > 0)
				Destroy(box[--count]);
		//}else if(box[0].transform.position.x < gameControl.stopCharacterX && gameControl.speedControl != tokenspeed) {
			//gameControl.speedControl = tokenspeed;
		}else if(box[0].transform.position.x > -10F){
			int i = 0;
			while(i < this.count){
				box[i].transform.Translate(Vector3.left * (gameControl.SpeedFlow() * Time.deltaTime),Space.World);
				i++;
			}
		}else {
			step = 4;
			while(count > 0)
				Destroy(box[--count]);
		}
	}
	
	private void EndEvent() {
		step = 0;
		gameControl.eventRun = false;
	}
	
	private bool TouchObject() {
		Vector2 touch = Camera.main.ScreenToWorldPoint( Input.GetTouch(0).deltaPosition );
		if(touch.x >= (box[this.count - 1].transform.position.x - (box[this.count - 1].transform.localScale.x/2)-1)
			&&  touch.x <= (box[this.count - 1].transform.position.x + (box[this.count - 1].transform.localScale.x/2)+1)
			&& touch.y >= (box[this.count - 1].transform.position.y - (box[this.count - 1].transform.localScale.z/2)-1)
			&&  touch.y <= (box[this.count - 1].transform.position.y + (box[this.count - 1].transform.localScale.z/2))+1)
			return true;
		return false;
	}
	private bool ClickObject() {
		Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if(mouse.x >= (box[this.count - 1].transform.position.x - (box[this.count - 1].transform.localScale.x/2))
			&&  mouse.x <= (box[this.count - 1].transform.position.x + (box[this.count - 1].transform.localScale.x/2))
			&& mouse.y >= (box[this.count - 1].transform.position.y - (box[this.count - 1].transform.localScale.z/2))
			&&  mouse.y <= (box[this.count - 1].transform.position.y + (box[this.count - 1].transform.localScale.z/2)))
			return true;
		return false;
	}
	
	public Texture2D film;
	public Texture2D _Touch;
	void OnGUI() {
		if(step == 1) {
			if(box[0].transform.position.x < 5){
				GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), film);
				if(count > 1) {
					Vector3 pos = Camera.main.WorldToScreenPoint(box[this.count - 1].transform.position);
					pos.y = Screen.height - pos.y;
					GUI.DrawTexture(new Rect(pos.x-25, pos.y-10, 100, 100), _Touch);
				}
			}
			//else if(box[0].transform.position.x < 10)
			//	GUI.DrawTexture(new Rect(400, 240, 100,50), wraning);
		}
	}
}