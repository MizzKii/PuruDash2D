using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {
	
	private int lv,time;
	
	private float speed;
	private GamePlay gp;
	private GameControl gc;
	private CharacterControl cc;
	private GameObject _character;
	private bool down = false,jump = false,end = false;
	
	private Vector3 pos = Vector3.zero;
	
	// Use this for initialization
	void Start () {
		lv = PlayerPrefs.GetInt("LV_Rocket");
		if(lv == 0)
			time = 8;
		else if(lv == 1)
			time = 10;
		else if(lv == 2)
			time = 14;
		else if(lv == 3)
			time = 18;
		else
			time = 24;
		//////////////////////////////////////////////////
		gp = GameObject.Find("GameControl").GetComponent<GamePlay>();
		gc = GameObject.Find("GameControl").GetComponent<GameControl>();
		gc.Rocket = true;
		gp.checkrungame = false;
		speed = gc.speedControl;
		if(GameObject.Find("CharacterControl") != null) {
			_character = GameObject.Find("CharacterControl");
			_character.transform.name = "CC";
			Debug.Log("CharacterControl");
		}else
			_character = GameObject.Find("CC");
		cc = _character.GetComponent<CharacterControl>();
		pos.x = gc.stopCharacterX+2;
		pos.y = -3.5F;
		pos.z = -2;

		posY.z = -2;
	}
	
	// Update is called once per frame
	void Update () {
		if(gc.SpeedFlow() > 0){
			if(!jump && down) {
				StartCoroutine(cc.JumpRocket());
				jump = true;
			}
		if(end) {
			if(transform.position.x < 10)
				transform.position = Vector3.Slerp(transform.position, pos,3 * Time.deltaTime);
			else 
				Destroy(transform.gameObject);
		}else if(_move)
			MoveRocket();
		else {	
			if(transform.position.y < pos.y && !down) {
				down = true;
				pos.y = 1;
				StartCoroutine("SpeedUp");
			}
			if(transform.position.y < 0)
				transform.position = Vector3.Slerp(transform.position, pos,3 * Time.deltaTime);
			}
		}
	}
	private bool _move = false;
	IEnumerator SpeedUp() {
		gc.speedControl *= 1.3F;
		//Time.timeScale *= 1.2F;
		int x = (int)Random.Range(0,5.9F);
		Instantiate(Resources.Load("PatternRocket/Lv0_"+x.ToString(), typeof(GameObject)));
		yield return new WaitForSeconds(0.5F);
		gc.speedControl *= 1.3F;
		//Time.timeScale *= 1.2F;
		yield return new WaitForSeconds(0.5F);
		gc.speedControl *= 1.3F;
		//Time.timeScale *= 1.2F;
		_move = true;
		yield return new WaitForSeconds(time);
		gc.speedControl = speed;
		//Time.timeScale = gc.speedtime;
		end = true;
		gc.Rocket = false;
		gc.ghost = false;
		gp.checkrungame = true;
		_character.transform.name = "CharacterControl";
		cc.Straight();
		pos.y = 4;
		pos.x = 12;
	}

	private Vector3 posY;

	void MoveRocket() {
		Ray ray;
		if(Input.GetMouseButtonDown(0)){
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Debug.DrawRay(ray.origin, ray.direction * 20,Color.green);
			posY.x = _character.transform.position.x;
			posY.y = ray.origin.y+1.5F;
			if(ray.origin.y > 4)
				posY.y = 5.5F;
			else if(ray.origin.y < -4)
				posY.y = -2.5F;
			_character.transform.position = Vector3.Lerp(_character.transform.position,posY,0.1F);
			posY.x = transform.position.x;
			posY.y = _character.transform.position.y-1.5F;
			transform.position = posY;
		}
		else if(Input.GetMouseButton(0)){
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Debug.DrawRay(ray.origin, ray.direction * 20,Color.green);
			posY.x = _character.transform.position.x;
			posY.y = ray.origin.y+1.5F;
			if(ray.origin.y > 4)
				posY.y = 5.5F;
			else if(ray.origin.y < -4)
				posY.y = -2.5F;
			_character.transform.position = Vector3.Lerp(_character.transform.position,posY,0.1F);
			posY.x = transform.position.x;
			posY.y = _character.transform.position.y-1.5F;
			transform.position = posY;
		}
	}
}
