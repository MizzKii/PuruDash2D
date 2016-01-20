using UnityEngine;
using System.Collections;

public class RocketFever : MonoBehaviour {
	
	private float speed;
	private GameControl gc;
	private CharacterControl cc;
	private GameObject _character;
	private bool down = false,jump = false;

	private int step = 1;
	
	private Vector3 pos = Vector3.zero;
	
	// Use this for initialization
	void Start () {
		gc = GameObject.Find("GameControl").GetComponent<GameControl>();
		speed = gc.speedControl;
		if(GameObject.Find("CharacterControl") != null) {
			_character = GameObject.Find("CharacterControl");
			_character.transform.name = "CC";
		}else
			_character = GameObject.Find("CC");
		cc = _character.GetComponent<CharacterControl>();
		pos.x = gc.stopCharacterX+1.5F;
		pos.y = -3.5F;
		pos.z = -2;
		
		posY.z = -2;
	}
	
	// Update is called once per frame
	void Update () {
		if(gc.SpeedFlow() > 0)
		if(step == 1) {
			if(!jump && down) {
				StartCoroutine(cc.JumpRocket());
				jump = true;
			}else if(transform.position.y < pos.y && !down) {
				down = true;
				pos.y = 1;
				StartCoroutine("SpeedUp");
			}else if(transform.position.y < 0)
				transform.position = Vector3.Slerp(transform.position, pos,3 * Time.deltaTime);
			else
				step = 2;
		}else if(gc.fever) {
			MoveRocket();
		}else if(step == 2){
			endRocket();
			step = 3;
		}else {
			if(transform.position.x < 10)
				transform.position = Vector3.Slerp(transform.position, pos,3 * Time.deltaTime);
			else 
				Destroy(transform.gameObject);
		}
	}
	
	IEnumerator SpeedUp() {
		//gc.speedControl *= 1.3F;
		//Time.timeScale *= 1.2F;
		yield return new WaitForSeconds(0.5F);
		gc.speedControl *= 1.2F;
		//Time.timeScale *= 1.2F;
		yield return new WaitForSeconds(0.5F);
		gc.speedControl *= 1.2F;
		//Time.timeScale *= 1.2F;
		yield return new WaitForSeconds(1);
		//Time.timeScale = gc.speedtime;
		if(!gc.fever) {
			endRocket();
		}
	}

	private void endRocket() {
		_character.transform.name = "CharacterControl";
		gc.speedControl = speed;
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
