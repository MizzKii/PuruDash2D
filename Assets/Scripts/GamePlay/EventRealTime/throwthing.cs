using UnityEngine;
using System.Collections;

public class throwthing : MonoBehaviour {
	
	public GameObject thingPre;
	public int count = 5;
	private GameObject thing;
	public Transform[] spwanpoint;
	public int forceup,forceright;
	
	private GameControl gameControl;
	private GamePlay gp;
	private CharacterControl cha;

	private int step = 0;

	// Use this for initialization
	void Start () {
		gameControl = GameObject.Find("GameControl").GetComponent("GameControl") as GameControl; 
		cha = GameObject.Find("CharacterControl").GetComponent<CharacterControl>();
		gp = GameObject.Find("GameControl").GetComponent<GamePlay>();
	}
	
	// Update is called once per frame
	void Update () {
		if(step == 1)
			Step1();
		else if(step == 2)
			checkrayonmouse();
		else if(step == 3)
			Step3();
		else if(step == 4)
			EndEvent();
	}

	public void  StartEvent(){
		gp.free = true;
		StartCoroutine("Ready");
	}

	private IEnumerator Ready() {
		yield return new WaitForSeconds(1.2F);
		warn = true;
		yield return new WaitForSeconds(1);
		warn = false;
		yield return new WaitForSeconds(0.2F);
		warn = true;
		yield return new WaitForSeconds(0.2F);
		warn = false;
		yield return new WaitForSeconds(0.2F);
		warn = true;
		yield return new WaitForSeconds(0.2F);
		warn = false;

		gp.free = false;
		step = 1;
		gp.checkrungame = false;
	}

	void Step1() {
		if(cha.MoveTo(0)){
			gameControl.Rocket = true;
			Time.timeScale = 0.5F + gameControl.speedtime/10;
			StartCoroutine("Step2");
			step = 2;
		}
	}

	private bool warn = false;
	public Texture2D warning;
	void OnGUI() {
		if(warn) {
			SetUpGUIMatrix.Resolution(800,480);
			GUI.DrawTexture(new Rect(350,190,100,100),warning);
		}
	}

	IEnumerator Step2(){
		for(int i=0; i < count;i++ ){
			if(gameControl.EndTrue)
				break;
			yield return new WaitForSeconds(0.5F);
			int a = (int)(Random.Range(0.0f,spwanpoint.Length-0.01f));
			thing = (GameObject)Instantiate(thingPre,spwanpoint[a].position,thingPre.transform.rotation);

			thing.rigidbody.AddForce(Vector3.up * forceup);
			if(spwanpoint[a].transform.position.x < 0 )
				thing.rigidbody2D.AddForce(Vector2.right * forceright);
			else
				thing.rigidbody2D.AddForce(Vector2.right * -forceright);
		}
		yield return new WaitForSeconds(2F);
		Time.timeScale = gameControl.speedtime;
		step = 3;
	}

	private void Step3() {
		if(cha.MoveTo(gameControl.stopCharacterX)){
			step = 4;
		}
	}

	private void EndEvent() {
		if(!gp.PatterOn) {
			gp.PatterOn = true;
			gp.Bonus = true;
			gameControl.Rocket = false;
			gp.checkrungame = true;
			step = 0;
		}
	}

	void checkrayonmouse(){
		RaycastHit hit;
		Ray ray;
		if(Input.GetMouseButtonDown(0)){
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		}
		if(Input.GetMouseButton(0)){
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Debug.DrawRay(ray.origin, ray.direction * 20,Color.red);
			if(Physics.Raycast(ray,out hit,2000))
				if(hit.collider.gameObject.name == "SnowBall(Clone)" || hit.collider.gameObject.name == "SnowBall")
					Destroy(hit.collider.gameObject);
		}
		if(Input.GetMouseButtonUp(0)){}
	}
}
