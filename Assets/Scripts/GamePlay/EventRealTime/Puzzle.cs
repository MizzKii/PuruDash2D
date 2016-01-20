using UnityEngine;
using System.Collections;

public class Puzzle : MonoBehaviour {

	public GameObject[] puzzle;
	public GameObject flow;
	private GameObject pattern;
	private GameControl gc;
	private GamePlay gp;
	private float speedNow;
	private int step = 0;

	// Use this for initialization
	void Start () {
		gc = GameObject.Find("GameControl").GetComponent<GameControl>();
		gp = GameObject.Find("GameControl").GetComponent<GamePlay>();
	}
	
	// Update is called once per frame
	void Update () {
		if(step == 1)
			Step1();
		else if(step == 2)
			Step2();
		else if(step == 3)
			EndEvent();
	}

	public void StartEvent() {
		int num = (int)Random.Range(0, puzzle.Length - 0.001F);
		pattern = Instantiate(puzzle[num], new Vector3(18,puzzle[num].transform.position.y, puzzle[num].transform.position.z), puzzle[num].transform.rotation) as GameObject;
		step = 1;
		speedNow = gc.speedControl;
	}

	void Step1() {
		if(pattern.transform.position.x <= 6 && Time.timeScale >= 1){
			Time.timeScale = 0.6F + gc.speedtime/10;
			gc.speedControl = 1;
			gc.Rocket = true;
			StartCoroutine("Ready");
		}else if(pattern.transform.position.x >= -17.25F)
			pattern.transform.Translate(Vector3.left * (gc.SpeedFlow() * Time.deltaTime),Space.World);
		else {
			Instantiate(flow,new Vector3(18,flow.transform.position.y,flow.transform.position.z),flow.transform.rotation);
			step = 2;
		}
		DestroyIce();
	}
	private int countDown = 2;
	IEnumerator Ready() {
		countDown = 1;
		yield return new WaitForSeconds(0.5F);
		countDown = 0;
		yield return new WaitForSeconds(0.5F);
		countDown = -1;
	}

	public GUISkin _GUISkin;
	public Texture2D flim,pointer;
	public Texture2D[] _num;
	void OnGUI() {
		if(pattern != null)
		if(countDown > -1 && countDown < 2) {
			GUI.skin = _GUISkin;
			Vector3 pos = Camera.main.WorldToScreenPoint(pattern.transform.position);
			pos.y = Screen.height - pos.y;
			GUI.DrawTexture(new Rect(pos.x+70+Mathf.PingPong(Time.time*300, 100),pos.y,100,100),pointer);
			SetUpGUIMatrix.Resolution(800,480);
			GUI.DrawTexture(new Rect(0,0,800,480), flim);
			GUI.DrawTexture(new Rect(350,190,200,100),_num[countDown]);
		}else if(pattern.transform.position.x < 8 && pattern.transform.position.x > 0.2) {
			Vector3 pos = Camera.main.WorldToScreenPoint(pattern.transform.position);
			pos.y = Screen.height - pos.y;
			GUI.DrawTexture(new Rect(pos.x+70+  Mathf.PingPong(Time.time*300 ,100),pos.y,100,100),pointer);
			SetUpGUIMatrix.Resolution(800,480);
			GUI.DrawTexture(new Rect(0,0,800,480), flim);
		}

	}

	void Step2() {
		if(pattern != null) {
			if(pattern.transform.position.x < -35) {
				gc.Rocket = false;
				gp.Bonus = true;
				Time.timeScale = gc.speedtime;
				gc.speedControl = speedNow;
				gc.SetSpeed(gc.speedtime);
				step = 3;
			}else
				pattern.transform.Translate(Vector3.left * (gc.SpeedFlow() * Time.deltaTime),Space.World);
		}
		DestroyIce();
	}

	void EndEvent() {
		if(pattern.transform.position.x < -36) {
			Destroy(pattern);
			step = 4;
		}else
			pattern.transform.Translate(Vector3.left * (gc.SpeedFlow() * Time.deltaTime),Space.World);
	}

	void DestroyIce() {
		RaycastHit hit;
		Ray ray;
		if(Input.GetMouseButtonDown(0)){
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Debug.DrawRay(ray.origin, ray.direction * 20,Color.green);
			if(Physics.Raycast(ray,out hit,2000))
			if(hit.transform.gameObject.name == "IceBox"){
				Destroy(hit.transform.gameObject);
				
			}
		}
		else if(Input.GetMouseButton(0)){
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Debug.DrawRay(ray.origin, ray.direction * 20,Color.red);
			if(Physics.Raycast(ray,out hit,2000))
				if(hit.transform.gameObject.name == "IceBox"){
				Destroy(hit.transform.gameObject);
					
				}
		}
	}
}
