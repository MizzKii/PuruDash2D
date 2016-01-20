using UnityEngine;
using System.Collections;

public class ForwardAttack : MonoBehaviour {

	private GameObject character;
	private Vector3 pos = Vector3.zero;
	private GameControl gameControl;
	private bool _do = false;
	
	// Use this for initialization
	void Start () {
		pos.z = -2;
		pos.x = transform.position.x;
		gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
		if(!gameControl.ghost) {
			character = GameObject.Find("CharacterControl");
			_do = true;
		}
	}
	
	private bool shoot = false,ready = true;
	// Update is called once per frame
	void Update () {
		if(transform.position.x < -10)
			Destroy(transform.gameObject);
		else if(ready){
			ready = false;
			StartCoroutine("Ready");
		}else if(shoot)
			if(end == 0)
				transform.position = Vector3.Lerp(transform.position, transform.position+Vector3.left,gameControl.SpeedFlow()*1.3F*Time.deltaTime);
			else
				BeeDown();
		else if(count > 1){
			if(_do){
				pos.y = character.transform.position.y;
				transform.position = Vector3.Slerp(transform.position, pos,3 * Time.deltaTime);
			}
		}
	}
	private int count = 2;
	IEnumerator Ready() {
		count = 1;
		yield return new WaitForSeconds(2);
		count = 1;
		yield return new WaitForSeconds(1);
		count = 0;
		yield return new WaitForSeconds(0.5F);
		count = -1;
		shoot = true;
	}
	
	public GUISkin _GUISkin;
	public Texture2D[] Wraning = new Texture2D[3];
	void OnGUI() {
		SetUpGUIMatrix.Resolution(800,480);
		GUI.skin = _GUISkin;
		if(count > -1){
			float y = 455 - Camera.main.WorldToScreenPoint(transform.position).y;
			//GUI.Label(new Rect(400,240,100,50),y.ToString());
			GUI.DrawTexture(new Rect(750,y,50,50), Wraning[count]);
		}
	}

	private int end = 0;
	private float tragetY;
	void OnTriggerEnter2D (Collider2D collision) {
		if (collision.gameObject.name == "CharacterControl") {
			end = 1;
			tragetY = transform.position.y+1;
			transform.position = new Vector3(transform.position.x, transform.position.y, -4);
		}
	}

	void BeeDown() {
		if(end == 1) {
			transform.Translate(Vector3.up *10* Time.deltaTime, Space.World);
			if(transform.position.y >= tragetY)
				end = 2;
		}else if(end == 2){
			transform.Translate(Vector3.down *10* Time.deltaTime, Space.World);
			if(transform.position.y < -5)
				Destroy(transform.gameObject);
		}
	}
}
