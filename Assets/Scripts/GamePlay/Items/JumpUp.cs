using UnityEngine;
using System.Collections;

public class JumpUp : MonoBehaviour {
	
	private int lv,time;
	void Start() {
		lv = PlayerPrefs.GetInt("LV_JumpUp");
		if(lv == 0)
			time = 4;
		else if(lv == 1)
			time = 10;
		else if(lv == 2)
			time = 14;
		else if(lv == 3)
			time = 18;
		else
			time = 24;
	}
	
	private Transform _Chracter;
	
	private bool _item = false,_start = true;
	void OnTriggerEnter2D (Collider2D collision) {
		if(collision.transform.name == "CharacterControl" || collision.transform.name == "CC"){
			_item = true;
			if(_start){
				_start = false;
				_Chracter = collision.transform;
				transform.position = new Vector3(0,0,5);
				StartCoroutine("RunItem",collision.transform.gameObject);
			}
		}
	}
	public Texture2D item;
	void OnGUI () {
		if(_item){
			Vector3 pos = Camera.main.WorldToScreenPoint(_Chracter.position);
			pos.y = Screen.height - pos.y;
			GUI.DrawTexture(new Rect(pos.x-20, pos.y-120, 100, 100), item);
		}
	}
	
	IEnumerator RunItem(GameObject go) {
		GameControl gc = GameObject.Find("GameControl").GetComponent<GameControl>();
		gc.jumpUp = true;
		yield return new WaitForSeconds(time);
		gc.jumpUp = false;
		Destroy(transform.gameObject);
	}
	
	void Update () {
		GameControl gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
		transform.Translate(Vector3.left * (gameControl.SpeedFlow() * Time.deltaTime),Space.World);
	}
}
